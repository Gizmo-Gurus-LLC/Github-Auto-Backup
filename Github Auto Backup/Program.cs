namespace Github_Auto_Backup
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using var mutex = new Mutex(true, "Github_Auto_Backup", out bool createdNew);
            if (!createdNew)
            {
                // If the mutex already exists, exit the application
                return;
            }

            // Add global exception handlers
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Form1.ShowForm();
            Application.Run();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            LogAction($"Unhandled exception: {ex.Message}\n{ex.StackTrace}", true);
            MessageBox.Show($"An unhandled exception occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LogAction($"Thread exception: {e.Exception.Message}\n{e.Exception.StackTrace}", true);
            MessageBox.Show($"A thread exception occurred: {e.Exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void LogAction(string message, bool addNewLines = false)
        {
            // Implement logging logic here
            try
            {
                string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Github_Auto_Backup.log");
                using StreamWriter writer = new(logFilePath, true);
                if (addNewLines)
                {
                    writer.WriteLine();
                    writer.WriteLine($"{DateTime.Now}: {message}");
                    writer.WriteLine();
                }
                else
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while logging: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
