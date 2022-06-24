using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace BirthdayTrackerApp.Classes
{
    public class Celebrant
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int BirthDay { get; set; }
        public int BirthMonth { get; set; }
        public int BirthYear { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
            set { }
        }
        public string BirthDateString
        {
            get
            {
                DateTime birthDate = new DateTime(BirthYear, BirthMonth, BirthDay);
                return birthDate.ToString("MMMM dd, yyyy", DateTimeFormatInfo.InvariantInfo);
            }
            set {}
        }

        public string BirthdayString
        {
            get 
            {
                DateTime birthDate = new DateTime(DateTime.Now.Year, BirthMonth, BirthDay);
                return birthDate.ToString("dddd, MMMM dd", DateTimeFormatInfo.InvariantInfo);
            }
            set {}
        }


        public override string ToString()
        {
            return $"{FirstName} {LastName} - {Email} - {BirthMonth}/{BirthDay}/{BirthYear}";
        }
    }
}
