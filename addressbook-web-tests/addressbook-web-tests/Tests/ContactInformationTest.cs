using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class ContactInformationTests : AuthTestBase
    {
        [Test]

        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(1);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(1);

            // verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);

        }

        [Test]

        public void TestContactInformationDetails()
        {
            
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(5);
            string detailedContact = app.Contacts.GetDetailedContactInformation(5);

            // verification
            Assert.AreEqual(detailedContact, fromForm.DetailsForm);

        }
    }
}
