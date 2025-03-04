namespace Github_Auto_Backup
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            RepoLocation_Text = new TextBox();
            RepoLocation_Label = new Label();
            OpenFileExpl_Button = new Button();
            BackupInterval_Combo = new ComboBox();
            BackupInterval_Label = new Label();
            BackupTime_Picker = new DateTimePicker();
            BackupTime_Label = new Label();
            OpenLog_Button = new Button();
            Close_Button = new Button();
            BackUp_NotifyIcon = new NotifyIcon(components);
            BackupSysTray_Menu = new ContextMenuStrip(components);
            ToolStripOpenForm_MenuItem = new ToolStripMenuItem();
            ToolStripRunBackup_MenuItem = new ToolStripMenuItem();
            LastBackup_MenuItem = new ToolStripMenuItem();
            LastBackup_toolStrip_TextBox = new ToolStripTextBox();
            NextBackup_MenuItem = new ToolStripMenuItem();
            NextBackup_toolStrip_TextBox = new ToolStripTextBox();
            ToolStripExit_MenuItem = new ToolStripMenuItem();
            pictureBox1 = new PictureBox();
            RunBack_Button = new Button();
            Backup_Timer = new System.Windows.Forms.Timer(components);
            LastBackup_Text = new TextBox();
            LastBackup_Label = new Label();
            NextBackup_Text = new TextBox();
            NextBackup_Label = new Label();
            BackupStatus_Text = new TextBox();
            BackupStatus_Label = new Label();
            panel1 = new Panel();
            nextBackupToolStripMenuItem = new ToolStripMenuItem();
            toolStripTextBox2 = new ToolStripTextBox();
            Help_button = new Button();
            BackupSysTray_Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // RepoLocation_Text
            // 
            RepoLocation_Text.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            RepoLocation_Text.Location = new Point(23, 34);
            RepoLocation_Text.Name = "RepoLocation_Text";
            RepoLocation_Text.Size = new Size(257, 23);
            RepoLocation_Text.TabIndex = 0;
            RepoLocation_Text.TextChanged += RepoLocation_Text_TextChanged;
            // 
            // RepoLocation_Label
            // 
            RepoLocation_Label.AutoSize = true;
            RepoLocation_Label.ForeColor = Color.White;
            RepoLocation_Label.Location = new Point(23, 16);
            RepoLocation_Label.Name = "RepoLocation_Label";
            RepoLocation_Label.Size = new Size(200, 15);
            RepoLocation_Label.TabIndex = 1;
            RepoLocation_Label.Text = "Folder where Repositories are stored:";
            // 
            // OpenFileExpl_Button
            // 
            OpenFileExpl_Button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            OpenFileExpl_Button.Location = new Point(286, 33);
            OpenFileExpl_Button.Name = "OpenFileExpl_Button";
            OpenFileExpl_Button.Size = new Size(26, 23);
            OpenFileExpl_Button.TabIndex = 2;
            OpenFileExpl_Button.Text = "...";
            OpenFileExpl_Button.UseVisualStyleBackColor = true;
            OpenFileExpl_Button.Click += OpenFileExpl_Button_Click;
            // 
            // BackupInterval_Combo
            // 
            BackupInterval_Combo.DropDownStyle = ComboBoxStyle.DropDownList;
            BackupInterval_Combo.FormattingEnabled = true;
            BackupInterval_Combo.Items.AddRange(new object[] { "Daily (Default)", "Week Days", "Weekly", "Monthly" });
            BackupInterval_Combo.Location = new Point(23, 93);
            BackupInterval_Combo.Name = "BackupInterval_Combo";
            BackupInterval_Combo.Size = new Size(135, 23);
            BackupInterval_Combo.TabIndex = 3;
            BackupInterval_Combo.SelectedIndexChanged += BackupInterval_Combo_SelectedIndexChanged;
            // 
            // BackupInterval_Label
            // 
            BackupInterval_Label.AutoSize = true;
            BackupInterval_Label.ForeColor = Color.White;
            BackupInterval_Label.Location = new Point(23, 75);
            BackupInterval_Label.Name = "BackupInterval_Label";
            BackupInterval_Label.Size = new Size(88, 15);
            BackupInterval_Label.TabIndex = 4;
            BackupInterval_Label.Text = "Backup Interval";
            // 
            // BackupTime_Picker
            // 
            BackupTime_Picker.Format = DateTimePickerFormat.Time;
            BackupTime_Picker.Location = new Point(23, 155);
            BackupTime_Picker.Name = "BackupTime_Picker";
            BackupTime_Picker.ShowUpDown = true;
            BackupTime_Picker.Size = new Size(135, 23);
            BackupTime_Picker.TabIndex = 5;
            BackupTime_Picker.ValueChanged += BackupTime_Picker_ValueChanged;
            // 
            // BackupTime_Label
            // 
            BackupTime_Label.AutoSize = true;
            BackupTime_Label.ForeColor = Color.White;
            BackupTime_Label.Location = new Point(23, 137);
            BackupTime_Label.Name = "BackupTime_Label";
            BackupTime_Label.Size = new Size(127, 15);
            BackupTime_Label.TabIndex = 6;
            BackupTime_Label.Text = "Preferred Backup Time";
            // 
            // OpenLog_Button
            // 
            OpenLog_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            OpenLog_Button.Location = new Point(23, 345);
            OpenLog_Button.Name = "OpenLog_Button";
            OpenLog_Button.Size = new Size(97, 23);
            OpenLog_Button.TabIndex = 7;
            OpenLog_Button.Text = "Open Log File";
            OpenLog_Button.UseVisualStyleBackColor = true;
            OpenLog_Button.Click += OpenLog_Button_Click;
            // 
            // Close_Button
            // 
            Close_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Close_Button.Location = new Point(207, 345);
            Close_Button.Name = "Close_Button";
            Close_Button.Size = new Size(97, 23);
            Close_Button.TabIndex = 8;
            Close_Button.Text = "Close";
            Close_Button.UseVisualStyleBackColor = true;
            Close_Button.Click += Close_Button_Click;
            // 
            // BackUp_NotifyIcon
            // 
            BackUp_NotifyIcon.ContextMenuStrip = BackupSysTray_Menu;
            BackUp_NotifyIcon.Icon = (Icon)resources.GetObject("BackUp_NotifyIcon.Icon");
            BackUp_NotifyIcon.Text = "Github Auto Backup";
            BackUp_NotifyIcon.Visible = true;
            BackUp_NotifyIcon.DoubleClick += BackUp_NotifyIcon_DoubleClick;
            // 
            // BackupSysTray_Menu
            // 
            BackupSysTray_Menu.ImageScalingSize = new Size(32, 32);
            BackupSysTray_Menu.Items.AddRange(new ToolStripItem[] { ToolStripOpenForm_MenuItem, ToolStripRunBackup_MenuItem, LastBackup_MenuItem, NextBackup_MenuItem, ToolStripExit_MenuItem });
            BackupSysTray_Menu.Name = "BackupSysTray_Menu";
            BackupSysTray_Menu.Size = new Size(166, 114);
            // 
            // ToolStripOpenForm_MenuItem
            // 
            ToolStripOpenForm_MenuItem.Name = "ToolStripOpenForm_MenuItem";
            ToolStripOpenForm_MenuItem.Size = new Size(165, 22);
            ToolStripOpenForm_MenuItem.Text = "Settings";
            ToolStripOpenForm_MenuItem.Click += ToolStripOpenForm_MenuItem_Click;
            // 
            // ToolStripRunBackup_MenuItem
            // 
            ToolStripRunBackup_MenuItem.Name = "ToolStripRunBackup_MenuItem";
            ToolStripRunBackup_MenuItem.Size = new Size(165, 22);
            ToolStripRunBackup_MenuItem.Text = "Run Backup Now";
            ToolStripRunBackup_MenuItem.Click += ToolStripRunBackup_MenuItem_Click;
            // 
            // LastBackup_MenuItem
            // 
            LastBackup_MenuItem.DropDownItems.AddRange(new ToolStripItem[] { LastBackup_toolStrip_TextBox });
            LastBackup_MenuItem.Name = "LastBackup_MenuItem";
            LastBackup_MenuItem.Size = new Size(165, 22);
            LastBackup_MenuItem.Text = "Last Backup";
            // 
            // LastBackup_toolStrip_TextBox
            // 
            LastBackup_toolStrip_TextBox.Name = "LastBackup_toolStrip_TextBox";
            LastBackup_toolStrip_TextBox.ReadOnly = true;
            LastBackup_toolStrip_TextBox.Size = new Size(100, 23);
            LastBackup_toolStrip_TextBox.Click += LastBackup_toolStrip_TextBox_Click;
            // 
            // NextBackup_MenuItem
            // 
            NextBackup_MenuItem.DropDownItems.AddRange(new ToolStripItem[] { NextBackup_toolStrip_TextBox });
            NextBackup_MenuItem.Name = "NextBackup_MenuItem";
            NextBackup_MenuItem.Size = new Size(165, 22);
            NextBackup_MenuItem.Text = "Next Backup";
            // 
            // NextBackup_toolStrip_TextBox
            // 
            NextBackup_toolStrip_TextBox.Name = "NextBackup_toolStrip_TextBox";
            NextBackup_toolStrip_TextBox.ReadOnly = true;
            NextBackup_toolStrip_TextBox.Size = new Size(100, 23);
            NextBackup_toolStrip_TextBox.Click += NextBackup_toolStrip_TextBox_Click;
            // 
            // ToolStripExit_MenuItem
            // 
            ToolStripExit_MenuItem.Name = "ToolStripExit_MenuItem";
            ToolStripExit_MenuItem.Size = new Size(165, 22);
            ToolStripExit_MenuItem.Text = "Exit";
            ToolStripExit_MenuItem.Click += ToolStripExit_MenuItem_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.GITHUB_AUTO_BACKUP_Logo1;
            pictureBox1.Location = new Point(180, 75);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(124, 103);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // RunBack_Button
            // 
            RunBack_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            RunBack_Button.Location = new Point(23, 271);
            RunBack_Button.Name = "RunBack_Button";
            RunBack_Button.Size = new Size(135, 52);
            RunBack_Button.TabIndex = 10;
            RunBack_Button.Text = "Run Backup Now";
            RunBack_Button.UseVisualStyleBackColor = true;
            RunBack_Button.Click += RunBack_Button_Click;
            // 
            // Backup_Timer
            // 
            Backup_Timer.Tick += Backup_Timer_Tick;
            // 
            // LastBackup_Text
            // 
            LastBackup_Text.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LastBackup_Text.Location = new Point(23, 221);
            LastBackup_Text.Name = "LastBackup_Text";
            LastBackup_Text.ReadOnly = true;
            LastBackup_Text.Size = new Size(135, 23);
            LastBackup_Text.TabIndex = 11;
            LastBackup_Text.TextChanged += LastBackup_Text_TextChanged;
            // 
            // LastBackup_Label
            // 
            LastBackup_Label.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LastBackup_Label.AutoSize = true;
            LastBackup_Label.ForeColor = Color.White;
            LastBackup_Label.Location = new Point(23, 203);
            LastBackup_Label.Name = "LastBackup_Label";
            LastBackup_Label.Size = new Size(70, 15);
            LastBackup_Label.TabIndex = 12;
            LastBackup_Label.Text = "Last Backup";
            // 
            // NextBackup_Text
            // 
            NextBackup_Text.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            NextBackup_Text.Location = new Point(180, 221);
            NextBackup_Text.Name = "NextBackup_Text";
            NextBackup_Text.ReadOnly = true;
            NextBackup_Text.Size = new Size(124, 23);
            NextBackup_Text.TabIndex = 13;
            // 
            // NextBackup_Label
            // 
            NextBackup_Label.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            NextBackup_Label.AutoSize = true;
            NextBackup_Label.ForeColor = Color.White;
            NextBackup_Label.Location = new Point(180, 203);
            NextBackup_Label.Name = "NextBackup_Label";
            NextBackup_Label.Size = new Size(73, 15);
            NextBackup_Label.TabIndex = 14;
            NextBackup_Label.Text = "Next Backup";
            // 
            // BackupStatus_Text
            // 
            BackupStatus_Text.Location = new Point(3, 18);
            BackupStatus_Text.Name = "BackupStatus_Text";
            BackupStatus_Text.ReadOnly = true;
            BackupStatus_Text.Size = new Size(124, 23);
            BackupStatus_Text.TabIndex = 15;
            // 
            // BackupStatus_Label
            // 
            BackupStatus_Label.AutoSize = true;
            BackupStatus_Label.ForeColor = Color.White;
            BackupStatus_Label.Location = new Point(3, 0);
            BackupStatus_Label.Name = "BackupStatus_Label";
            BackupStatus_Label.Size = new Size(105, 15);
            BackupStatus_Label.TabIndex = 16;
            BackupStatus_Label.Text = "Last Backup Status";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(BackupStatus_Label);
            panel1.Controls.Add(BackupStatus_Text);
            panel1.Location = new Point(176, 270);
            panel1.Name = "panel1";
            panel1.Size = new Size(135, 52);
            panel1.TabIndex = 17;
            // 
            // nextBackupToolStripMenuItem
            // 
            nextBackupToolStripMenuItem.Name = "nextBackupToolStripMenuItem";
            nextBackupToolStripMenuItem.Size = new Size(180, 22);
            nextBackupToolStripMenuItem.Text = "Next Backup";
            // 
            // toolStripTextBox2
            // 
            toolStripTextBox2.Name = "toolStripTextBox2";
            toolStripTextBox2.Size = new Size(100, 23);
            // 
            // Help_button
            // 
            Help_button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Help_button.Location = new Point(126, 345);
            Help_button.Name = "Help_button";
            Help_button.Size = new Size(32, 23);
            Help_button.TabIndex = 18;
            Help_button.Text = "?";
            Help_button.UseVisualStyleBackColor = true;
            Help_button.Click += Help_button_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.SlateGray;
            ClientSize = new Size(324, 380);
            Controls.Add(Help_button);
            Controls.Add(RunBack_Button);
            Controls.Add(panel1);
            Controls.Add(NextBackup_Label);
            Controls.Add(NextBackup_Text);
            Controls.Add(LastBackup_Label);
            Controls.Add(LastBackup_Text);
            Controls.Add(pictureBox1);
            Controls.Add(Close_Button);
            Controls.Add(OpenLog_Button);
            Controls.Add(BackupTime_Label);
            Controls.Add(BackupTime_Picker);
            Controls.Add(BackupInterval_Label);
            Controls.Add(BackupInterval_Combo);
            Controls.Add(OpenFileExpl_Button);
            Controls.Add(RepoLocation_Label);
            Controls.Add(RepoLocation_Text);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(335, 403);
            Name = "Form1";
            Text = "Github Auto Backup";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            BackupSysTray_Menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox RepoLocation_Text;
        private Label RepoLocation_Label;
        private Button OpenFileExpl_Button;
        private ComboBox BackupInterval_Combo;
        private Label BackupInterval_Label;
        private DateTimePicker BackupTime_Picker;
        private Label BackupTime_Label;
        private Button OpenLog_Button;
        private Button Close_Button;
        private NotifyIcon BackUp_NotifyIcon;
        private PictureBox pictureBox1;
        private Button RunBack_Button;
        private System.Windows.Forms.Timer Backup_Timer;
        private TextBox LastBackup_Text;
        private Label LastBackup_Label;
        private TextBox NextBackup_Text;
        private Label NextBackup_Label;
        private TextBox BackupStatus_Text;
        private Label BackupStatus_Label;
        private Panel panel1;
        private ContextMenuStrip BackupSysTray_Menu;
        private ToolStripMenuItem ToolStripRunBackup_MenuItem;
        private ToolStripMenuItem LastBackup_MenuItem;
        private ToolStripTextBox LastBackup_toolStrip_TextBox;
        private ToolStripMenuItem NextBackup_MenuItem;
        private ToolStripTextBox NextBackup_toolStrip_TextBox;
        private ToolStripMenuItem nextBackupToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox2;
        private ToolStripMenuItem ToolStripOpenForm_MenuItem;
        private ToolStripMenuItem ToolStripExit_MenuItem;
        private Button Help_button;
    }
}
