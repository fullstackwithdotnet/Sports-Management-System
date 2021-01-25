using System.Collections.Generic;

namespace Sports_Management_System.Models.ViewModels
{
    public class ReportsViewModel
    {
        public List<EventDataViewModel> EventData { get; set; }
        public List<WorldRecordsViewModel> WorldRecords { get; set; }

    }
}