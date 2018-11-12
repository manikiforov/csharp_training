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
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            SelectItem(1);
            ConfirmDel();
            DeleteContact();
            GoToHomePage();
        }

    }
}
