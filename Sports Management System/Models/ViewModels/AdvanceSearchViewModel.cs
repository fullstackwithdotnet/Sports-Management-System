using System.Collections.Generic;

namespace Sports_Management_System.Models.ViewModels
{
    public class AdvanceSearchViewModel
    {
        public Event Event { get; set; }
        public List<Event> Events { get; set; }
        public Event_Photos Photos { get; set; }

    }
}