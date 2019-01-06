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

    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData contactSpare = new ContactData("","");
            contactSpare.Nickname = "";

            ContactData newContact = new ContactData("Darth","Wader");
            newContact.Nickname = "Sith";

            app.Contacts.manager.Navigator.GoToHomePage();

            if (!app.Contacts.ContactPresence())
            {
                app.Contacts.Create(contactSpare);
            }

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldContactData = oldContacts[5];

            app.Contacts.Modify(oldContactData, newContact);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContactData.Firstname = newContact.Firstname;
            oldContactData.Lastname = newContact.Lastname;
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
