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
using UtilityBot.Controllers;

namespace UtilityBot.Services
{
    public class Symbols
    {
        private readonly ITelegramBotClient _telegramClient;

        public Symbols(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            {
                Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
                await _telegramClient.SendTextMessageAsync(update.Message.From.Id, $"Длина сообщения: {update.Message.Text.Length} знаков", cancellationToken: cancellationToken);
            }
        }
    }
}
