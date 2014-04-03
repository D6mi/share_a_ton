namespace Share_a_Ton.Forms
{
    partial class TransferView
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.filelengthLabel = new System.Windows.Forms.Label();
            this.senderLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.filenameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.transferProgress = new System.Windows.Forms.ProgressBar();
            this.actionButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.label5);
            this.groupBox.Controls.Add(this.filelengthLabel);
            this.groupBox.Controls.Add(this.senderLabel);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.filenameLabel);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(364, 131);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Transfer info";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "File length : ";
            // 
            // filelengthLabel
            // 
            this.filelengthLabel.AutoSize = true;
            this.filelengthLabel.Location = new System.Drawing.Point(79, 59);
            this.filelengthLabel.Name = "filelengthLabel";
            this.filelengthLabel.Size = new System.Drawing.Size(67, 13);
            this.filelengthLabel.TabIndex = 4;
            this.filelengthLabel.Text = "2313010 Mb";
            // 
            // senderLabel
            // 
            this.senderLabel.AutoSize = true;
            this.senderLabel.Location = new System.Drawing.Point(79, 91);
            this.senderLabel.Name = "senderLabel";
            this.senderLabel.Size = new System.Drawing.Size(47, 13);
            this.senderLabel.TabIndex = 3;
            this.senderLabel.Text = "D6mi-Pc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sender : ";
            // 
            // filenameLabel
            // 
            this.filenameLabel.AutoSize = true;
            this.filenameLabel.Location = new System.Drawing.Point(79, 29);
            this.filenameLabel.Name = "filenameLabel";
            this.filenameLabel.Size = new System.Drawing.Size(59, 13);
            this.filenameLabel.TabIndex = 1;
            this.filenameLabel.Text = "Test Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File : ";
            // 
            // transferProgress
            // 
            this.transferProgress.Location = new System.Drawing.Point(12, 149);
            this.transferProgress.Name = "transferProgress";
            this.transferProgress.Size = new System.Drawing.Size(364, 23);
            this.transferProgress.Step = 1;
            this.transferProgress.TabIndex = 1;
            // 
            // actionButton
            // 
            this.actionButton.AutoSize = true;
            this.actionButton.Location = new System.Drawing.Point(278, 178);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(98, 24);
            this.actionButton.TabIndex = 2;
            this.actionButton.Text = "Cancel transfer";
            this.actionButton.UseVisualStyleBackColor = true;
            this.actionButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 184);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(69, 13);
            this.statusLabel.TabIndex = 3;
            this.statusLabel.Text = "Transfering...";
            // 
            // TransferView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 214);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.actionButton);
            this.Controls.Add(this.transferProgress);
            this.Controls.Add(this.groupBox);
            this.Name = "TransferView";
            this.Text = "Overview";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.ProgressBar transferProgress;
        private System.Windows.Forms.Label senderLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label filenameLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button actionButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label filelengthLabel;
        private System.Windows.Forms.Label statusLabel;
    }
}