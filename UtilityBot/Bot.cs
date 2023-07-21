using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using UtilityBot.Controllers;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBot.Services;


namespace UtilityBot
{
    class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramClient;
        private InlineKeyboardController _inlineKeyboardController;
        private TextMessageController _textMessageController;
        private DefaultMessageController _defaultMessageController;
        private ISymbols _symbolsServices;
        private INumbers _numbersServices;


        public Bot(ITelegramBotClient telegramClient, InlineKeyboardController inlineKeyboardController, TextMessageController textMessageController, DefaultMessageController defaultMessageController, ISymbols symbolsServices, INumbers numbersServices)
        {
            _telegramClient = telegramClient;
            _inlineKeyboardController = inlineKeyboardController;
            _textMessageController = textMessageController;
            _defaultMessageController = defaultMessageController;
            _symbolsServices = symbolsServices;
            _numbersServices = numbersServices;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _telegramClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions() { AllowedUpdates = { } },
                cancellationToken: stoppingToken);

            Console.WriteLine("Бот запущен");
        }

        public string ActionCommand { get; set; }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                ActionCommand = await _inlineKeyboardController.Handle(update.CallbackQuery, cancellationToken);
                return;
            }

            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Text:
                        if (ActionCommand != null)
                        {
                            CommandHandler(botClient, update, cancellationToken, ActionCommand);
                        }
                        await _textMessageController.Handle(update.Message, cancellationToken);
                        return;
                    default:
                        await _defaultMessageController.Handle(update.Message, cancellationToken);
                        return;
                }
            }
        }

        async Task CommandHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, string actionCommand)
        {
            if (update.Type == UpdateType.Message)
            {
                if (actionCommand == "Посчитать символы")
                {
                    await _symbolsServices.CoutingChars(botClient, update, cancellationToken);
                }

                if ((actionCommand == "Посчитать сумму"))
                {
                    await _numbersServices.SummingNumbers(botClient, update, cancellationToken);
                }
            }
        }

        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            Console.WriteLine("Waiting 10 seconds before retry");
            Thread.Sleep(10000);
            return Task.CompletedTask;
        }
    }
}
