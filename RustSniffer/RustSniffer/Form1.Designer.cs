namespace RustSniffer {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bootButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.adapterList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whitelistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DoSAPIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearButton = new System.Windows.Forms.Button();
            this.autoPurgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ip,
            this.loc,
            this.isp,
            this.boot,
            this.bootButton});
            this.dataGridView1.Location = new System.Drawing.Point(14, 78);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(718, 254);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // ip
            // 
            this.ip.HeaderText = "IP Address";
            this.ip.Name = "ip";
            this.ip.ReadOnly = true;
            // 
            // loc
            // 
            this.loc.HeaderText = "Location";
            this.loc.Name = "loc";
            this.loc.ReadOnly = true;
            this.loc.Width = 185;
            // 
            // isp
            // 
            this.isp.HeaderText = "ISP";
            this.isp.Name = "isp";
            this.isp.ReadOnly = true;
            this.isp.Width = 200;
            // 
            // boot
            // 
            this.boot.HeaderText = "Boot Status";
            this.boot.Name = "boot";
            this.boot.ReadOnly = true;
            this.boot.Width = 90;
            // 
            // bootButton
            // 
            this.bootButton.HeaderText = "Stop/Start Boot";
            this.bootButton.Name = "bootButton";
            this.bootButton.ReadOnly = true;
            // 
            // adapterList
            // 
            this.adapterList.FormattingEnabled = true;
            this.adapterList.Location = new System.Drawing.Point(65, 37);
            this.adapterList.Name = "adapterList";
            this.adapterList.Size = new System.Drawing.Size(424, 21);
            this.adapterList.TabIndex = 1;
            this.adapterList.SelectedIndexChanged += new System.EventHandler(this.adapterList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Interface";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(495, 35);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(576, 35);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 4;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(747, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whitelistToolStripMenuItem,
            this.DoSAPIToolStripMenuItem,
            this.autoPurgeToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // whitelistToolStripMenuItem
            // 
            this.whitelistToolStripMenuItem.Name = "whitelistToolStripMenuItem";
            this.whitelistToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.whitelistToolStripMenuItem.Text = "IP Whitelist";
            this.whitelistToolStripMenuItem.Click += new System.EventHandler(this.whitelistToolStripMenuItem_Click);
            // 
            // DoSAPIToolStripMenuItem
            // 
            this.DoSAPIToolStripMenuItem.Name = "DoSAPIToolStripMenuItem";
            this.DoSAPIToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DoSAPIToolStripMenuItem.Text = "DoS API";
            this.DoSAPIToolStripMenuItem.Click += new System.EventHandler(this.DoSAPIToolStripMenuItem_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(657, 35);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 6;
            this.clearButton.Text = "Clear All";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // autoPurgeToolStripMenuItem
            // 
            this.autoPurgeToolStripMenuItem.Checked = true;
            this.autoPurgeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoPurgeToolStripMenuItem.Name = "autoPurgeToolStripMenuItem";
            this.autoPurgeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.autoPurgeToolStripMenuItem.Text = "Auto Purge";
            this.autoPurgeToolStripMenuItem.Click += new System.EventHandler(this.autoPurgeToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 350);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.adapterList);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Saint\'s Rusty Resolver";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox adapterList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn loc;
        private System.Windows.Forms.DataGridViewTextBoxColumn isp;
        private System.Windows.Forms.DataGridViewTextBoxColumn boot;
        private System.Windows.Forms.DataGridViewButtonColumn bootButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whitelistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DoSAPIToolStripMenuItem;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ToolStripMenuItem autoPurgeToolStripMenuItem;
    }
}

