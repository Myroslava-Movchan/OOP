using Online_Store_Management.Models;

namespace Online_Store_Management.Infrastructure
{
    public class Logger(ILogger<Logger> logger)
    {
        private readonly ILogger<Logger> _logger = logger;

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

        public static Task LogToConsole(OrderInfo order, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            static void displayCustomer(DateTime dateTime, string lastName)
            {
                Console.WriteLine($"{dateTime:d MMM yyyy}: {lastName}");
            }
            Display(displayCustomer);
            return Task.CompletedTask;
        }

        private static void Display(Action<DateTime, string> displayCustomer) 
        {
            displayCustomer(DateTime.UtcNow, "No such last name");
        }
    }
}
   
