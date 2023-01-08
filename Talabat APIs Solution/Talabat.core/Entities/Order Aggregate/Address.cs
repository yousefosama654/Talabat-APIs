using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities.Order_Aggregate
{
    public class Address
    {
        public Address()
        {

        }
        public Address(string firstName, string lastName, string city, string street, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            Country = country;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
    }
}
