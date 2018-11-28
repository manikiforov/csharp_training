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
            manager.SelectSubmit.Submit();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Modify(int p, ContactData contact, ContactData newData)
        {
            manager.SelectSubmit.SelectItem(p);
            InitContactModification(p);
            InputContactData(newData);
            SubmitContactModification();
            manager.Navigator.GoToHomePage();
            return this;
        }

       
        public ContactHelper Remove (int p, ContactData contact)
        {
            manager.SelectSubmit.SelectItem(p);
            ConfirmDel();
            DeleteContact();
            manager.Navigator.GoToHomePage();
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
            return this;
        }

        private ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + index + " ]")).Click();
            return this;
        }

        private ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[2]")).Click();
            return this;
        }

        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();
            manager.Navigator.GoToHomePage();
            ICollection<IWebElement> elements = driver.FindElements(By.Name("selected[]"));

            foreach (IWebElement element in elements)
            {
                contacts.Add(new ContactData(element.Text));
            }

            return contacts;
        }

        //public ContactHelper SubModify(int p, ContactData newData)
        //{
        //manager.SelectSubmit.SelectItem(p);
        //InitContactModification(p);
        //InputContactData(newData);
        //SubmitContactModification();
        //manager.Navigator.GoToHomePage();
        //return this;
        //}

        //public ContactHelper SubRemove(int p)
        //{
        //manager.SelectSubmit.SelectItem(p);
        //ConfirmDel();
        //DeleteContact();
        //manager.Navigator.GoToHomePage();
        //return this;
        //}
    }
}
