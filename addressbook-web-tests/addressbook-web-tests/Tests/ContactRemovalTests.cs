using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
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
            ContactData contactSpare = new ContactData("","");
            contactSpare.Nickname = "";

            app.Contacts.manager.Navigator.GoToHomePage();

            if (!app.Contacts.ContactPresence())
            {
                app.Contacts.Create(contactSpare);
            }

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            ContactData ContoBeRemoved = oldContacts[0];

            app.Contacts.Remove(0);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.ContactId, ContoBeRemoved.ContactId);
            }

        }

    }
}
