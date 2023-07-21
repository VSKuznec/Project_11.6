using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using UtilityBot.Configuration;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Посчитать символы" , $"Символы"),
                        InlineKeyboardButton.WithCallbackData($" Посчитать сумму" , $"Числа")
                    });

                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот считает количество символов в тексте и сумму чисел, как калькулятор.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Отправьте текст или числа через пробел для получения результата, но сначала выберите одну из функций бота.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Отправьте мне сообщение для обработки.", cancellationToken: ct);
                    break;
            }
        }
    }
}
