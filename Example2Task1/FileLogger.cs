namespace Example2Task1
{
    public interface ILogger
    {
        public void Log(string message);
    }

    public class FileLogger : ILogger 
    {
        private const string FileName = "Send.log";
        public void Log(string message) 
        { 
            File.AppendAllText(FileName, $"{message}{Environment.NewLine}");
        }
    }
}
