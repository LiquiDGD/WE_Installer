namespace PrometheusExporterInstaller
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button InstallButton;
        private System.Windows.Forms.Button CreateConfigButton;
        private System.Windows.Forms.TextBox OutputTextBox;

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
            this.InstallButton = new System.Windows.Forms.Button();
            this.CreateConfigButton = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InstallButton
            // 
            this.InstallButton.Location = new System.Drawing.Point(9, 10);
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
            this.CreateConfigButton.Location = new System.Drawing.Point(164, 10);
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
            this.OutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextBox.Location = new System.Drawing.Point(13, 42);
            this.OutputTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.ReadOnly = true;
            this.OutputTextBox.Size = new System.Drawing.Size(738, 314);
            this.OutputTextBox.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(319, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 367);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.CreateConfigButton);
            this.Controls.Add(this.InstallButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Prometheus Exporter Installer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
    }
}
