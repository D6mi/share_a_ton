﻿namespace Share_a_Ton.Forms
{
    partial class OptionsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.usernameErrorLabel = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.autoOpenDownloadFolderCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.askForDownloadFolderCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.confirmationCheckBox = new System.Windows.Forms.CheckBox();
            this.openDownloadFolder = new System.Windows.Forms.Button();
            this.changeButton = new System.Windows.Forms.Button();
            this.downloadFolderTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.autoFadeOutCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.usernameErrorLabel);
            this.groupBox1.Controls.Add(this.usernameTextBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.openDownloadFolder);
            this.groupBox1.Controls.Add(this.changeButton);
            this.groupBox1.Controls.Add(this.downloadFolderTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 249);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // usernameErrorLabel
            // 
            this.usernameErrorLabel.AutoSize = true;
            this.usernameErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.usernameErrorLabel.Location = new System.Drawing.Point(235, 28);
            this.usernameErrorLabel.Name = "usernameErrorLabel";
            this.usernameErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.usernameErrorLabel.TabIndex = 9;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(103, 25);
            this.usernameTextBox.MaxLength = 10;
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(126, 20);
            this.usernameTextBox.TabIndex = 8;
            this.usernameTextBox.TextChanged += new System.EventHandler(this.usernameTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Name : ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.autoFadeOutCheckBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.autoOpenDownloadFolderCheckBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.askForDownloadFolderCheckBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.confirmationCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(9, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(401, 118);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // autoOpenDownloadFolderCheckBox
            // 
            this.autoOpenDownloadFolderCheckBox.AutoSize = true;
            this.autoOpenDownloadFolderCheckBox.Location = new System.Drawing.Point(205, 67);
            this.autoOpenDownloadFolderCheckBox.Name = "autoOpenDownloadFolderCheckBox";
            this.autoOpenDownloadFolderCheckBox.Size = new System.Drawing.Size(15, 14);
            this.autoOpenDownloadFolderCheckBox.TabIndex = 7;
            this.autoOpenDownloadFolderCheckBox.UseVisualStyleBackColor = true;
            this.autoOpenDownloadFolderCheckBox.CheckedChanged += new System.EventHandler(this.autoOpenDownloadFolderCheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Auto open download folder on transfer :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Always ask to open download folder on transfer completion : ";
            // 
            // askForDownloadFolderCheckBox
            // 
            this.askForDownloadFolderCheckBox.AutoSize = true;
            this.askForDownloadFolderCheckBox.Location = new System.Drawing.Point(305, 43);
            this.askForDownloadFolderCheckBox.Name = "askForDownloadFolderCheckBox";
            this.askForDownloadFolderCheckBox.Size = new System.Drawing.Size(15, 14);
            this.askForDownloadFolderCheckBox.TabIndex = 4;
            this.askForDownloadFolderCheckBox.UseVisualStyleBackColor = true;
            this.askForDownloadFolderCheckBox.CheckedChanged += new System.EventHandler(this.askForDownloadFolderCheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Confirmation needed : ";
            // 
            // confirmationCheckBox
            // 
            this.confirmationCheckBox.AutoSize = true;
            this.confirmationCheckBox.Location = new System.Drawing.Point(125, 15);
            this.confirmationCheckBox.Name = "confirmationCheckBox";
            this.confirmationCheckBox.Size = new System.Drawing.Size(15, 14);
            this.confirmationCheckBox.TabIndex = 3;
            this.confirmationCheckBox.UseVisualStyleBackColor = true;
            this.confirmationCheckBox.CheckedChanged += new System.EventHandler(this.confirmationCheckBox_CheckedChanged);
            // 
            // openDownloadFolder
            // 
            this.openDownloadFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.openDownloadFolder.Location = new System.Drawing.Point(246, 81);
            this.openDownloadFolder.Name = "openDownloadFolder";
            this.openDownloadFolder.Size = new System.Drawing.Size(87, 24);
            this.openDownloadFolder.TabIndex = 5;
            this.openDownloadFolder.Text = "Download Folder";
            this.openDownloadFolder.UseVisualStyleBackColor = true;
            this.openDownloadFolder.Click += new System.EventHandler(this.openDownloadFolder_Click);
            // 
            // changeButton
            // 
            this.changeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.changeButton.Location = new System.Drawing.Point(339, 81);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(71, 24);
            this.changeButton.TabIndex = 4;
            this.changeButton.Text = "Change";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // downloadFolderTextBox
            // 
            this.downloadFolderTextBox.Location = new System.Drawing.Point(103, 55);
            this.downloadFolderTextBox.Name = "downloadFolderTextBox";
            this.downloadFolderTextBox.Size = new System.Drawing.Size(307, 20);
            this.downloadFolderTextBox.TabIndex = 1;
            this.downloadFolderTextBox.TextChanged += new System.EventHandler(this.downloadFolderTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "File directory : ";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(351, 267);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(270, 267);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.ForeColor = System.Drawing.Color.Red;
            this.statusLabel.Location = new System.Drawing.Point(12, 242);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 13);
            this.statusLabel.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(192, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Auto close transfer overview on finish : ";
            // 
            // autoFadeOutCheckBox
            // 
            this.autoFadeOutCheckBox.AutoSize = true;
            this.autoFadeOutCheckBox.Location = new System.Drawing.Point(205, 92);
            this.autoFadeOutCheckBox.Name = "autoFadeOutCheckBox";
            this.autoFadeOutCheckBox.Size = new System.Drawing.Size(15, 14);
            this.autoFadeOutCheckBox.TabIndex = 9;
            this.autoFadeOutCheckBox.UseVisualStyleBackColor = true;
            this.autoFadeOutCheckBox.CheckedChanged += new System.EventHandler(this.fadeOutCheckBox_CheckedChanged);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 302);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preferences";
            this.Shown += new System.EventHandler(this.OptionsForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox downloadFolderTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox confirmationCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.Button openDownloadFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox askForDownloadFolderCheckBox;
        private System.Windows.Forms.CheckBox autoOpenDownloadFolderCheckBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label usernameErrorLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.CheckBox autoFadeOutCheckBox;
        private System.Windows.Forms.Label label6;
    }
}