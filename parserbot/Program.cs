using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;

namespace parserbot
{
    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("------------------------------------");

        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }

        public async Task WeDriverRun()
        {
            IWebDriver driver;

            // Дайвер путь
            string path = "C:\\Users\\shymkentbay.b\\source\\repos\\parserbot\\parserbot\\drivers";

            EdgeOptions options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            // Создать экземплярь драйвера
            driver = new EdgeDriver(path, options);
            // Запуск браузера
            driver.Navigate().GoToUrl("https://kaspi.kz/shop/p/apple-iphone-13-128gb-chernyi-102298404/?c=750000000");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);
            try
            {
                //var head = driver.FindElement(By.ClassName("item__heading"));
                //var rate = driver.FindElement(By.ClassName("item__rating-link"));
                var price = driver.FindElement(By.ClassName("item__price-once"));
                if (price == null)
                {
                    Console.WriteLine("price = null");
                }
                else
                    Console.WriteLine("Цена: " + price.Text);
                //Console.WriteLine("Рейтинг: " + driver.FindElement(By.ClassName("item__rating-link")).Text);
                //Console.WriteLine("Цена: " + driver.FindElement(By.ClassName("item__price-once")).Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n*************************** Error ****************************************");
                Console.WriteLine();
            }
            finally
            {
                driver.Close();
            }
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

    }
}








