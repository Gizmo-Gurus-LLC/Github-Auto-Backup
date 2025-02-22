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

        public Form1()
        {
            InitializeComponent();
            BackupInterval_Combo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LogAction(string message, bool addNewLines = false, bool includeDateTime = true)
        {
            try
            {
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
                case "Running":
                    BackupStatus_Text.BackColor = System.Drawing.Color.Gray;
                    break;
                case "Completed: Success":
                    BackupStatus_Text.BackColor = System.Drawing.Color.Green;
                    break;
                case "Completed: ERROR":
                    BackupStatus_Text.BackColor = System.Drawing.Color.Red;
                    break;
                case "Completed: Warning":
                    BackupStatus_Text.BackColor = System.Drawing.Color.Yellow;
                    break;
                case "Not Running":
                    BackupStatus_Text.BackColor = System.Drawing.Color.Gray;
                    break;
            }
            BackupStatus_Text.Text = BackupStatus;
        }

        public string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Github Auto Backup.log");
        public string? RepoLocation = Properties.Settings.Default.RepoLocation;
        public string? BackupInterval = Properties.Settings.Default.BackupInterval;
        public string? LastBackup = Properties.Settings.Default.LastBackup;
        public string? NextBackup = Properties.Settings.Default.NextBackup;
        public string? BackupTime = Properties.Settings.Default.BackupTime;
        public string? BackupStatus = Properties.Settings.Default.BackupStatus;

        private void Form1_Load(object sender, EventArgs e)
        {
            isFormLoading = true;

            RepoLocation_Text.Text = RepoLocation;
            BackupInterval_Combo.Text = BackupInterval;
            BackupTime_Picker.Text = BackupTime;
            LastBackup_Text.Text = LastBackup;
            NextBackup_Text.Text = NextBackup;
            // Check if BackupStatus is null and set a default value if it is
            if (string.IsNullOrEmpty(BackupStatus))
            {
                BackupStatus = "Not Running";
            }
            BackupStatus_Text.Text = BackupStatus;
            UpdateBackupStatus(BackupStatus); // Call UpdateBackupStatus with BackupStatus
            Backup_Timer.Start();
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

            isFormLoading = false;
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
            Close();
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
            await Task.Run(() => RunBackup());
        }

        private void RunBackup()
        {
            Thread backupThread = new(() =>
            {
                try
                {
                    LogAction("************************************************************", false, false);
                    LogAction("New backup started");
                    UpdateBackupStatus("Running");

                    if (!string.IsNullOrEmpty(RepoLocation))
                    {
                        if (IsGitRepository(RepoLocation))
                        {
                            ProcessRepository(RepoLocation);
                        }
                        else
                        {
                            foreach (string directory in Directory.GetDirectories(RepoLocation))
                            {
                                if (IsGitRepository(directory))
                                {
                                    ProcessRepository(directory);
                                }
                            }
                        }
                    }
                    else
                    {
                        LogAction("Repository location is not set.", false, true);
                        UpdateBackupStatus("Completed: ERROR");
                    }

                    LogAction("Backup Completed", false, true);
                    LogAction("************************************************************", false, false);
                    LastBackup = DateTime.Now.ToString();

                    if (!string.IsNullOrEmpty(BackupInterval) && backupIntervalMapping.TryGetValue(BackupInterval, out int intervalMinutes))
                    {
                        NextBackup = DateTime.Now.AddMinutes(intervalMinutes).ToString();
                    }
                    else
                    {
                        NextBackup = "Unknown";
                    }
                }
                catch (Exception ex)
                {
                    LogAction($"Error: {ex.Message}", false, true);
                    UpdateBackupStatus("Completed: Error");
                }
                if (BackupStatus == "Completed: Warning")
                {
                    LogAction("Backup completed with warnings. See above for further details", false, true);
                    UpdateBackupStatus(BackupStatus);
                }
                else
                {
                    UpdateBackupStatus("Completed: Success");
                }
                LogAction($"Next backup scheduled for {NextBackup}", false, true);
            })
            {
                Priority = ThreadPriority.Lowest
            };
            backupThread.Start();
        }


        private void ProcessRepository(string repoPath)
        {
            string repoName = new DirectoryInfo(repoPath).Name;
            LogAction($"Processing repository: {repoName}");

            string currentBranch = RunGitCommand("rev-parse --abbrev-ref HEAD", repoPath);
            if (currentBranch == "main" || currentBranch == "master")
            {
                LogAction($"Warning: Skipping repository \"{repoName}\" because it is set to the \"{currentBranch}\" branch.", false, true);
                BackupStatus = "Completed: Warning";
                return;
            }

            if (!RemoteBranchExists(repoPath, currentBranch))
            {
                DialogResult result = MessageBox.Show($"Branch \"{currentBranch}\" in \"{repoName}\" does not exist in the remote repository. Do you want to create it?", "Github Auto Backup: Branch Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    RunGitCommand($"push -u origin {currentBranch}", repoPath);
                    LogAction($"Branch \"{currentBranch}\" in Repository \"{repoName}\" created in the remote repository.");
                }
                else
                {
                    LogAction($"Branch \"{currentBranch}\" in Repository \"{repoName}\" was NOT created in the remote repository.");
                    return;
                }
            }

            RunGitCommand("add .", repoPath);

            // Check if there are changes to commit
            string statusOutput = RunGitCommand("status --porcelain", repoPath);
            bool hasChanges = !string.IsNullOrWhiteSpace(statusOutput);
            if (hasChanges)
            {
                RunGitCommand("commit -m \"Auto Backup of working Github repositories\"", repoPath);
                LogAction("Changes committed.");
            }
            else
            {
                LogAction("No changes to commit.");
            }

            if (hasChanges)
            {
                string remoteUrl = RunGitCommand("config --get remote.origin.url", repoPath);
                string upstreamBranch = RunGitCommand("rev-parse --abbrev-ref --symbolic-full-name @{u}", repoPath);

                if (!string.IsNullOrWhiteSpace(remoteUrl) && !string.IsNullOrWhiteSpace(upstreamBranch))
                {
                    LogAction($"Pushing repository: {repoName} on branch {currentBranch}");
                    string pushOutput = RunGitCommand("push", repoPath);
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

        private bool RemoteBranchExists(string repoPath, string branchName)
        {
            string result = RunGitCommand($"ls-remote --heads origin {branchName}", repoPath);
            return !string.IsNullOrWhiteSpace(result);
        }

        private static bool IsGitRepository(string path)
        {
            return Directory.Exists(Path.Combine(path, ".git"));
        }

        string RunGitCommand(string command, string workingDirectory)
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
            string output = process.StandardOutput.ReadToEnd().Trim();
            string error = process.StandardError.ReadToEnd().Trim();
            process.WaitForExit();

            if (!string.IsNullOrWhiteSpace(error) && !error.Contains("To https://")) // Ignore standard push messages
            {
                LogAction("Error: " + error);
            }

            return output;
        }

        private void Backup_Timer_Tick(object sender, EventArgs e)
        {
            LastBackup_Text.Text = LastBackup;
            NextBackup_Text.Text = NextBackup;
            if (!string.IsNullOrEmpty(NextBackup) && DateTime.Now > DateTime.Parse(NextBackup))
            {
                RunBackup();
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
            await Task.Run(() => RunBackup());

        }
    }
}
