using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace GosuslugiNUnitTest
{
    //Реализация теста для проверки открытия формы смены пароля на сайте Госуслуги
    //При помощи инструмента Selenium (простейший вариант)
    //Также была идея реализации через http запросы
    //Но DOM сайта содержащий кнопки подгружается не сразу
    [TestFixture]
    public class PasswordRecoveryTest
    {
        private IWebDriver driver;

        //Запускаем браузер Chrome 
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void PasswordRecoveryFormIsDisplayed()
        {
            //Переменные для наших кнопок
            IWebElement loginButton, cantLoginButton, recoveryButton;

            //Открытие сайта с Госуслугами
            driver.Navigate().GoToUrl("https://gosuslugi.ru");

            //Получаем наши кнопки и кликаем на них
            loginButton = WaitForElement(By.XPath("//button[contains(text(), 'Войти')]"));
            loginButton.Click();

            cantLoginButton = WaitForElement(By.XPath("//button[contains(text(), 'Не удаётся войти?')]"));
            cantLoginButton.Click();

            recoveryButton = WaitForElement(By.XPath("//button[contains(text(), 'восстановления пароля')]"));
            recoveryButton.Click();

            //Если в итоге мы перешли на сайт восстановления пароля  
            //То сайт должен содержать в url recovery (esia.gosuslugi.ru/login/recovery)
            Assert.IsTrue(driver.Url.Contains("recovery"));
        }

        //Ожидание для подгрузки элементов страницы (DOM дерева)
        private IWebElement WaitForElement(By locator, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(d => d.FindElement(locator));
        }

        //Закрываем браузер
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
