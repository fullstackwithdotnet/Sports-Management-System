using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Models
{
    public class Event_Photos
    {
        [Key]
        public int Photo_ID { get; set; }

        [Display(Name = "Photo")]
        public string Event_Photo { get; set; }

        [Display(Name = "Tags")]
        public string Event_PhotoTags { get; set; }

        [Display(Name = "Event")]
        public Event Event { get; set; }
        public int Event_ID { get; set; }
    }
}