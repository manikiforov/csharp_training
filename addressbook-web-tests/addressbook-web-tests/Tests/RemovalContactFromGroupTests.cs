using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemovalContactFromGroupTests : AuthTestBase
    {
        [Test]

        public void TestRemovalContactFromGroup()
        {
            GroupData group = GroupData.GetAll().Last();
            List<ContactData> oldContactList = group.GetContacts();
            ContactData contact = oldContactList.First();
            
            app.Contacts.RemoveContactFromGroup(group, contact);

            List<ContactData> newContactList = group.GetContacts();
            oldContactList.Remove(contact);
            newContactList.Sort();
            oldContactList.Sort();

            Assert.AreEqual(oldContactList, newContactList);
        }
    }
}
