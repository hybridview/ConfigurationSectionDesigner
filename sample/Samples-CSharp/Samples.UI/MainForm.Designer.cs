namespace Samples.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RunDemoButton = new System.Windows.Forms.Button();
            this.ConsoleTextBox = new System.Windows.Forms.TextBox();
            this.XmlViewTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.selectedSampleInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.selectedSampleInfoTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.enableVerboseOutputCheckBox = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.enablePreloadConfigFileCheckBox = new System.Windows.Forms.CheckBox();
            this.sampleGroupsListBox = new System.Windows.Forms.ListBox();
            this.sampleGroupsConfigBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.samplesListBox = new System.Windows.Forms.ListBox();
            this.samplesConfigBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.clearConsoleLinkLabel = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.selectedSampleInfoGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sampleGroupsConfigBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.samplesConfigBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Console";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "->";
            // 
            // RunDemoButton
            // 
            this.RunDemoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RunDemoButton.Location = new System.Drawing.Point(777, 118);
            this.RunDemoButton.Name = "RunDemoButton";
            this.RunDemoButton.Size = new System.Drawing.Size(183, 54);
            this.RunDemoButton.TabIndex = 11;
            this.RunDemoButton.Text = "Run Selected Sample";
            this.RunDemoButton.UseVisualStyleBackColor = true;
            this.RunDemoButton.Click += new System.EventHandler(this.RunDemoButton_Click);
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ConsoleTextBox.Location = new System.Drawing.Point(6, 21);
            this.ConsoleTextBox.Multiline = true;
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleTextBox.Size = new System.Drawing.Size(475, 450);
            this.ConsoleTextBox.TabIndex = 8;
            // 
            // XmlViewTextBox
            // 
            this.XmlViewTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.XmlViewTextBox.Location = new System.Drawing.Point(487, 21);
            this.XmlViewTextBox.Multiline = true;
            this.XmlViewTextBox.Name = "XmlViewTextBox";
            this.XmlViewTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.XmlViewTextBox.Size = new System.Drawing.Size(468, 450);
            this.XmlViewTextBox.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "XML Result";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.selectedSampleInfoGroupBox);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.sampleGroupsListBox);
            this.panel1.Controls.Add(this.samplesListBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.RunDemoButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 498);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 172);
            this.panel1.TabIndex = 16;
            // 
            // selectedSampleInfoGroupBox
            // 
            this.selectedSampleInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedSampleInfoGroupBox.Controls.Add(this.selectedSampleInfoTextBox);
            this.selectedSampleInfoGroupBox.Location = new System.Drawing.Point(443, 3);
            this.selectedSampleInfoGroupBox.Name = "selectedSampleInfoGroupBox";
            this.selectedSampleInfoGroupBox.Size = new System.Drawing.Size(262, 109);
            this.selectedSampleInfoGroupBox.TabIndex = 18;
            this.selectedSampleInfoGroupBox.TabStop = false;
            this.selectedSampleInfoGroupBox.Text = "Selected Sample";
            // 
            // selectedSampleInfoTextBox
            // 
            this.selectedSampleInfoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedSampleInfoTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.selectedSampleInfoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.selectedSampleInfoTextBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedSampleInfoTextBox.Location = new System.Drawing.Point(6, 19);
            this.selectedSampleInfoTextBox.Multiline = true;
            this.selectedSampleInfoTextBox.Name = "selectedSampleInfoTextBox";
            this.selectedSampleInfoTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.selectedSampleInfoTextBox.Size = new System.Drawing.Size(250, 84);
            this.selectedSampleInfoTextBox.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.enableVerboseOutputCheckBox);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.enablePreloadConfigFileCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(711, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 106);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // enableVerboseOutputCheckBox
            // 
            this.enableVerboseOutputCheckBox.AccessibleDescription = "";
            this.enableVerboseOutputCheckBox.AutoSize = true;
            this.enableVerboseOutputCheckBox.Location = new System.Drawing.Point(6, 65);
            this.enableVerboseOutputCheckBox.Name = "enableVerboseOutputCheckBox";
            this.enableVerboseOutputCheckBox.Size = new System.Drawing.Size(153, 17);
            this.enableVerboseOutputCheckBox.TabIndex = 2;
            this.enableVerboseOutputCheckBox.Text = "VERBOSE console output.";
            this.enableVerboseOutputCheckBox.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AccessibleDescription = "";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 42);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(162, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Enable app config redirector.";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // enablePreloadConfigFileCheckBox
            // 
            this.enablePreloadConfigFileCheckBox.AccessibleDescription = "";
            this.enablePreloadConfigFileCheckBox.AutoSize = true;
            this.enablePreloadConfigFileCheckBox.Location = new System.Drawing.Point(6, 19);
            this.enablePreloadConfigFileCheckBox.Name = "enablePreloadConfigFileCheckBox";
            this.enablePreloadConfigFileCheckBox.Size = new System.Drawing.Size(208, 17);
            this.enablePreloadConfigFileCheckBox.TabIndex = 0;
            this.enablePreloadConfigFileCheckBox.Text = "Enable configuration manager preload.";
            this.enablePreloadConfigFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // sampleGroupsListBox
            // 
            this.sampleGroupsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sampleGroupsListBox.DataSource = this.sampleGroupsConfigBindingSource;
            this.sampleGroupsListBox.DisplayMember = "label";
            this.sampleGroupsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sampleGroupsListBox.FormattingEnabled = true;
            this.sampleGroupsListBox.ItemHeight = 15;
            this.sampleGroupsListBox.Location = new System.Drawing.Point(56, 3);
            this.sampleGroupsListBox.Name = "sampleGroupsListBox";
            this.sampleGroupsListBox.Size = new System.Drawing.Size(120, 109);
            this.sampleGroupsListBox.TabIndex = 16;
            this.sampleGroupsListBox.SelectedIndexChanged += new System.EventHandler(this.sampleGroupsListBox_SelectedIndexChanged);
            // 
            // sampleGroupsConfigBindingSource
            // 
            this.sampleGroupsConfigBindingSource.DataSource = typeof(Samples.UI.ApplicationConfig.SampleGroup);
            // 
            // samplesListBox
            // 
            this.samplesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.samplesListBox.DataSource = this.samplesConfigBindingSource;
            this.samplesListBox.DisplayMember = "label";
            this.samplesListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.samplesListBox.FormattingEnabled = true;
            this.samplesListBox.ItemHeight = 15;
            this.samplesListBox.Location = new System.Drawing.Point(204, 3);
            this.samplesListBox.Name = "samplesListBox";
            this.samplesListBox.Size = new System.Drawing.Size(233, 109);
            this.samplesListBox.TabIndex = 15;
            this.samplesListBox.SelectedIndexChanged += new System.EventHandler(this.samplesListBox_SelectedIndexChanged);
            // 
            // samplesConfigBindingSource
            // 
            this.samplesConfigBindingSource.DataSource = typeof(Samples.UI.ApplicationConfig.Sample);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Samples:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.clearConsoleLinkLabel);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.XmlViewTextBox);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.ConsoleTextBox);
            this.panel2.Location = new System.Drawing.Point(12, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(960, 471);
            this.panel2.TabIndex = 17;
            // 
            // clearConsoleLinkLabel
            // 
            this.clearConsoleLinkLabel.AutoSize = true;
            this.clearConsoleLinkLabel.Location = new System.Drawing.Point(54, 5);
            this.clearConsoleLinkLabel.Name = "clearConsoleLinkLabel";
            this.clearConsoleLinkLabel.Size = new System.Drawing.Size(30, 13);
            this.clearConsoleLinkLabel.TabIndex = 18;
            this.clearConsoleLinkLabel.TabStop = true;
            this.clearConsoleLinkLabel.Text = "clear";
            this.clearConsoleLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.clearConsoleLinkLabel_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 682);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(1000, 300);
            this.Name = "MainForm";
            this.Text = "Sample Runner";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.selectedSampleInfoGroupBox.ResumeLayout(false);
            this.selectedSampleInfoGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sampleGroupsConfigBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.samplesConfigBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RunDemoButton;
        private System.Windows.Forms.TextBox ConsoleTextBox;
        private System.Windows.Forms.TextBox XmlViewTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox samplesListBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel clearConsoleLinkLabel;
        private System.Windows.Forms.BindingSource samplesConfigBindingSource;
        private System.Windows.Forms.BindingSource sampleGroupsConfigBindingSource;
        private System.Windows.Forms.ListBox sampleGroupsListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox enablePreloadConfigFileCheckBox;
        private System.Windows.Forms.GroupBox selectedSampleInfoGroupBox;
        private System.Windows.Forms.TextBox selectedSampleInfoTextBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox enableVerboseOutputCheckBox;

    }
}

