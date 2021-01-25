using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Models
{
    public class Event_Record
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Event")]
        public Event Event { get; set; }
        public int Event_ID { get; set; }

        [Display(Name = "Competitor")]
        public Competitor Competitor { get; set; }

        public int Competitor_ID { get; set; }

        [Display(Name = "Position")]
        [Required]
        public Positions Competitor_Position { get; set; }

        [Display(Name = "Medal")]
        [Required]
        public Medals Competitor_Medal { get; set; }

        [Display(Name = "World Record")]
        public bool WorldRecord { get; set; }

    }
}