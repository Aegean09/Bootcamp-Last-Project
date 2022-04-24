using System;

namespace _01_initial.Models
{
    public class EventsDTO
    {
        public int Event_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public bool isTicket { get; set; }
        public int Price { get; set; }
        public string City_Name { get; set; }
        public string Category_Name { get; set; }
    }
}
