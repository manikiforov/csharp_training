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
            app.Navigator.GoToHomePage();
            if (!app.Contacts.ContactPresence())
            {
                ContactData ReserveContact = new ContactData("ReserveName", "ReserveLastName");
                app.Contacts.Create(ReserveContact);
            }

            app.Navigator.GoToGroupsPage();
            if (!app.Groups.GroupPresence())
            {
                GroupData ReserveGroup = new GroupData("ExtraGroup");
                app.Groups.Create(ReserveGroup);
            }

            GroupData group = GroupData.GetAll().Last();
            List<ContactData> oldContactList = group.GetContacts();

            //if (oldContactList == null)
            //{
                //int index = System.Array.IndexOf()
            //}

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
