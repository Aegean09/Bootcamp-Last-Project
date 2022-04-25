using System.Collections;
using System.Collections.Generic;

namespace _01_initial.Models
{
    public class DeletedEvents
    {
        public DeletedEvents()
        {
            Deleted_Events = new HashSet<Events>();
        }
        public int Del_Id { get; set; }
        public ICollection<Events> Deleted_Events { get; set; }
    }
}
