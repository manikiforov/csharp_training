using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void Task5_ContactRemovalTest()
        {
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            selectSubmitHelper.SelectItem(1);
            contactHelper.ConfirmDel();
            contactHelper.DeleteContact();
            navigator.GoToHomePage();
        }

    }
}
