using ErrorAlert.Api.Helper;
using ErrorAlert.Api.Models;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace ErrorAlert.Api.SendErrorMessageService
{
    public class SendErrorMessageService : ISendErrorMessageService
    {
        private ITelegramBotClient botClient;
        private readonly IConfiguration configuration;

        public SendErrorMessageService(
            ITelegramBotClient botClient,
            IConfiguration configuration)
        {
            this.botClient = botClient;
            this.configuration = configuration;
        }

        public async Task SendErrorMessageAsync(ActionLog log)
        {
            var groupId = configuration.GetValue<long>("TelegramBot:GroupId");

            await botClient.SendTextMessageAsync(
                chatId: groupId,
                text: $"<b>{MessageFactory.TableBuilder(log)}</b>",
                parseMode: ParseMode.Html);
        }
    }
}
