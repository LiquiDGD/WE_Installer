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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.OutputTextBox.Location = new System.Drawing.Point(8, 382);
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
            this.listBoxServicesAvailable.Location = new System.Drawing.Point(8, 168);
            this.listBoxServicesAvailable.Name = "listBoxServicesAvailable";
            this.listBoxServicesAvailable.Size = new System.Drawing.Size(305, 95);
            this.listBoxServicesAvailable.TabIndex = 5;
            // 
            // listBoxServicesSelected
            // 
            this.listBoxServicesSelected.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.listBoxServicesSelected.Location = new System.Drawing.Point(375, 168);
            this.listBoxServicesSelected.Name = "listBoxServicesSelected";
            this.listBoxServicesSelected.Size = new System.Drawing.Size(303, 95);
            this.listBoxServicesSelected.TabIndex = 6;
            // 
            // buttonAddService
            // 
            this.buttonAddService.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAddService.Location = new System.Drawing.Point(321, 168);
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
            this.buttonRemoveService.Location = new System.Drawing.Point(319, 197);
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
            this.listBoxProcessesAvailable.Location = new System.Drawing.Point(8, 282);
            this.listBoxProcessesAvailable.Name = "listBoxProcessesAvailable";
            this.listBoxProcessesAvailable.Size = new System.Drawing.Size(305, 95);
            this.listBoxProcessesAvailable.TabIndex = 9;
            // 
            // listBoxProcessesSelected
            // 
            this.listBoxProcessesSelected.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.listBoxProcessesSelected.Location = new System.Drawing.Point(375, 282);
            this.listBoxProcessesSelected.Name = "listBoxProcessesSelected";
            this.listBoxProcessesSelected.Size = new System.Drawing.Size(303, 95);
            this.listBoxProcessesSelected.TabIndex = 10;
            // 
            // buttonAddProcess
            // 
            this.buttonAddProcess.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAddProcess.Location = new System.Drawing.Point(319, 282);
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
            this.buttonRemoveProcess.Location = new System.Drawing.Point(319, 311);
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
            this.checkedListBoxCollectors.Location = new System.Drawing.Point(8, 53);
            this.checkedListBoxCollectors.MultiColumn = true;
            this.checkedListBoxCollectors.Name = "checkedListBoxCollectors";
            this.checkedListBoxCollectors.Size = new System.Drawing.Size(670, 94);
            this.checkedListBoxCollectors.Sorted = true;
            this.checkedListBoxCollectors.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Processes:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Services:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Collectors:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(684, 386);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Prometheus Exporter Installer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}