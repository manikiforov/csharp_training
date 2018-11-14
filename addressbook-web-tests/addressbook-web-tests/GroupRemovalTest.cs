using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin","secret"));
            navigator.GoToGroupsPage();
            selectSubmitHelper.SelectItem(1);
            grouphelper.RemoveGroup();
            grouphelper.ReturnToGroupsPage();
            loginHelper.Logout();
        }
    }
}
