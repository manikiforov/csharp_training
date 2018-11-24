using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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

            app.Contacts.Modify(1, contact, newContact);

        }
    }
}
