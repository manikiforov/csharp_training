using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]

    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData contact = new ContactData("");
            contact.Lastname = "";
            contact.Nickname = "";

            ContactData newContact = new ContactData("Harry");
            newContact.Lastname = "Potter";
            newContact.Nickname = "Wizard";

            app.Contacts.manager.Navigator.GoToHomePage();

            if (!app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                app.Contacts.Create(contact);
            }

            app.Contacts.Modify(1, contact, newContact);

        }
    }
}
