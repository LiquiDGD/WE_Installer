using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace PrometheusExporterInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void InstallButton_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\Program Files\windows_exporter";
            string configFileName = "config.yml";
            string filePath = Path.Combine(folderPath, configFileName);
            string tempInstallerPath = Path.Combine(Path.GetTempPath(), "windows_exporter.exe");

            string installerUrl = await GetLatestInstallerUrlAsync();
            if (string.IsNullOrEmpty(installerUrl))
            {
                OutputTextBox.AppendText("Failed to find the latest installer URL.\n");
                return;
            }

            await DownloadFileAsync(installerUrl, tempInstallerPath);

            if (File.Exists(tempInstallerPath))
            {
                if (IsServiceInstalled("windows_exporter"))
                {
                    DialogResult result = MessageBox.Show("Exporter is already installed. Do you want to uninstall the existing exporter?", "Uninstall Exporter", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        UninstallExporter();
                    }
                    else
                    {
                        OutputTextBox.AppendText("Exiting script as the exporter is already installed.\n");
                        return;
                    }
                }
                else
                {
                    OutputTextBox.AppendText("windows_exporter service is not installed.\n");
                }

                DialogResult createConfig = MessageBox.Show("Do you want to use a custom config file?", "Create Config File", MessageBoxButtons.YesNo);

                bool customConfigUsed = false;
                if (createConfig == DialogResult.Yes)
                {
                    customConfigUsed = CreateConfigFile(filePath, folderPath);
                }
                else
                {
                    if (File.Exists(filePath))
                    {
                        DialogResult removeConfig = MessageBox.Show("A config file already exists. Do you want to remove the existing config file?", "Remove Config File", MessageBoxButtons.YesNo);
                        if (removeConfig == DialogResult.Yes)
                        {
                            File.Delete(filePath);
                            OutputTextBox.AppendText("Existing config file removed.\n");
                        }
                    }
                }

                InstallExporter(tempInstallerPath, folderPath, customConfigUsed ? filePath : null);
            }
            else
            {
                OutputTextBox.AppendText("Installer not found. Please ensure the installer is downloaded correctly.\n");
            }
        }

        private async void CreateConfigButton_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\Program Files\windows_exporter";
            string filePath = Path.Combine(folderPath, "config.yml");

            bool customConfigUsed = CreateConfigFile(filePath, folderPath);
            if (customConfigUsed)
            {
                OutputTextBox.AppendText("Custom config file created.\n");
            }
            else
            {
                OutputTextBox.AppendText("Failed to create custom config file.\n");
            }
        }

        private async Task<string> GetLatestInstallerUrlAsync()
        {
            string apiUrl = "https://api.github.com/repos/prometheus-community/windows_exporter/releases/latest";
            string downloadUrl = null;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; WindowsExporterInstaller/1.0)");
                try
                {
                    var response = await client.GetStringAsync(apiUrl);
                    JObject release = JObject.Parse(response);

                    var assets = release["assets"];
                    foreach (var asset in assets)
                    {
                        var name = asset["name"].ToString();
                        if (name.EndsWith("amd64.exe"))
                        {
                            downloadUrl = asset["browser_download_url"].ToString();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    OutputTextBox.AppendText($"An error occurred while retrieving the latest installer URL: {ex.Message}\n");
                }
            }

            return downloadUrl;
        }

        private async Task DownloadFileAsync(string url, string outputPath)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    OutputTextBox.AppendText($"Downloading installer from {url}...\n");
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    using (var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fs);
                    }

                    OutputTextBox.AppendText($"Downloaded installer to {outputPath}\n");
                }
                catch (Exception ex)
                {
                    OutputTextBox.AppendText($"An error occurred while downloading the installer: {ex.Message}\n");
                }
            }
        }

        private bool IsServiceInstalled(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            return services.Any(s => s.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
        }

        private void UninstallExporter()
        {
            if (!IsServiceInstalled("windows_exporter"))
            {
                OutputTextBox.AppendText("The windows_exporter service is not installed.\n");
                return;
            }

            try
            {
                ServiceController service = new ServiceController("windows_exporter");
                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    OutputTextBox.AppendText("Stopping the windows_exporter service...\n");
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMinutes(1));
                    OutputTextBox.AppendText("Service stopped.\n");
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText($"Failed to stop the service: {ex.Message}\n");
            }

            OutputTextBox.AppendText("Uninstalling, please wait...\n");
            var uninstallProcess = Process.Start(new ProcessStartInfo("sc", "delete windows_exporter")
            {
                UseShellExecute = true,
                Verb = "runas"
            });
            uninstallProcess.WaitForExit();
            if (uninstallProcess.ExitCode == 0)
            {
                OutputTextBox.AppendText("Exporter uninstalled.\n");
            }
            else
            {
                OutputTextBox.AppendText($"Failed to uninstall the exporter. Exit code: {uninstallProcess.ExitCode}\n");
            }
        }

        private void InstallExporter(string installerPath, string folderPath, string configFilePath)
        {
            try
            {
                // Ensure the folder exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Define the target path for the installer
                string targetPath = Path.Combine(folderPath, "windows_exporter.exe");

                // Copy the installer to the target folder
                File.Copy(installerPath, targetPath, true);

                // Ensure the service is completely removed before recreating it
                EnsureServiceRemoved("windows_exporter");

                // Create the service using sc.exe
                string createServiceCommand = $"create windows_exporter binPath= \"{targetPath}\" start= auto";
                RunCommand("sc", createServiceCommand);

                // Update the service to add arguments using PowerShell
                string serviceArgs = configFilePath != null
                    ? $"\"{targetPath}\" --log.file eventlog --web.listen-address 0.0.0.0:9182 --config.file=\\\"{configFilePath}\\\""
                    : $"\"{targetPath}\" --log.file eventlog --web.listen-address 0.0.0.0:9182";

                string setServicePathScript = $"$servicePath = '{serviceArgs}'; Set-ItemProperty -Path 'HKLM:\\SYSTEM\\CurrentControlSet\\Services\\windows_exporter' -Name ImagePath -Value $servicePath";
                RunPowerShellCommand(setServicePathScript);

                // Start the service
                string startServiceCommand = "start windows_exporter";
                RunCommand("sc.exe", startServiceCommand);

                OutputTextBox.AppendText("Exporter installed and service started successfully.\n");
            }
            catch (UnauthorizedAccessException)
            {
                OutputTextBox.AppendText("Access to the path is denied. Please run the application as an administrator.\n");
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText($"An error occurred while installing the exporter: {ex.Message}\n");
            }
        }

        private void EnsureServiceRemoved(string serviceName)
        {
            try
            {
                ServiceController service = new ServiceController(serviceName);
                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMinutes(1));
                }
            }
            catch (InvalidOperationException)
            {
                // Service does not exist
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText($"An error occurred while stopping the service: {ex.Message}\n");
            }

            var uninstallProcess = Process.Start(new ProcessStartInfo("sc", $"delete {serviceName}")
            {
                UseShellExecute = true,
                Verb = "runas"
            });
            uninstallProcess.WaitForExit();
            if (uninstallProcess.ExitCode != 0 && uninstallProcess.ExitCode != 1060)
            {
                throw new InvalidOperationException($"Command 'sc delete {serviceName}' failed with exit code {uninstallProcess.ExitCode}");
            }
        }

        private void RunCommand(string executable, string arguments)
        {
            var process = Process.Start(new ProcessStartInfo(executable, arguments)
            {
                UseShellExecute = true,
                Verb = "runas"
            });
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"Command '{executable} {arguments}' failed with exit code {process.ExitCode}");
            }
        }

        private void RunPowerShellCommand(string script)
        {
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{script}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"PowerShell command failed with exit code {process.ExitCode}: {error}");
            }

            OutputTextBox.AppendText(output);
        }

        private bool CreateConfigFile(string filePath, string folderPath)
        {
            if (File.Exists(filePath))
            {
                DialogResult dialogResult = MessageBox.Show($"A config file already exists at {filePath}. Do you want to overwrite the existing config file?", "Overwrite Config", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    OutputTextBox.AppendText("Using the existing config file.\n");
                    return true;
                }
                else
                {
                    File.Delete(filePath);
                    OutputTextBox.AppendText("Existing config file removed.\n");
                }
            }

            DialogResult modifyCollectorsResult = MessageBox.Show("Do you want to modify the list of enabled collectors defaults (cpu,cs,logical_disk,net,os,physical_disk,process,service,system,textfile)?", "Modify Collectors", MessageBoxButtons.YesNo);
            string enabledCollectors = "cpu,cs,logical_disk,net,os,physical_disk,process,service,system,textfile";
            if (modifyCollectorsResult == DialogResult.Yes)
            {
                enabledCollectors = PromptForInput("Enter the collectors you want to enable (comma-separated), or press Enter to use defaults:");
                if (string.IsNullOrEmpty(enabledCollectors))
                {
                    enabledCollectors = "cpu,cs,logical_disk,net,os,physical_disk,process,service,system,textfile";
                }
            }

            var services = ServiceController.GetServices().Select(s => s.ServiceName).ToList();
            string serviceNames = PromptForInput("Enter the service names from the list (comma-separated for multiple services):", services);

            var formattedServiceNames = string.Join(" OR ", serviceNames.Split(',').Select(s => $"Name='{s.Trim()}'"));

            var processes = Process.GetProcesses().Select(p => p.ProcessName).Distinct().ToList();
            string processIncludes = PromptForInput("Enter the process names from the list (comma-separated for multiple processes):", processes);

            var formattedProcessIncludes = string.Join("|", processIncludes.Split(',').Select(p => p.Trim()));

            string configContent = $@"
---
collectors:
  enabled: {enabledCollectors}
collector:
  service:
    services-where: {formattedServiceNames}
  process:
    include: ""{formattedProcessIncludes}""
log:
  level: debug
scrape:
  timeout-margin: 0.5
telemetry:
  path: /metrics
  max-requests: 5
web:
  listen-address: "":9182""
";

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    OutputTextBox.AppendText($"Folder created at {folderPath}\n");
                }
                else
                {
                    OutputTextBox.AppendText($"Folder exists at {folderPath}\n");
                }

                File.WriteAllText(filePath, configContent);
                OutputTextBox.AppendText($"File created at {filePath} with the provided configuration\n");
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                OutputTextBox.AppendText("Access to the path is denied. Please run the application as an administrator.\n");
                return false;
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText($"An error occurred while creating the config file: {ex.Message}\n");
                return false;
            }
        }

        private string PromptForInput(string message, List<string> options = null)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(message, "Input Required", "");

            if (options != null)
            {
                foreach (var option in options)
                {
                    OutputTextBox.AppendText($"{option}\n");
                }
            }

            return input;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
