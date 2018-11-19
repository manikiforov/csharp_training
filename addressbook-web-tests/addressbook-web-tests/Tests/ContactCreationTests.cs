﻿using System;
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
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Marty");
            contact.Lastname = "McFly";
            contact.Nickname = "Schoolboy";

            app.Contacts.Create(contact);
                
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("");
            contact.Lastname = "";
            contact.Nickname = "";

            app.Contacts.Create(contact);

        }

    }
}