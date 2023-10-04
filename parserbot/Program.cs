using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

IWebDriver driver;

for(int i = 1; i <= 1; i++)
{
    try
    {
        // Дайвер путь
        string path = "C:\\Users\\shymkentbay.b\\source\\repos\\parserbot\\parserbot\\drivers";

        // Создать экземплярь драйвера
        driver = new  ChromeDriver(path);
        // Запуск браузера
        driver.Navigate().GoToUrl("https://kaspi.kz/shop/p/apple-iphone-13-128gb-chernyi-102298404/?c=750000000");

        Console.WriteLine("Продукт: " + driver.FindElement(By.ClassName("item__heading")).Text);
        Console.WriteLine("Рейтинг: " + driver.FindElement(By.ClassName("item__rating-link")).Text);
        Console.WriteLine("Цена: " + driver.FindElement(By.ClassName("item__price-once")).Text);
        //driver.Dispose();
        driver.Close();
        await Task.Delay(500);
    }
    catch(Exception ex)
    {
        Console.WriteLine();
        Console.WriteLine(ex.Message);
        Console.WriteLine();
    }
}
