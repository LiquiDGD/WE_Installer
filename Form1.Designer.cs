namespace PrometheusExporterInstaller
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.Button CreateConfigButton;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Button UninstallButton;
        private System.Windows.Forms.Button DetailsButton;

        private System.Windows.Forms.ListBox listBoxServicesAvailable;
        private System.Windows.Forms.ListBox listBoxServicesSelected;
        private System.Windows.Forms.Button buttonAddService;
        private System.Windows.Forms.Button buttonRemoveService;
        private System.Windows.Forms.ListBox listBoxProcessesAvailable;
        private System.Windows.Forms.ListBox listBoxProcessesSelected;
        private System.Windows.Forms.Button buttonAddProcess;
        private System.Windows.Forms.Button buttonRemoveProcess;

        // CheckedListBox for collectors
        private System.Windows.Forms.CheckedListBox checkedListBoxCollectors;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.InstallButton = new System.Windows.Forms.Button();
            this.CreateConfigButton = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.UninstallButton = new System.Windows.Forms.Button();
            this.DetailsButton = new System.Windows.Forms.Button();
            this.listBoxServicesAvailable = new System.Windows.Forms.ListBox();
            this.listBoxServicesSelected = new System.Windows.Forms.ListBox();
            this.buttonAddService = new System.Windows.Forms.Button();
            this.buttonRemoveService = new System.Windows.Forms.Button();
            this.listBoxProcessesAvailable = new System.Windows.Forms.ListBox();
            this.listBoxProcessesSelected = new System.Windows.Forms.ListBox();
            this.buttonAddProcess = new System.Windows.Forms.Button();
            this.buttonRemoveProcess = new System.Windows.Forms.Button();
            this.checkedListBoxCollectors = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // InstallButton
            // 
            this.InstallButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.InstallButton.Location = new System.Drawing.Point(8, 4);
            this.InstallButton.Margin = new System.Windows.Forms.Padding(2);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(150, 24);
            this.InstallButton.TabIndex = 0;
            this.InstallButton.Text = "Install Exporter";
            this.InstallButton.UseVisualStyleBackColor = true;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // CreateConfigButton
            // 
            this.CreateConfigButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CreateConfigButton.Location = new System.Drawing.Point(163, 4);
            this.CreateConfigButton.Margin = new System.Windows.Forms.Padding(2);
            this.CreateConfigButton.Name = "CreateConfigButton";
            this.CreateConfigButton.Size = new System.Drawing.Size(150, 24);
            this.CreateConfigButton.TabIndex = 1;
            this.CreateConfigButton.Text = "Create Config File";
            this.CreateConfigButton.UseVisualStyleBackColor = true;
            this.CreateConfigButton.Click += new System.EventHandler(this.CreateConfigButton_Click);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.OutputTextBox.Location = new System.Drawing.Point(8, 334);
            this.OutputTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.Size = new System.Drawing.Size(670, 0);
            this.OutputTextBox.TabIndex = 2;
            this.OutputTextBox.Visible = false;
            this.OutputTextBox.TextChanged += new System.EventHandler(this.OutputTextBox_TextChanged);
            // 
            // UninstallButton
            // 
            this.UninstallButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.UninstallButton.Location = new System.Drawing.Point(372, 3);
            this.UninstallButton.Name = "UninstallButton";
            this.UninstallButton.Size = new System.Drawing.Size(150, 24);
            this.UninstallButton.TabIndex = 3;
            this.UninstallButton.Text = "Uninstall Exporter";
            this.UninstallButton.UseVisualStyleBackColor = true;
            this.UninstallButton.Click += new System.EventHandler(this.UninstallButton_Click);
            // 
            // DetailsButton
            // 
            this.DetailsButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DetailsButton.Location = new System.Drawing.Point(528, 3);
            this.DetailsButton.Name = "DetailsButton";
            this.DetailsButton.Size = new System.Drawing.Size(150, 24);
            this.DetailsButton.TabIndex = 4;
            this.DetailsButton.Text = "Show Details";
            this.DetailsButton.UseVisualStyleBackColor = true;
            this.DetailsButton.Click += new System.EventHandler(this.DetailsButton_Click);
            // 
            // listBoxServicesAvailable
            // 
            this.listBoxServicesAvailable.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.listBoxServicesAvailable.Location = new System.Drawing.Point(8, 133);
            this.listBoxServicesAvailable.Name = "listBoxServicesAvailable";
            this.listBoxServicesAvailable.Size = new System.Drawing.Size(305, 95);
            this.listBoxServicesAvailable.TabIndex = 5;
            // 
            // listBoxServicesSelected
            // 
            this.listBoxServicesSelected.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.listBoxServicesSelected.Location = new System.Drawing.Point(375, 133);
            this.listBoxServicesSelected.Name = "listBoxServicesSelected";
            this.listBoxServicesSelected.Size = new System.Drawing.Size(303, 95);
            this.listBoxServicesSelected.TabIndex = 6;
            // 
            // buttonAddService
            // 
            this.buttonAddService.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAddService.Location = new System.Drawing.Point(321, 133);
            this.buttonAddService.Name = "buttonAddService";
            this.buttonAddService.Size = new System.Drawing.Size(50, 23);
            this.buttonAddService.TabIndex = 7;
            this.buttonAddService.Text = ">";
            this.buttonAddService.UseVisualStyleBackColor = true;
            this.buttonAddService.Click += new System.EventHandler(this.ButtonAddService_Click);
            // 
            // buttonRemoveService
            // 
            this.buttonRemoveService.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonRemoveService.Location = new System.Drawing.Point(319, 162);
            this.buttonRemoveService.Name = "buttonRemoveService";
            this.buttonRemoveService.Size = new System.Drawing.Size(50, 23);
            this.buttonRemoveService.TabIndex = 8;
            this.buttonRemoveService.Text = "<";
            this.buttonRemoveService.UseVisualStyleBackColor = true;
            this.buttonRemoveService.Click += new System.EventHandler(this.ButtonRemoveService_Click);
            // 
            // listBoxProcessesAvailable
            // 
            this.listBoxProcessesAvailable.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.listBoxProcessesAvailable.Location = new System.Drawing.Point(8, 234);
            this.listBoxProcessesAvailable.Name = "listBoxProcessesAvailable";
            this.listBoxProcessesAvailable.Size = new System.Drawing.Size(305, 95);
            this.listBoxProcessesAvailable.TabIndex = 9;
            // 
            // listBoxProcessesSelected
            // 
            this.listBoxProcessesSelected.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.listBoxProcessesSelected.Location = new System.Drawing.Point(375, 234);
            this.listBoxProcessesSelected.Name = "listBoxProcessesSelected";
            this.listBoxProcessesSelected.Size = new System.Drawing.Size(303, 95);
            this.listBoxProcessesSelected.TabIndex = 10;
            // 
            // buttonAddProcess
            // 
            this.buttonAddProcess.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAddProcess.Location = new System.Drawing.Point(319, 234);
            this.buttonAddProcess.Name = "buttonAddProcess";
            this.buttonAddProcess.Size = new System.Drawing.Size(50, 23);
            this.buttonAddProcess.TabIndex = 11;
            this.buttonAddProcess.Text = ">";
            this.buttonAddProcess.UseVisualStyleBackColor = true;
            this.buttonAddProcess.Click += new System.EventHandler(this.ButtonAddProcess_Click);
            // 
            // buttonRemoveProcess
            // 
            this.buttonRemoveProcess.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonRemoveProcess.Location = new System.Drawing.Point(319, 263);
            this.buttonRemoveProcess.Name = "buttonRemoveProcess";
            this.buttonRemoveProcess.Size = new System.Drawing.Size(50, 23);
            this.buttonRemoveProcess.TabIndex = 12;
            this.buttonRemoveProcess.Text = "<";
            this.buttonRemoveProcess.UseVisualStyleBackColor = true;
            this.buttonRemoveProcess.Click += new System.EventHandler(this.ButtonRemoveProcess_Click);
            // 
            // checkedListBoxCollectors
            // 
            this.checkedListBoxCollectors.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkedListBoxCollectors.CheckOnClick = true;
            this.checkedListBoxCollectors.FormattingEnabled = true;
            this.checkedListBoxCollectors.Location = new System.Drawing.Point(8, 33);
            this.checkedListBoxCollectors.MultiColumn = true;
            this.checkedListBoxCollectors.Name = "checkedListBoxCollectors";
            this.checkedListBoxCollectors.Size = new System.Drawing.Size(670, 94);
            this.checkedListBoxCollectors.Sorted = true;
            this.checkedListBoxCollectors.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(684, 337);
            this.Controls.Add(this.checkedListBoxCollectors);
            this.Controls.Add(this.buttonRemoveProcess);
            this.Controls.Add(this.buttonAddProcess);
            this.Controls.Add(this.listBoxProcessesSelected);
            this.Controls.Add(this.listBoxProcessesAvailable);
            this.Controls.Add(this.buttonRemoveService);
            this.Controls.Add(this.buttonAddService);
            this.Controls.Add(this.listBoxServicesSelected);
            this.Controls.Add(this.listBoxServicesAvailable);
            this.Controls.Add(this.DetailsButton);
            this.Controls.Add(this.UninstallButton);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.CreateConfigButton);
            this.Controls.Add(this.InstallButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Prometheus Exporter Installer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}