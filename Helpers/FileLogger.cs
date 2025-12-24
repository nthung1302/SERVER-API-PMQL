namespace Files.Helpers
{
    public static class FileLogger
    {
        private static readonly object _lock = new object();

        public static void Log(string message, string level = "INFO")
        {
            string logLine =
                $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

            Console.WriteLine(logLine);

            try
            {
                string logFolder = Path.Combine(
                    Directory.GetCurrentDirectory(), "Logs");

                if (!Directory.Exists(logFolder))
                    Directory.CreateDirectory(logFolder);

                string fileName = $"log-{DateTime.Now:yyyy-MM-dd}.txt";
                string filePath = Path.Combine(logFolder, fileName);

                lock (_lock)
                {
                    File.AppendAllText(
                        filePath,
                        logLine + Environment.NewLine
                    );
                }
            }
            catch
            {

            }
        }
    }
}
