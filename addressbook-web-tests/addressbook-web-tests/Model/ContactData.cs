using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string detailsForm;


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

        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        public string Nickname { get; set; }

        public string ContactId { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

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
                    return (CleanMails(Email) + "\r\n" + CleanMails(Email2) + "\r\n" + CleanMails(Email3) + "\r\n").Trim();
                }
            }

            set
            {
                allEmails = value;
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
                    return Regex.Replace((Firstname + Lastname + Nickname + Address + HomePhone + MobilePhone + WorkPhone + Email + Email2 + Email3), " ", "");
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

        private string CleanMails (string mail)
        {
            if (mail == null || mail == "")
            {
                return "";
            }
            return mail;
        }
    }
}
