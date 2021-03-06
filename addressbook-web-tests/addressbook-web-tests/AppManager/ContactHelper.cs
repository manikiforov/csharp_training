﻿using System;
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

        public ContactHelper Modify(int p, ContactData newData)
        {
            SelectContact(p);
            InitContactModification(p);
            InputContactData(newData);
            SubmitContactModification();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Modify(ContactData oldData, ContactData newData)
        {
            SelectContactFromDb(oldData.ContactId);
            InitContactModification(oldData.ContactId);
            InputContactData(newData);
            SubmitContactModification();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Remove(int p)
        {
            SelectContact(p);
            ConfirmDel();
            DeleteContact();
            manager.Navigator.GoToHomePage();
            return this;
              
        }

        public ContactHelper Remove(ContactData contact)
        {
            SelectContactFromDb(contact.ContactId);
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

        public ContactHelper SelectContactFromDb (String id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='"+id+"'])")).Click();
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
            driver.FindElement(By.CssSelector("div.msgbox"));
            contactCache = null;
            return this;
        }

        private ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index+1) + " ]")).Click();
            return this;
        }

        private ContactHelper InitContactModification(string id)
        {
            driver.FindElement(By.XPath("//a[@href='edit.php?id=" + id + "']")).Click();
            return this;
        }

        private ContactHelper ContactDetailsInformation (int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'])[" + (index+1) + " ]")).Click();
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

                    contactCache.Add(new ContactData(Firstname, Lastname)
                    {
                        ContactId = element.FindElement(By.Name("selected[]")).GetAttribute("value")
                    }); 
                }
            }
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.XPath("//*[@id='maintable']/tbody/tr[@name='entry']")).Count;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allemails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allemails
            };

        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            // for task 12
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                WorkPhone = workPhone,
                MobilePhone = mobilePhone,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                // for task 12
                Nickname = nickName
            };

        }

        public string GetDetailedContactInformation(int index)
        {
            manager.Navigator.GoToHomePage();
            ContactDetailsInformation(index);
            string details = driver.FindElement(By.Id("content")).Text;
            return details;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

        public void AddContactToGroup (ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.ContactId);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        private void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name); ;
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void RemoveContactFromGroup (GroupData group, ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            ChooseGroupFilter(group);
            SelectContact(contact.ContactId);
            DeleteContact();
        }

       public void ChooseGroupFilter (GroupData group)
       {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(group.Name);
       }
    }
}
