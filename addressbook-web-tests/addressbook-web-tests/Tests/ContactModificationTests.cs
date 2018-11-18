using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContact = new ContactData("Emmet");
            newContact.Lastname = "Brown";
            newContact.Nickname = "Doc";

            app.Contacts.Modify(1, newContact);

        }
    }
}
