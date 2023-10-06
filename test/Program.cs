
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using Telegram.Bot;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using System.Threading.Tasks;

namespace test;
class Program
{
    static ITelegramBotClient bot = new TelegramBotClient("6384813785:AAEt5oFkXLxjQE6WA4Cm4guvUtrVTnwlDbg");

    static void Main(string[] args)
    {
        Console.WriteLine(WebDriverRun("https://kaspi.kz/shop/p/apple-iphone-13-128gb-chernyi-102298404/?c=750000000"));
    }


    public static string WebDriverRun(string url)
    {
        String result = "Информация по товару:\n";
        IWebDriver driver;
        List<string> review_datas = new List<string>();

        // Дайвер путь
        string path = "C:\\Users\\shymkentbay.b\\source\\repos\\parserbot\\parserbot\\drivers";

        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        // Создать экземплярь драйвера
        driver = new ChromeDriver(path, options);
        // Запуск браузера
        //driver.Navigate().GoToUrl("https://kaspi.kz/shop/p/apple-iphone-13-128gb-chernyi-102298404/?c=750000000");
        driver.Navigate().GoToUrl(url);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        try
        {
            driver.FindElement(By.LinkText("Алматы")).Click();
            var head = driver.FindElement(By.ClassName("item__heading"));
            result = result + head.Text + "\n\n";
            var rate = driver.FindElement(By.ClassName("item__rating-link"));
            result = result + "Отзыви: " + rate.Text.Substring(1, rate.Text.IndexOf(" ")) + "\n";
            var price = driver.FindElement(By.ClassName("item__price-once"));
            result = result + "Цена: " + price.Text + "\n";

            driver.FindElement(By.XPath($"//li[@linktext='{rate.Text}'")).Click();
            var click = driver.FindElement(By.XPath("//a[@class='reviews__view-change reviews__more button _secondary']"));
            click.Click();
            var rev_datas = driver.FindElements(By.ClassName("reviews__date"));
            
            foreach(var v in rev_datas)
            {
                //review_datas.Add(v.Text);
                Console.Write(v.Text + "_");
            }
            driver.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("\n*************************** Error ****************************************");
            Console.WriteLine(ex.Message);
            driver.Close();
            return "Системная ошибка, попробуйте еще раз";
        }
        return result;
    }
}