﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]

    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContact = new ContactData("Tomas");
            newContact.Lastname = "Anderson";
            newContact.Nickname = "NEO";

            app.Contacts.Modify(2, newContact);

        }
    }
}