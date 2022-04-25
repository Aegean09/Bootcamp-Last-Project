using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace _01_initial.Models
{
    public partial class Events
    {
        public Events()
        {
            Attenders = new HashSet<Users>();
        }

        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public bool isTicket { get; set; }
        public int Price { get; set; }
        public Cities City { get; set; }
        public Categories Category { get; set; }
        public ICollection<Users> Attenders { get; set; }
    }
}
