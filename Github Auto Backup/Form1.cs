namespace Github_Auto_Backup
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, int> backupIntervalMapping = new()
        {
            { "Daily (Default)", 1440 }, // 1440 minutes in a day
            { "Week Days", 1440 }, // Assuming daily for weekdays
            { "Weekly", 10080 }, // 10080 minutes in a week
            { "Monthly", 43200 } // Approximate 43200 minutes in a month (30 days)
        };

        private bool isFormLoading = false;
        private static Form1? form1Instance;

        private static class BackupStatusConstants
        {
            public const string Running = "Running";
            public const string CompletedSuccess = "Completed: Success";
            public const string CompletedError = "Completed: ERROR";
            public const string CompletedWarning = "Completed: Warning";
            public const string NotRunning = "Not Running";
        }

        public Form1()
        {
            InitializeComponent();
            BackupInterval_Combo.DropDownStyle = ComboBoxStyle.DropDownList;

            // Initialize variables
            RepoLocation_Text.Text = RepoLocation;
            BackupInterval_Combo.Text = BackupInterval;
            BackupTime_Picker.Text = BackupTime;
            LastBackup_Text.Text = LastBackup;
            NextBackup_Text.Text = NextBackup;

            // Start the timer
            Backup_Timer.Start();

            // Check if it's the first run
            if (Properties.Settings.Default.IsFirstRun)
            {
                RunFirstTimeSetup();
                Properties.Settings.Default.IsFirstRun = false;
                Properties.Settings.Default.Save();
            }

            // Check if BackupInterval is null or empty and set a default value if it is
            if (string.IsNullOrEmpty(BackupInterval))
            {
                BackupInterval = "Daily (Default)";
                Properties.Settings.Default.BackupInterval = BackupInterval;
                Properties.Settings.Default.Save();
            }

            // Check if BackupStatus is null and set a default value if it is
            if (string.IsNullOrEmpty(BackupStatus))
            {
                BackupStatus = BackupStatusConstants.NotRunning;
            }
            BackupStatus_Text.Text = BackupStatus;
            UpdateBackupStatus(BackupStatus); // Call UpdateBackupStatus with BackupStatus

            if (!string.IsNullOrEmpty(BackupInterval))
            {
                int Backup_Index = BackupInterval_Combo.Items.IndexOf(BackupInterval);
                if (Backup_Index != -1)
                {
                    BackupInterval_Combo.SelectedIndex = Backup_Index;
                }
            }
            else if (BackupInterval_Combo.Items.Count > 0)
            {
                BackupInterval_Combo.SelectedIndex = 0;
            }

            this.Load += Form1_Load; // Ensure the Load event is properly hooked
        }
        public static void ShowForm()
        {
            if (form1Instance == null || form1Instance.IsDisposed)
            {
                form1Instance = new Form1();
                if (Properties.Settings.Default.FormVisibility == "Hidden")
                {
                    form1Instance.Hide();
                }
                else
                {
                    form1Instance.Show();
                }
            }
            else
            {
                if (form1Instance.Visible)
                {
                    form1Instance.BringToFront();
                }
                else
                {
                    form1Instance.Show();
                }
            }
        }

        private void LogAction(string message, bool addNewLines = false, bool includeDateTime = true)
        {
            try
            {
                // Ensure the log directory exists
                string? logDirectory = Path.GetDirectoryName(logFilePath);
                if (logDirectory != null && !Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                using var writer = new StreamWriter(logFilePath, true);
                if (addNewLines)
                {
                    writer.WriteLine();
                }

                if (includeDateTime)
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
                else
                {
                    writer.WriteLine(message);
                }

                if (addNewLines)
                {
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while logging: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateBackupStatus(string status)
        {
            if (BackupStatus_Text.InvokeRequired)
            {
                BackupStatus_Text.Invoke(new Action<string>(UpdateBackupStatus), status);
                return;
            }

            BackupStatus = status;
            switch (status)
            {
                case BackupStatusConstants.Running:
                    BackupStatus_Text.BackColor = System.Drawing.Color.Gray;
                    break;
                case BackupStatusConstants.CompletedSuccess:
                    BackupStatus_Text.BackColor = System.Drawing.Color.Green;
                    break;
                case BackupStatusConstants.CompletedError:
                    BackupStatus_Text.BackColor = System.Drawing.Color.Red;
                    break;
                case BackupStatusConstants.CompletedWarning:
                    BackupStatus_Text.BackColor = System.Drawing.Color.Yellow;
                    break;
                case BackupStatusConstants.NotRunning:
                    BackupStatus_Text.BackColor = System.Drawing.Color.Gray;
                    break;
            }
            BackupStatus_Text.Text = BackupStatus;
        }

        public string logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Github Auto Backup", "Github Auto Backup.log");
        public string? RepoLocation = Properties.Settings.Default.RepoLocation;
        public string? BackupInterval = Properties.Settings.Default.BackupInterval;
        public string? LastBackup = Properties.Settings.Default.LastBackup;
        public string? NextBackup = Properties.Settings.Default.NextBackup;
        public string? BackupTime = Properties.Settings.Default.BackupTime;
        public string? BackupStatus = Properties.Settings.Default.BackupStatus;

        private void Form1_Load(object? sender, EventArgs e)
        {
            isFormLoading = true;

            // Apply the saved form visibility state
            if (Properties.Settings.Default.FormVisibility == "Hidden")
            {
                this.Hide();
            }
            else
            {
                this.Show();
            }

            isFormLoading = false;
        }
        private void RunFirstTimeSetup()
        {
            // Set the default backup interval
            BackupInterval = "Daily (Default)";
            Properties.Settings.Default.BackupInterval = BackupInterval;
            Properties.Settings.Default.FormVisibility = "Hidden";
            BackupTime = BackupTime_Picker.Value.ToString();
            Properties.Settings.Default.Save();
        }

        private void OpenFileExpl_Button_Click(object sender, EventArgs e)
        {
            using FolderBrowserDialog folderBrowserDialog = new();
            folderBrowserDialog.Description = "Select a Folder";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                RepoLocation = folderBrowserDialog.SelectedPath;
                RepoLocation_Text.Text = RepoLocation;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Cancel the default close action
                e.Cancel = true;

                // Hide the form instead of closing it
                Properties.Settings.Default.FormVisibility = "Hidden";
                Properties.Settings.Default.Save();
                this.Hide();
            }

            SaveSettings();
            //Properties.Settings.Default.FormVisibility = this.Visible ? "Visible" : "Hidden";
            //Properties.Settings.Default.Save();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.RepoLocation = RepoLocation;
            Properties.Settings.Default.BackupInterval = BackupInterval;
            Properties.Settings.Default.BackupTime = BackupTime;
            Properties.Settings.Default.NextBackup = NextBackup;
            Properties.Settings.Default.LastBackup = LastBackup;
            Properties.Settings.Default.BackupStatus = BackupStatus;
            Properties.Settings.Default.Save();
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            // Hide the form instead of closing it
            this.Hide();
            Properties.Settings.Default.FormVisibility = "Hidden";
            Properties.Settings.Default.Save();
        }

        private void RepoLocation_Text_TextChanged(object sender, EventArgs e)
        {
            RepoLocation = RepoLocation_Text.Text;
        }

        private void BackupInterval_Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BackupInterval = BackupInterval_Combo.Text;
            if (!string.IsNullOrEmpty(BackupInterval) && backupIntervalMapping.TryGetValue(BackupInterval, out int intervalMinutes))
            {
                NextBackup = DateTime.Now.AddMinutes(intervalMinutes).ToString();
                if (!isFormLoading)
                {
                    LogAction($"Backup interval set to {BackupInterval}");
                }
            }
            else
            {
                NextBackup = "Unknown";
                LogAction("ERROR: Invalid backup interval selected.");
            }
        }

        private async void RunBack_Button_Click(object sender, EventArgs e)
        {
            await RunBackupAsync();
        }

        private async Task RunBackupAsync()
        {
            Backup_Timer.Stop();
            RunBack_Button.Enabled = false;
            await Task.Run(async () =>
            {
                try
                {
                    LogAction("************************************************************", false, false);
                    LogAction("New backup started");
                    UpdateBackupStatus(BackupStatusConstants.Running);

                    if (!string.IsNullOrEmpty(RepoLocation))
                    {
                        if (IsGitRepository(RepoLocation))
                        {
                            await ProcessRepositoryAsync(RepoLocation);
                        }
                        else
                        {
                            foreach (string directory in Directory.GetDirectories(RepoLocation))
                            {
                                if (IsGitRepository(directory))
                                {
                                    await ProcessRepositoryAsync(directory);
                                }
                            }
                        }
                    }
                    else
                    {
                        LogAction("Repository location is not set.", false, true);
                        UpdateBackupStatus(BackupStatusConstants.CompletedError);
                    }

                    LogAction("Backup Completed", false, true);
                    LogAction("************************************************************", false, false);
                    LastBackup = DateTime.Now.ToString();

                    if (!string.IsNullOrEmpty(BackupInterval) && backupIntervalMapping.TryGetValue(BackupInterval, out int intervalMinutes))
                    {
                        if (!string.IsNullOrEmpty(BackupTime))
                        {
                            DateTime nextBackupTime = DateTime.Parse(BackupTime);
                            DateTime now = DateTime.Now;
                            nextBackupTime = new DateTime(now.Year, now.Month, now.Day, nextBackupTime.Hour, nextBackupTime.Minute, nextBackupTime.Second);

                            if (nextBackupTime <= now)
                            {
                                nextBackupTime = nextBackupTime.AddMinutes(intervalMinutes);
                            }

                            NextBackup = nextBackupTime.ToString();
                        }
                        else
                        {
                            NextBackup = "Unknown";
                        }
                    }
                    else
                    {
                        NextBackup = "Unknown";
                    }

                }
                catch (Exception ex)
                {
                    LogAction($"Error: {ex.Message}", false, true);
                    UpdateBackupStatus(BackupStatusConstants.CompletedError);
                }
                if (BackupStatus == BackupStatusConstants.CompletedWarning)
                {
                    LogAction("Backup completed with warnings. See above for further details", false, true);
                    UpdateBackupStatus(BackupStatus);
                }
                else
                {
                    UpdateBackupStatus(BackupStatusConstants.CompletedSuccess);
                }
                LogAction($"Next backup scheduled for {NextBackup}", false, true);
            });
            RunBack_Button.Enabled = true;
            Backup_Timer.Start();
        }

        private async Task ProcessRepositoryAsync(string repoPath)
        {
            string repoName = new DirectoryInfo(repoPath).Name;
            LogAction($"Processing repository: {repoName}");

            string currentBranch = await RunGitCommandAsync("rev-parse --abbrev-ref HEAD", repoPath);
            if (currentBranch == "main" || currentBranch == "master")
            {
                LogAction($"Warning: Skipping repository \"{repoName}\" because it is set to the \"{currentBranch}\" branch.", false, true);
                BackupStatus = BackupStatusConstants.CompletedWarning;
                return;
            }

            if (!await RemoteBranchExistsAsync(repoPath, currentBranch))
            {
                DialogResult result = MessageBox.Show($"Branch \"{currentBranch}\" in \"{repoName}\" does not exist in the remote repository. Do you want to create it?", "Github Auto Backup: Branch Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    await RunGitCommandAsync($"push -u origin {currentBranch}", repoPath);
                    LogAction($"Branch \"{currentBranch}\" in Repository \"{repoName}\" created in the remote repository.");
                }
                else
                {
                    LogAction($"Branch \"{currentBranch}\" in Repository \"{repoName}\" was NOT created in the remote repository.");
                    return;
                }
            }

            await RunGitCommandAsync("add .", repoPath);

            // Check if there are changes to commit
            string statusOutput = await RunGitCommandAsync("status --porcelain", repoPath);
            bool hasChanges = !string.IsNullOrWhiteSpace(statusOutput);
            if (hasChanges)
            {
                await RunGitCommandAsync("commit -m \"Auto Backup of working Github repositories\"", repoPath);
                LogAction("Changes committed.");
            }
            else
            {
                LogAction("No changes to commit.");
            }

            if (hasChanges)
            {
                string remoteUrl = await RunGitCommandAsync("config --get remote.origin.url", repoPath);
                string upstreamBranch = await RunGitCommandAsync("rev-parse --abbrev-ref --symbolic-full-name @{u}", repoPath);

                if (!string.IsNullOrWhiteSpace(remoteUrl) && !string.IsNullOrWhiteSpace(upstreamBranch))
                {
                    LogAction($"Pushing repository: {repoName} on branch {currentBranch}");
                    string pushOutput = await RunGitCommandAsync("push", repoPath);
                    if (!string.IsNullOrWhiteSpace(pushOutput))
                    {
                        LogAction($"Push output: {pushOutput}");
                    }
                }
                else
                {
                    LogAction($"Skipping push for {repoName}: No remote repository or upstream branch set.");
                }
            }
        }

        private async Task<bool> RemoteBranchExistsAsync(string repoPath, string branchName)
        {
            string result = await RunGitCommandAsync($"ls-remote --heads origin {branchName}", repoPath);
            return !string.IsNullOrWhiteSpace(result);
        }

        private static bool IsGitRepository(string path)
        {
            return Directory.Exists(Path.Combine(path, ".git"));
        }

        private async Task<string> RunGitCommandAsync(string command, string workingDirectory)
        {
            ProcessStartInfo psi = new()
            {
                FileName = "git",
                Arguments = command,
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = new() { StartInfo = psi };
            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (!string.IsNullOrWhiteSpace(error) && !error.Contains("To https://")) // Ignore standard push messages
            {
                LogAction("Error: " + error);
            }

            return output.Trim();
        }

        private void Backup_Timer_Tick(object sender, EventArgs e)
        {
            LastBackup_Text.Text = LastBackup;
            NextBackup_Text.Text = NextBackup;
            LastBackup_toolStrip_TextBox.Text = LastBackup;
            NextBackup_toolStrip_TextBox.Text = NextBackup;
            if (!string.IsNullOrWhiteSpace(RepoLocation))
            {
                RunBack_Button.Enabled = true;
            }
            else
            {
                RunBack_Button.Enabled = false;
            }
            if (!string.IsNullOrEmpty(NextBackup) && DateTime.Now > DateTime.Parse(NextBackup))
            {
                _ = RunBackupAsync();
            }
        }

        private void LastBackup_Text_TextChanged(object sender, EventArgs e)
        {

        }

        private void OpenLog_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(logFilePath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = logFilePath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Log file does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the log file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ToolStripRunBackup_MenuItem_Click(object sender, EventArgs e)
        {
            await RunBackupAsync();
        }

        private void LastBackup_toolStrip_TextBox_Click(object sender, EventArgs e)
        {

        }

        private void NextBackup_toolStrip_TextBox_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripOpenForm_MenuItem_Click(object sender, EventArgs e)
        {
            Form1.ShowForm();
            Properties.Settings.Default.FormVisibility = "Visible";
            Properties.Settings.Default.Save();
        }

        private void ToolStripExit_MenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Application.Exit();
        }

        private void BackupTime_Picker_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedTime = BackupTime_Picker.Value;
            DateTime now = DateTime.Now;

            // Calculate the next backup time based on the selected time
            DateTime nextBackupTime = new(now.Year, now.Month, now.Day, selectedTime.Hour, selectedTime.Minute, selectedTime.Second);

            // If the selected time is earlier in the day than the current time, set the next backup to the next day
            if (nextBackupTime <= now)
            {
                nextBackupTime = nextBackupTime.AddDays(1);
            }

            NextBackup = nextBackupTime.ToString();
            NextBackup_Text.Text = NextBackup;
            BackupTime = selectedTime.ToString();
            Properties.Settings.Default.BackupTime = BackupTime;
            Properties.Settings.Default.NextBackup = NextBackup;
            Properties.Settings.Default.Save();
        }

        private void BackUp_NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Form1.ShowForm();
            Properties.Settings.Default.FormVisibility = "Visible";
            Properties.Settings.Default.Save();
        }

        private void Help_button_Click(object sender, EventArgs e)
        {
            string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Github-Auto-Backup-Manual.pdf");

            try
            {
                if (File.Exists(pdfPath))
                {
                    Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
                }
                else
                {
                    MessageBox.Show("The manual PDF file could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the manual: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
