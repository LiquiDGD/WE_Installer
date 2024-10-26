﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
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
        private bool detailsVisible = false;
        private int normalHeight;

        public Form1()
        {
            InitializeComponent();
            normalHeight = this.ClientSize.Height;
        }

        private async Task DownloadAndExtractJsonPackageAsync()
        {
            string jsonPackageUrl = $"https://www.nuget.org/api/v2/package/Newtonsoft.Json/13.0.3";
            string jsonPackagePath = Path.Combine(Path.GetTempPath(), "Newtonsoft.Json.nupkg");
            string jsonDllPath = Path.Combine(Application.StartupPath, "Newtonsoft.Json.dll");

            if (File.Exists(jsonDllPath))
            {
                OutputTextBox.AppendText($"Newtonsoft.Json.dll already exists at {jsonDllPath}\r\n");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    OutputTextBox.AppendText($"Downloading Newtonsoft.Json package from {jsonPackageUrl}...\r\n");
                    var response = await client.GetAsync(jsonPackageUrl);
                    response.EnsureSuccessStatusCode();

                    using (var fs = new FileStream(jsonPackagePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fs);
                    }

                    OutputTextBox.AppendText($"Downloaded Newtonsoft.Json package to {jsonPackagePath}\r\n");

                    using (var zipArchive = ZipFile.OpenRead(jsonPackagePath))
                    {
                        var entry = zipArchive.Entries
                            .FirstOrDefault(e => e.FullName.Equals("lib/net45/Newtonsoft.Json.dll", StringComparison.OrdinalIgnoreCase));

                        if (entry != null)
                        {
                            string tempPath = Path.Combine(Path.GetTempPath(), entry.Name);
                            entry.ExtractToFile(tempPath, true);
                            File.Move(tempPath, jsonDllPath);
                            File.SetAttributes(jsonDllPath, FileAttributes.Hidden);
                            OutputTextBox.AppendText($"Extracted Newtonsoft.Json.dll to {jsonDllPath}\r\n");
                        }
                        else
                        {
                            OutputTextBox.AppendText("Newtonsoft.Json.dll for net45 not found in the package.\r\n");
                        }
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    OutputTextBox.AppendText($"HTTP Request error: {httpEx.Message}\r\n");
                }
                catch (Exception ex)
                {
                    OutputTextBox.AppendText($"An error occurred while downloading and extracting Newtonsoft.Json package: {ex.Message}\r\n");
                }
            }
        }

        private async void InstallButton_Click(object sender, EventArgs e)
        {
            await DownloadAndExtractJsonPackageAsync();
            string folderPath = @"C:\Program Files\windows_exporter";
            string configFileName = "config.yml";
            string filePath = Path.Combine(folderPath, configFileName);
            string tempInstallerPath = Path.Combine(Path.GetTempPath(), "windows_exporter.exe");

            string installerUrl = await GetLatestInstallerUrlAsync();
            if (string.IsNullOrEmpty(installerUrl))
            {
                OutputTextBox.AppendText("Failed to find the latest installer URL.\r\n");
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
                        OutputTextBox.AppendText("Exiting script as the exporter is already installed.\r\n");
                        return;
                    }
                }
                else
                {
                    OutputTextBox.AppendText("windows_exporter service is not installed.\r\n");
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
                            OutputTextBox.AppendText("Existing config file removed.\r\n");
                        }
                    }
                }

                InstallExporter(tempInstallerPath, folderPath, customConfigUsed ? filePath : null);
            }
            else
            {
                OutputTextBox.AppendText("Installer not found. Please ensure the installer is downloaded correctly.\r\n");
            }
        }

        private void CreateConfigButton_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\Program Files\windows_exporter";
            string filePath = Path.Combine(folderPath, "config.yml");

            bool customConfigUsed = CreateConfigFile(filePath, folderPath);
            if (customConfigUsed)
            {
                OutputTextBox.AppendText("Custom config file created.\r\n");
            }
            else
            {
                OutputTextBox.AppendText("Failed to create custom config file.\r\n");
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
                    OutputTextBox.AppendText($"Sending request to GitHub API: {apiUrl}\r\n");
                    var response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    JObject release = JObject.Parse(await response.Content.ReadAsStringAsync());
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

                    if (!string.IsNullOrEmpty(downloadUrl))
                    {
                        OutputTextBox.AppendText("Found download URL.\r\n");
                    }
                    else
                    {
                        OutputTextBox.AppendText("Failed to find a suitable download URL in the response.\r\n");
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    OutputTextBox.AppendText($"HTTP Request error: {httpEx.Message}\r\n");
                }
                catch (Exception ex)
                {
                    OutputTextBox.AppendText($"An error occurred while retrieving the latest installer URL: {ex.Message}\r\n");
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
                    OutputTextBox.AppendText($"Downloading installer from {url}...\r\n");
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    using (var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fs);
                    }

                    OutputTextBox.AppendText($"Downloaded installer to {outputPath}\r\n");
                }
                catch (Exception ex)
                {
                    OutputTextBox.AppendText($"An error occurred while downloading the installer: {ex.Message}\r\n");
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
                OutputTextBox.AppendText("The windows_exporter service is not installed.\r\n");
                return;
            }

            try
            {
                ServiceController service = new ServiceController("windows_exporter");

                OutputTextBox.AppendText($"Service status before stopping: {service.Status}\r\n");

                if (service.Status != ServiceControllerStatus.Stopped && service.Status != ServiceControllerStatus.StopPending)
                {
                    OutputTextBox.AppendText("Stopping the windows_exporter service...\r\n");
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMinutes(1));
                    OutputTextBox.AppendText("Service stopped.\r\n");
                }
                else
                {
                    OutputTextBox.AppendText("Service is already stopped or stopping.\r\n");
                }
            }
            catch (InvalidOperationException ex)
            {
                OutputTextBox.AppendText($"Invalid operation while stopping the service: {ex.Message}\r\n");
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText($"Failed to stop the service: {ex.Message}\r\n");
            }

            OutputTextBox.AppendText("Uninstalling, please wait...\r\n");
            var uninstallProcess = Process.Start(new ProcessStartInfo("sc", "delete windows_exporter")
            {
                UseShellExecute = true,
                Verb = "runas"
            });
            uninstallProcess.WaitForExit();
            if (uninstallProcess.ExitCode == 0)
            {
                OutputTextBox.AppendText("Exporter uninstalled.\r\n");
            }
            else
            {
                OutputTextBox.AppendText($"Failed to uninstall the exporter. Exit code: {uninstallProcess.ExitCode}\r\n");
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

                OutputTextBox.AppendText("Exporter installed and service started successfully.\r\n");
            }
            catch (UnauthorizedAccessException)
            {
                OutputTextBox.AppendText("Access to the path is denied. Please run the application as an administrator.\r\n");
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText($"An error occurred while installing the exporter: {ex.Message}\r\n");
            }
        }

        private void EnsureServiceRemoved(string serviceName)
        {
            try
            {
                // Check if the service exists
                if (!CheckServiceInstalled(serviceName))
                {
                    OutputTextBox.AppendText($"Service '{serviceName}' does not exist.\r\n");
                    return;
                }

                ServiceController service = new ServiceController(serviceName);

                // Attempt to stop the service if it's running or in a pending state
                if (service.Status != ServiceControllerStatus.Stopped && service.Status != ServiceControllerStatus.StopPending)
                {
                    OutputTextBox.AppendText($"Stopping the '{serviceName}' service...\r\n");
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMinutes(1));
                    OutputTextBox.AppendText("Service stopped successfully.\r\n");
                }
                else
                {
                    OutputTextBox.AppendText($"Service '{serviceName}' is already stopped.\r\n");
                }
            }
            catch (InvalidOperationException)
            {
                // Service does not exist, or other operation errors
                OutputTextBox.AppendText($"Service '{serviceName}' does not exist or could not be managed.\r\n");
                return;
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText($"An error occurred while stopping the service: {ex.Message}\r\n");
                return;
            }

            // Attempt to delete the service
            OutputTextBox.AppendText($"Uninstalling the '{serviceName}' service...\r\n");
            var uninstallProcess = Process.Start(new ProcessStartInfo("sc", $"delete {serviceName}")
            {
                UseShellExecute = true,
                Verb = "runas"
            });
            uninstallProcess.WaitForExit();

            // Check the exit code and provide feedback
            if (uninstallProcess.ExitCode == 0)
            {
                OutputTextBox.AppendText($"Service '{serviceName}' uninstalled successfully.\r\n");
            }
            else if (uninstallProcess.ExitCode == 1060)
            {
                OutputTextBox.AppendText($"Service '{serviceName}' does not exist or was already removed.\r\n");
            }
            else
            {
                OutputTextBox.AppendText($"Failed to uninstall the service '{serviceName}'. Exit code: {uninstallProcess.ExitCode}\r\n");
                throw new InvalidOperationException($"Command 'sc delete {serviceName}' failed with exit code {uninstallProcess.ExitCode}");
            }
        }

        // Helper method to check if a service is installed
        private bool CheckServiceInstalled(string serviceName)
        {
            return ServiceController.GetServices().Any(s => s.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
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
                throw new InvalidOperationException($"PowerShell command failed with exit code {process.ExitCode}: {error}\r\n");
            }

            OutputTextBox.AppendText(output);
        }

        private bool CreateConfigFile(string filePath, string folderPath)
        {
            // Full list of collectors
            var selectedCollectors = checkedListBoxCollectors.CheckedItems.Cast<string>().ToList();
            var defaultCollectors = new List<string> { "cpu", "cs", "logical_disk", "net", "os", "physical_disk", "process", "service", "system", "textfile" };

            // Ensure default collectors are included
            selectedCollectors.AddRange(defaultCollectors.Except(selectedCollectors));

            // Service and process sections
            var selectedServices = listBoxServicesSelected.Items.Cast<string>().ToList();
            var selectedProcesses = listBoxProcessesSelected.Items.Cast<string>().ToList();

            var formattedServiceNames = string.Join("|", selectedServices);
            var formattedProcessIncludes = string.Join("|", selectedProcesses);

            string configContent = $@"
---
collectors:
  enabled: {string.Join(",", selectedCollectors)}
collector:
  service:
    include: ""{formattedServiceNames}""
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
                    OutputTextBox.AppendText($"Folder created at {folderPath}\r\n");
                }
                else
                {
                    OutputTextBox.AppendText($"Folder exists at {folderPath}\r\n");
                }

                File.WriteAllText(filePath, configContent);
                OutputTextBox.AppendText($"File created at {filePath} with the provided configuration\r\n");
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                OutputTextBox.AppendText("Access to the path is denied. Please run the application as an administrator.\r\n");
                return false;
            }
            catch (Exception ex)
            {
                OutputTextBox.AppendText($"An error occurred while creating the config file: {ex.Message}\r\n");
                return false;
            }
        }

        private void ButtonAddService_Click(object sender, EventArgs e)
        {
            MoveSelectedItem(listBoxServicesAvailable, listBoxServicesSelected);
        }

        private void ButtonRemoveService_Click(object sender, EventArgs e)
        {
            MoveSelectedItem(listBoxServicesSelected, listBoxServicesAvailable);
        }

        private void ButtonAddProcess_Click(object sender, EventArgs e)
        {
            MoveSelectedItem(listBoxProcessesAvailable, listBoxProcessesSelected);
        }

        private void ButtonRemoveProcess_Click(object sender, EventArgs e)
        {
            MoveSelectedItem(listBoxProcessesSelected, listBoxProcessesAvailable);
        }

        private void MoveSelectedItem(ListBox source, ListBox destination)
        {
            if (source.SelectedItem != null)
            {
                destination.Items.Add(source.SelectedItem);
                source.Items.Remove(source.SelectedItem);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var services = ServiceController.GetServices().Select(s => s.ServiceName).OrderBy(name => name).ToList();
            var processes = Process.GetProcesses().Select(p => p.ProcessName).Distinct().OrderBy(name => name).ToList();

            listBoxServicesAvailable.Items.AddRange(services.ToArray());
            listBoxProcessesAvailable.Items.AddRange(processes.ToArray());

            // Full list of collectors to show in CheckedListBox
            string[] allCollectors = {
                "ad", "adcs", "adfs", "cache", "cpu", "cpu_info", "cs", "container", "diskdrive", "dfsr", "dhcp",
                "dns", "exchange", "fsrmquota", "hyperv", "iis", "license", "logical_disk", "logon", "memory",
                "mscluster_cluster", "mscluster_network", "mscluster_node", "mscluster_resource", "mscluster_resourcegroup",
                "msmq", "mssql", "netframework_clrexceptions", "netframework_clrinterop", "netframework_clrjit",
                "netframework_clrloading", "netframework_clrlocksandthreads", "netframework_clrmemory", "netframework_clrremoting",
                "netframework_clrsecurity", "net", "os", "physical_disk", "printer", "process", "remote_fx", "scheduled_task",
                "service", "smb", "smbclient", "smtp", "system", "tcp", "teradici_pcoip", "time", "thermalzone",
                "terminal_services", "textfile", "vmware_blast", "vmware"
            };

            string[] defaultCollectors = { "cpu", "cs", "logical_disk", "net", "os", "physical_disk", "process", "service", "system", "textfile" };

            foreach (var collector in allCollectors)
            {
                checkedListBoxCollectors.Items.Add(collector, defaultCollectors.Contains(collector));
            }
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            UninstallExporter();
        }

        private void DetailsButton_Click(object sender, EventArgs e)
        {
            detailsVisible = !detailsVisible;
            OutputTextBox.Visible = detailsVisible;
            DetailsButton.Text = detailsVisible ? "Hide Details" : "Show Details";
            this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, detailsVisible ? normalHeight + 200 : normalHeight);
        }

        private void OutputTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
