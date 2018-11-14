using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin","secret"));
            navigator.GoToGroupsPage();
            grouphelper.InitGroupCreation();
            GroupData group = new GroupData("aaa");
            group.Header = "ddd";
            group.Footer = "fff";
            grouphelper.FillGroupForm(group);
            selectSubmitHelper.Submit();
            grouphelper.ReturnToGroupsPage();
            loginHelper.Logout();
        }

                 
    }
}
