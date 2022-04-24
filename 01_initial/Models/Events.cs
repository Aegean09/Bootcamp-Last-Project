using System;
using System.Collections.Generic;

namespace _01_initial.Models
{
    public partial class Events
    {
        public Events()
        {
            City_Name = new HashSet<City>();
            Category_Name = new HashSet<Categories>();
        }
        public int EventId { get; set; } 
        public string Name { get; set; }
        public string City { get; set; }
        public virtual ICollection<City> City_Name { get; set; }
        public virtual ICollection<Categories> Category_Name{ get; set; }

        //public string Category { get; set; }
    }
}
