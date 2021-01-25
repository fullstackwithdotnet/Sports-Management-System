using Sports_Management_System.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Models
{
    public class Event
    {
        [Key]
        public int Event_ID { get; set; }
        public Game Game { get; set; }
        public int Game_ID { get; set; }
        [Display(Name = "Feature Event")]
        public bool FeatureEvent { get; set; }

        [Display(Name = "Event Venue")]
        [Required]
        public string EventVenue { get; set; }

        [Display(Name = "Date")]
        public DateTime EventDate { get; set; } = new DateTime().Date;

        [Display(Name = "Start Time")]
        public DateTime EventStartTime { get; set; }
        [Display(Name = "End Time")]
        public DateTime EventEndTime { get; set; }
        [Display(Name = "Description")]
        [MaxWords(100, ErrorMessage = "Only allowed 100 words")]
        public string EventDescription { get; set; }
    }
}