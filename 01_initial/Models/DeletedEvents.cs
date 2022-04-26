using System;
using System.Collections;
using System.Collections.Generic;

namespace _01_initial.Models
{
    public class DeletedEvents
    {
        //public DeletedEvents()
        //{
        //    Deleted_Events = new HashSet<Events>();
        //}
        public string Del_Name { get; set; }
        public DateTime Del_Event_Date { get; set; }
        public string Del_Description { get; set; }
        public DateTime Del_Date { get; set; }
        //    public ICollection<Events> Deleted_Events { get; set; }
    }
}
