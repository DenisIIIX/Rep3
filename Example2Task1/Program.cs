using Microsoft.Extensions.DependencyInjection;

namespace Example2Task1
{
    internal class Program
    {
        public class NotificationService
        {
            private readonly INotificationSender _notificationSender;
            private readonly ILogger _logger;

            public NotificationService(INotificationSender notificationSender, ILogger logger)
            {
                _notificationSender = notificationSender; 
                _logger = logger;
            }

            public void SendNotification(string message, string recipient)
            {
                
                string formattedMessage = $"Уведомление: {message}";

                _notificationSender.Send(recipient, formattedMessage);
                
                _logger.Log($"Отправлено уведомление для {recipient}");
            }
        }

        class Program1
        {
            static void Main()
            {
                
                ServiceCollection services = new ServiceCollection();
                services.AddSingleton<ILogger,FileLogger>();
                services.AddSingleton<EmailSender>();
                services.AddSingleton<SmsSender>();

                ServiceProvider provider = services.BuildServiceProvider();

                Console.WriteLine("Выберите тип уведомления:");
                Console.WriteLine("1 - Email");
                Console.WriteLine("2 - SMS");
                Console.Write("Ваш выбор: ");
                var choise = Console.ReadLine();
                INotificationSender sender = choise switch
                { 
                    "1" => provider.GetRequiredService<EmailSender>(),
                    "2"=> provider.GetRequiredService<SmsSender>(),                    
                };
                var logger = provider.GetRequiredService<ILogger>();
                
                var service = new NotificationService(sender,logger);
                service.SendNotification("Ваш заказ готов", "user@example.com");

                Console.WriteLine();
                Console.ReadKey();
            }
        }       
    }
}
