using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Models
{
    public class Address
    {
        public string Id { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
