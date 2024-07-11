using EasyNetQ;
using Telegram.Bot;

namespace PetSocialNetwork.NotificationService
{
    public class TelegramNotificationService : BackgroundService
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IBus _bus;
        public TelegramNotificationService(ITelegramBotClient telegramBotClient, IBus bus)
        {
            _telegramBotClient = telegramBotClient ?? throw new ArgumentException(nameof(telegramBotClient));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.PubSub.SubscribeAsync<TelegramRegistrationMessage>("registration_queue", async (receivedMessage) =>
                {
                    var message = await _telegramBotClient.SendTextMessageAsync
                        (receivedMessage.ChatId, receivedMessage.Message, cancellationToken: stoppingToken);
                }, stoppingToken);
            }
        }
    }
}
