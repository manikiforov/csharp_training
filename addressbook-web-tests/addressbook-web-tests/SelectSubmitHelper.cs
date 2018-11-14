using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class SelectSubmitHelper : HelperBase
    {
        public SelectSubmitHelper(IWebDriver driver) : base(driver)

        {
        }

        public void Submit()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        public void SelectItem(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + " ]")).Click();
        }
    }
}
