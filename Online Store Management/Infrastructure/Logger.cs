using Online_Store_Management.Models;

namespace Online_Store_Management.Infrastructure
{
    public class Logger
    {
        private readonly ILogger<Logger> _logger;
        public Logger(ILogger<Logger> logger)
        {
            _logger = logger;
        }
        public void Log(string message)
        {
            _logger.LogInformation("Log: {message}", message);
        }
        public void LogError(string message) 
        { 
            _logger.LogError("Error: {Message}", message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning("Warning: {Message}", message);
        }

        public static Task LogToConsole(Customer customer, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            Action<DateTime, string> displayCustomer = delegate (DateTime dateTime, string lastName)
            {
                Console.WriteLine($"{dateTime.ToString("d MMM yyyy")}: {lastName}");
            };
            Display(displayCustomer);
            return Task.CompletedTask;
        }

        private static void Display(Action<DateTime, string> displayCustomer) 
        {
            displayCustomer(DateTime.UtcNow, "No such last name");
        }
    }
}
   
