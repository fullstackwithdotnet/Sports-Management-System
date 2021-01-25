using System.Collections.Generic;

namespace Sports_Management_System.Models.ViewModels
{
    public class EventDataViewModel
    {
        public Event Event { get; set; }
        public string EventVenue { get; set; }
        public string Country { get; set; }
        public int GoldMedals { get; set; }
        public int SilverMedals { get; set; }
        public int BronzMedals { get; set; }
        public int TotalMedals { get; set; }

        public List<WorldRecordsViewModel> WorldRecords { get; set; }

    }
}