using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sports_Management_System.Models
{
    public class Competitor
    {
        [Key]
        public int Competitor_ID { get; set; }
        [MaxLength(100)]
        public string Competitor_Salutation { get; set; }
        [MaxLength(250)]
        [Required]
        [Display(Name = "Name")]
        public string Competitor_Name { get; set; }
        [Display(Name = "DOB")]
        public DateTime Competitor_DoB { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        [Required]
        [Display(Name = "Email")]
        public string Competitor_Email { get; set; }
        [MaxLength(100)]
        [Display(Name = "Description")]
        public string Competitor_Description { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Competitor_Country { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public GenderTypes Competitor_Gender { get; set; }
        [Display(Name = "Contact")]
        public string Competitor_ContactNo { get; set; }
        [Display(Name = "Web Site")]
        public string Competitor_Website { get; set; }
        [Display(Name = "Photo")]
        public string Competitor_Photo { get; set; }
        public virtual ICollection<Game> Games { get; set; }

        public Competitor()
        {
            Games = new Collection<Game>();
        }
    }
}