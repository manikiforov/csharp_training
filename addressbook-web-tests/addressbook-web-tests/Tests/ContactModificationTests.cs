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
            ContactData contactSpare = new ContactData("","");
            contactSpare.Nickname = "";

            ContactData newContact = new ContactData("Harry","Potter");
            newContact.Nickname = "Wizard";

            app.Contacts.manager.Navigator.GoToHomePage();

            if (!app.Contacts.ContactPresence())
            {
                app.Contacts.Create(contactSpare);
            }

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            ContactData oldContactData = oldContacts[0];

            app.Contacts.Modify(0, newContact);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].Firstname = newContact.Firstname;
            oldContacts[0].Lastname = newContact.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.ContactId == oldContactData.ContactId)
                {
                    if (newContact.Lastname == contact.Lastname)
                    {
                        Assert.AreEqual(newContact.Firstname, contact.Firstname);
                    }
                    else
                    {
                        Assert.AreEqual(newContact.Lastname, contact.Lastname);
                    }
                }
            }

        }
    }
}
