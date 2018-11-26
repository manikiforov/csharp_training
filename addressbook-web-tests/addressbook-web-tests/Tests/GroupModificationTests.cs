using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]

    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            GroupData newData = new GroupData("ppp");
            newData.Header = null;
            newData.Footer = null;

            app.Groups.manager.Navigator.GoToGroupsPage();

            if (!app.Groups.IsElementPresent(By.Name("selected[]")))
            {
                app.Groups.Create(group);
            }

            app.Groups.Modify(1, group, newData);
        }
    }
}
