namespace Share_a_Ton
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.clientGroupBox = new System.Windows.Forms.GroupBox();
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.clientNameLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.lanGroupBox = new System.Windows.Forms.GroupBox();
            this.listOfPcs = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.clientGroupBox.SuspendLayout();
            this.lanGroupBox.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.clientGroupBox);
            this.panel2.Controls.Add(this.statusLabel);
            this.panel2.Controls.Add(this.lanGroupBox);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(523, 252);
            this.panel2.TabIndex = 5;
            // 
            // clientGroupBox
            // 
            this.clientGroupBox.Controls.Add(this.ipAddressLabel);
            this.clientGroupBox.Controls.Add(this.clientNameLabel);
            this.clientGroupBox.Controls.Add(this.label3);
            this.clientGroupBox.Controls.Add(this.label1);
            this.clientGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clientGroupBox.Location = new System.Drawing.Point(289, 42);
            this.clientGroupBox.Name = "clientGroupBox";
            this.clientGroupBox.Size = new System.Drawing.Size(222, 104);
            this.clientGroupBox.TabIndex = 5;
            this.clientGroupBox.TabStop = false;
            this.clientGroupBox.Text = "Client info : ";
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ipAddressLabel.Location = new System.Drawing.Point(88, 65);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(0, 15);
            this.ipAddressLabel.TabIndex = 4;
            // 
            // clientNameLabel
            // 
            this.clientNameLabel.AutoSize = true;
            this.clientNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clientNameLabel.Location = new System.Drawing.Point(88, 30);
            this.clientNameLabel.Name = "clientNameLabel";
            this.clientNameLabel.Size = new System.Drawing.Size(0, 15);
            this.clientNameLabel.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(6, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "IP Address : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name : ";
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.statusLabel.Location = new System.Drawing.Point(12, 231);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(477, 28);
            this.statusLabel.TabIndex = 4;
            // 
            // lanGroupBox
            // 
            this.lanGroupBox.Controls.Add(this.listOfPcs);
            this.lanGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lanGroupBox.Location = new System.Drawing.Point(12, 42);
            this.lanGroupBox.Name = "lanGroupBox";
            this.lanGroupBox.Size = new System.Drawing.Size(271, 172);
            this.lanGroupBox.TabIndex = 3;
            this.lanGroupBox.TabStop = false;
            this.lanGroupBox.Text = "LAN ";
            // 
            // listOfPcs
            // 
            this.listOfPcs.AllowDrop = true;
            this.listOfPcs.AutoArrange = false;
            this.listOfPcs.GridLines = true;
            this.listOfPcs.Location = new System.Drawing.Point(3, 22);
            this.listOfPcs.MultiSelect = false;
            this.listOfPcs.Name = "listOfPcs";
            this.listOfPcs.Size = new System.Drawing.Size(262, 144);
            this.listOfPcs.TabIndex = 0;
            this.listOfPcs.UseCompatibleStateImageBehavior = false;
            this.listOfPcs.View = System.Windows.Forms.View.List;
            this.listOfPcs.SelectedIndexChanged += new System.EventHandler(this.listOfPcs_SelectedIndexChanged);
            this.listOfPcs.DragDrop += new System.Windows.Forms.DragEventHandler(this.listOfPcs_DragDrop);
            this.listOfPcs.DragEnter += new System.Windows.Forms.DragEventHandler(this.listOfPcs_DragEnter);
            this.listOfPcs.DragOver += new System.Windows.Forms.DragEventHandler(this.listOfPcs_DragOver);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(24, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(475, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Drag and drop a file from your computer to a computer in your LAN to initiate the" +
    " transfer";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.SystemColors.Menu;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(523, 24);
            this.menu.TabIndex = 4;
            this.menu.Text = "menuStrip1";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.preferencesToolStripMenuItem.Text = "File";
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionToolStripMenuItem.Text = "Options";
            this.optionToolStripMenuItem.Click += new System.EventHandler(this.optionToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 276);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Share-a-Ton";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.panel2.ResumeLayout(false);
            this.clientGroupBox.ResumeLayout(false);
            this.clientGroupBox.PerformLayout();
            this.lanGroupBox.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox lanGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.GroupBox clientGroupBox;
        private System.Windows.Forms.Label ipAddressLabel;
        private System.Windows.Forms.Label clientNameLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listOfPcs;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Label statusLabel;

    }
}

