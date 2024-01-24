using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace GosuslugiNUnitTest
{
    //���������� ����� ��� �������� �������� ����� ����� ������ �� ����� ���������
    //��� ������ ����������� Selenium (���������� �������)
    //����� ���� ���� ���������� ����� http �������
    //�� DOM ����� ���������� ������ ������������ �� �����
    [TestFixture]
    public class PasswordRecoveryTest
    {
        private IWebDriver driver;

        //��������� ������� Chrome 
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void PasswordRecoveryFormIsDisplayed()
        {
            //���������� ��� ����� ������
            IWebElement loginButton, cantLoginButton, recoveryButton;

            //�������� ����� � �����������
            driver.Navigate().GoToUrl("https://gosuslugi.ru");

            //�������� ���� ������ � ������� �� ���
            loginButton = WaitForElement(By.XPath("//button[contains(text(), '�����')]"));
            loginButton.Click();

            cantLoginButton = WaitForElement(By.XPath("//button[contains(text(), '�� ������ �����?')]"));
            cantLoginButton.Click();

            recoveryButton = WaitForElement(By.XPath("//button[contains(text(), '�������������� ������')]"));
            recoveryButton.Click();

            //���� � ����� �� ������� �� ���� �������������� ������  
            //�� ���� ������ ��������� � url recovery (esia.gosuslugi.ru/login/recovery)
            Assert.IsTrue(driver.Url.Contains("recovery"));
        }

        //�������� ��� ��������� ��������� �������� (DOM ������)
        private IWebElement WaitForElement(By locator, int timeoutInSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(d => d.FindElement(locator));
        }

        //��������� �������
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
