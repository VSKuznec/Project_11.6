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
    public class Numbers
    {
        private readonly ITelegramBotClient _telegramClient;

        public Numbers(ITelegramBotClient telegramClient, int Calc)
        {
            _telegramClient = telegramClient;
        }

        public int Calc(string message)
        {
            int result = 0;
            try
            {
                string s = message.ToString();

                string[] subs = s.Split(' ');

                foreach (var item in subs)
                {
                    int num = Convert.ToInt32(item);
                    result += num;
                }
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Не удалось приобразовать строку в число {ex}");
            }
            return result;
        }
    }
}
