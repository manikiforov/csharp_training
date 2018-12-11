using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {

        public GroupData(string name)
        {
            Name = name;
        }

        public bool Equals(GroupData other)
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
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name = " + Name + "\nheader = " + Header + "\nfooter = " + Footer;
        }

        public int CompareTo(GroupData other)
        {

#pragma warning disable IDE0041 // Use 'is null' check
            if (Object.ReferenceEquals(other, null))
#pragma warning restore IDE0041 // Use 'is null' check

            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public string Name { get; set; }
       
        public string Header { get; set; }
       
        public string Footer { get; set; }

        public string Id { get; set; }
        
    }
}
