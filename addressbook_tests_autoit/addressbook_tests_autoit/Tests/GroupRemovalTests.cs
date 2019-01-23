using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
    [TestFixture]

    public class GroupRemovalTests : TestBase
    {
        [Test]

        public void TestGroupRemoval()
        {
            int p = 0;
            GroupData newGroup = new GroupData()
            {
                Name = "ForRemove"
            };
            
            if (app.Groups.GroupsNumber() >= 1)
            {
                app.Groups.Add(newGroup);
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(p);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(p);

            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
