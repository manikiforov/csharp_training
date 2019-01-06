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
    public class ContactRemovalTests : ContactTestBase
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

            List<ContactData> oldContacts = ContactData.GetAll();

            ContactData ConToBeRemoved = oldContacts[7];

            app.Contacts.Remove(ConToBeRemoved);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.RemoveAt(7);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.ContactId, ConToBeRemoved.ContactId);
            }

        }

    }
}
