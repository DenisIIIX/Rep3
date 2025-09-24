
namespace Example2Task1
{
    public interface INotificationSender
    {
        void Send(string to, string message);
    }
    public class EmailSender : INotificationSender
    {

        public void Send(string to, string message)
        {
            // Симуляция отправки email
            Console.WriteLine($"Email для {to}: {message}");
        }
    }
    public class SmsSender : INotificationSender
    {

        public void Send(string to, string message)
        {
            // Симуляция отправки email
            Console.WriteLine($"Sms для {to}: {message}");
        }
    }
}
