using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class Numbers : INumbers
    {
        private readonly ITelegramBotClient _telegramClient;

        public Numbers(ITelegramBotClient telegramClient, int Calc)
        {
            _telegramClient = telegramClient;
        }

        public async Task SummingNumbers(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            int result = 0;
            try
            {
                string s = update.Message.Text;
                if (s == "Посчитать сумму")
                {
                    string[] subs = s.Split(' ');

                    foreach (var item in subs)
                    {
                        int num = Convert.ToInt32(item);
                        result += num;
                    }
                    Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
                    await _telegramClient.SendTextMessageAsync(update.Message.From.Id, $"Сумма введеных чисел равна: {result}", cancellationToken: cancellationToken);
                }
                else
                {
                    await _telegramClient.SendTextMessageAsync(update.Message.From.Id, $"Введите числа!", cancellationToken: cancellationToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось приобразовать строку в число {ex}");
            }
        }
    }
}
