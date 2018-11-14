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
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            contactHelper.AddNewContact();
            ContactData contact = new ContactData("Marty");
            contact.Lastname = "McFly";
            contact.Nickname = "Schoolboy";
            contactHelper.InputContactData(contact);
            selectSubmitHelper.Submit();
            loginHelper.Logout();
        }

                       
    }
}
