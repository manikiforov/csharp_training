using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void Task4_ContactCreationTest()
        {
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            AddNewContact();
            ContactData contact = new ContactData("Marty");
            contact.Lastname = "McFly";
            contact.Nickname = "Schoolboy";
            InputContactData(contact);
            Submit();
            Logout();
        }

                       
    }
}
