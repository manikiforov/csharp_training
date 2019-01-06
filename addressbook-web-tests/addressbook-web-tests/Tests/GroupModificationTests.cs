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

    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData groupSpare = new GroupData("");
            groupSpare.Header = "";
            groupSpare.Footer = "";

            GroupData newData = new GroupData("hydra");
            newData.Header = null;
            newData.Footer = null;

            app.Groups.manager.Navigator.GoToGroupsPage();

            if (!app.Groups.GroupPresence())
            {
                app.Groups.Create(groupSpare);
            }

            List<GroupData> oldGroups = GroupData.GetAll();

            GroupData oldData = oldGroups[4];

            app.Groups.Modify(oldData, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());


            List<GroupData> newGroups = GroupData.GetAll();
            oldData.Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
