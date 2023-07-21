using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace UtilityBot.Services
{
    public interface INumbers
    {
        Task SummingNumbers(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
