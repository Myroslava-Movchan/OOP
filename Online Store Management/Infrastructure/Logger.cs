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
    }
}
   
