using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]

        public void TestAddingContactToGroup()
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

            
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();
            
            //List<GroupData> GroupsList = GroupData.GetAll();
            
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
