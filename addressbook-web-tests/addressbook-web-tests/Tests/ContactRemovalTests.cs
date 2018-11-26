using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            ContactData contact = new ContactData("");
            contact.Lastname = "";
            contact.Nickname = "";

            app.Contacts.manager.Navigator.GoToHomePage();

            if (!app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                app.Contacts.Create(contact);
            }

            app.Contacts.Remove(1, contact);
        }

    }
}
