using EasyNetQ;
using Telegram.Bot;

namespace PetSocialNetwork.NotificationService
{
    public class TelegramNotificationService : BackgroundService
    {
        private readonly ITelegramBotClient _telegramBotClient;
        public TelegramNotificationService(ITelegramBotClient telegramBotClient)
        {
                _telegramBotClient = telegramBotClient ?? throw new ArgumentException(nameof(telegramBotClient));
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //создаем подключение к rabit mq
            using var bus = RabbitHutch.CreateBus("host=localhost");


            await bus.PubSub.SubscribeAsync<TelegramOptions>("sender_queue", async (receivedMessage) =>
            {
    
                var message = await _telegramBotClient.SendTextMessageAsync(long.Parse(receivedMessage.TelegramId), receivedMessage.Message);
            });


            throw new NotImplementedException();
        }
    }
}
