using System;

namespace Sports_Management_System.Models.ViewModels
{
    public class EventViewModel
    {
        public Event Event { get; set; }
        public Game Game { get; set; }
        public Event_Photos EventPhoto { get; set; }
        public DateTime EventStartTime { get; set; }
        public DateTime EventEndTime { get; set; }
    }
}