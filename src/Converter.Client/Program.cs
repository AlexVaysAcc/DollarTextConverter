using Serilog;

namespace Converter.Client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "client_log-.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Application is starting");

            try
            {
                ApplicationConfiguration.Initialize();
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application terminated unexpectedly.");
            }
        }
    }
}