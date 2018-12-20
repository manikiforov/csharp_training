using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]

    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string detailsForm;
        private string name;
        private string nameBlock;
        private string phonesBlock;


        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public bool Equals(ContactData other)
        {
#pragma warning disable IDE0041 // Use 'is null' check
            if (Object.ReferenceEquals(other, null))
#pragma warning restore IDE0041 // Use 'is null' check
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return (Firstname == other.Firstname && Lastname == other.Lastname);
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode() + Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "Firstname = " + Firstname + "\nLastname = " + Lastname + "\nNickname = " + Nickname;
        }

        public int CompareTo(ContactData other)
        {
#pragma warning disable IDE0041 // Use 'is null' check
            if (Object.ReferenceEquals(other, null))
#pragma warning restore IDE0041 // Use 'is null' check
            {
                return 1;
            }
            if (Object.ReferenceEquals(other.Lastname, Lastname))
            {
                return Firstname.CompareTo(other.Firstname);
            }
            else
            {
                return Lastname.CompareTo(other.Lastname);

            }
        }


        [Column(Name = "id"), PrimaryKey]
        public string ContactId { get; set; }

        [Column(Name = "firstname"), NotNull]
        public string Firstname { get; set; }

        [Column(Name = "lastname"), NotNull]
        public string Lastname { get; set; }

        [Column(Name = "nickname"), NotNull]
        public string Nickname { get; set; }
        
        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }

            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanItems(Email) + CleanItems(Email2) + CleanItems(Email3)).Trim();
                }
            }

            set
            {
                allEmails = value;
            }
        }

        public string Name
        {
            get
            {
                if (name != null)
                {
                    return name;
                }
                else
                {
                    return (Clean(Firstname) + " " + Clean(Lastname)).Trim();
                }
            }

            set
            {
                name = value;
            }
        }

        public string NameBlock
        {
            get
            {
                if (nameBlock != null)
                {
                    return nameBlock;
                }
                else
                {
                    return (CleanItems(Name) + CleanItems(Nickname) + CleanItems(Address)).Trim();
                }
            }

            set
            {
                nameBlock = value;
            }
        }

        public string PhonesBlock
        {
            get
            {
                if (phonesBlock != null)
                {
                    return phonesBlock;
                }
                else
                {
                    return (CleanHomePhone(HomePhone) + CleanMobilePhone(MobilePhone) + CleanWorkPhone(WorkPhone)).Trim();
                }
            }

            set
            {
                phonesBlock = value;
            }
        }

        public string DetailsForm

        {
            get
            {
                if (detailsForm != null)
                {
                    return detailsForm;
                }
                else
                {
                    return (CleanBlocks(NameBlock) + CleanBlocks(PhonesBlock) + CleanBlocks(AllEmails)).Trim();
                }
            }

            set
            {
                detailsForm = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[- ()]", "") + "\r\n";
        }

        private string CleanItems (string item)
        {
            if (item == null || item == "")
            {
                return "";
            }
            return (item + "\r\n");
        }

        private string Clean (string item)
        {
            if (item == null || item == "")
            {
                return "";
            }
            return item;
        }

        private string CleanHomePhone(string homephone)
        {
            if (homephone == null || homephone == "")
            {
                return "";
            }
            return ("H: " + homephone + "\r\n");
        }

        private string CleanMobilePhone(string mobilephone)
        {
            if (mobilephone == null || mobilephone == "")
            {
                return "";
            }
            return ("M: " + mobilephone + "\r\n");
        }

        private string CleanWorkPhone(string workphone)
        {
            if (workphone == null || workphone == "")
            {
                return "";
            }
            return ("W: " + workphone + "\r\n");
        }

        private string CleanBlocks (string block)
        {
            if (block == null || block == "")
            {
                return "";
            }
            return (block + "\r\n" + "\r\n");
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }

        }

    }
}
