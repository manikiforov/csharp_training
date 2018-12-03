using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        protected bool acceptNextAlert = true;

        public ContactHelper(ApplicationManager manager) : base(manager)

        {
        }

        public ContactHelper Create(ContactData contact)
        {
            AddNewContact();
            InputContactData(contact);
            SubmitContactCreation();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Modify(int p, ContactData contact, ContactData newData)
        {
            SelectContact(p);
            InitContactModification(p);
            InputContactData(newData);
            SubmitContactModification();
            manager.Navigator.GoToHomePage();
            return this;
        }

       
        public ContactHelper Remove (int p, ContactData contact)
        {
            SelectContact(p);
            ConfirmDel();
            DeleteContact();
            manager.Navigator.GoToHomePage();
            return this;
              
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + " ]")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        public ContactHelper AddNewContact()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper InputContactData(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);
            return this;
        }

        public ContactHelper ConfirmDel()
        {
            acceptNextAlert = true;
            return this;
        }

        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            contactCache = null;
            return this;
        }

        private ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index+1) + " ]")).Click();
            return this;
        }

        private ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            contactCache = null;
            return this;
        }

        public bool ContactPresence()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();

                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//*[@id='maintable']/tbody/tr[@name='entry']")); // организуем список элементов строк из таблицы контактов

                foreach (IWebElement element in elements)
                {
                    string Firstname = element.FindElement(By.XPath(".//td[3]")).Text; //забираем текст имени из строки
                    string Lastname = element.FindElement(By.XPath(".//td[2]")).Text; // забираем текст фамилии из строки
                    contactCache.Add(new ContactData(Firstname, Lastname)); // формируем объект контакта с именем и фамилией
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.XPath("//*[@id='maintable']/tbody/tr[@name='entry']")).Count;
        }

    }
}
