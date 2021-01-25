using Sports_Management_System.CustomValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Models
{
    public class Game
    {
        [Key]
        public int Game_ID { get; set; }
        [Required]
        [ValidGameCode(ErrorMessage = "Game Code must be 7 characters in length and a mixture of 4 uppercase characters and 3 numerals")]
        [MaxLength(250)]
        [Display(Name = "Game Code")]
        public string Game_Code { get; set; }
        [Required]
        [Display(Name = "Game Name")]
        public string Game_Name { get; set; }

        [Display(Name = "Duration(Hours)")]
        public int Game_DurationInHours { get; set; }
        [Display(Name = "Description")]
        public string Game_Description { get; set; }
        [Required]
        [Display(Name = "Rules Booklet")]
        public string Game_RulesBooklet { get; set; }
        public virtual ICollection<Competitor> Competitors { get; set; }
    }
}