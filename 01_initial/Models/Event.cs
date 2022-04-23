using System;
using System.Collections.Generic;

namespace _01_initial.Models
{
    public partial class Event
    {
        public int EventId { get; set; } 
        public string Name { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
    }
}
