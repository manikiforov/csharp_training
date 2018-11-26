using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            app.Groups.manager.Navigator.GoToGroupsPage();

            if (!app.Groups.IsElementPresent(By.Name("selected[]")))
            {
                app.Groups.Create(group);
            }

            app.Groups.Remove(1, group);
        }
    }
}
