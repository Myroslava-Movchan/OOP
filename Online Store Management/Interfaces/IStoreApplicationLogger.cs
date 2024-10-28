namespace Online_Store_Management.Interfaces
{
    public interface IStoreApplicationLogger
    {
        void Log(string message);
        void LogError(string message);
        void LogWarning(string message);
    }
}
