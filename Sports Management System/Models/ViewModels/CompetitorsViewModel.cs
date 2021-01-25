using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Models.ViewModels
{
    public class CompetitorsViewModel
    {
        public CompetitorsViewModel()
        {
            Games = new Collection<AssignedGameData>();
        }
        public int Competitor_ID { get; set; }
        [Display(Name = "Salutation")]
        public string Competitor_Salutation { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Competitor_Name { get; set; }
        [Display(Name = "DOB")]
        public DateTime Competitor_DoB { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Competitor_Email { get; set; }
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
        [Display(Name = "Website")]
        public string Competitor_Website { get; set; }
        [Display(Name = "Photo")]
        public string Competitor_Photo { get; set; }
        public virtual ICollection<AssignedGameData> Games { get; set; }
    }

}
