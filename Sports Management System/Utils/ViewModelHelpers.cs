using Sports_Management_System.Models;
using Sports_Management_System.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Sports_Management_System.Utils
{
    public static class ViewModelHelpers
    {
        public static CompetitorsViewModel ToViewModel(this Competitor competitor)
        {
            var competitorViewModel = new CompetitorsViewModel
            {
                Competitor_ID = competitor.Competitor_ID,
                Competitor_Salutation = competitor.Competitor_Salutation,
                Competitor_Name = competitor.Competitor_Name,
                Competitor_DoB = competitor.Competitor_DoB,
                Competitor_ContactNo = competitor.Competitor_ContactNo,
                Competitor_Email = competitor.Competitor_Email,
                Competitor_Description = competitor.Competitor_Description,
                Competitor_Country = competitor.Competitor_Country,
                Competitor_Gender = competitor.Competitor_Gender,
                Competitor_Website = competitor.Competitor_Website,
                Competitor_Photo = competitor.Competitor_Photo
            };

            foreach (var game in competitor.Games)
            {
                competitorViewModel.Games.Add(new AssignedGameData
                {
                    Game_ID = game.Game_ID,
                    Game_Name = game.Game_Name,
                    Assigned = true
                }); ;
            }

            return competitorViewModel;
        }

        public static CompetitorsViewModel ToViewModel(this Competitor competitor, ICollection<Game> allDbGames)
        {
            var competitorViewModel = new CompetitorsViewModel
            {
                Competitor_ID = competitor.Competitor_ID,
                Competitor_Salutation = competitor.Competitor_Salutation,
                Competitor_Name = competitor.Competitor_Name,
                Competitor_DoB = competitor.Competitor_DoB,
                Competitor_Email = competitor.Competitor_Email,
                Competitor_ContactNo = competitor.Competitor_ContactNo,
                Competitor_Description = competitor.Competitor_Description,
                Competitor_Country = competitor.Competitor_Country,
                Competitor_Gender = competitor.Competitor_Gender,
                Competitor_Website = competitor.Competitor_Website,
                Competitor_Photo = competitor.Competitor_Photo
            };

            // Collection for full list of courses with user's already assigned courses included
            ICollection<AssignedGameData> allGames = new List<AssignedGameData>();

            foreach (var c in allDbGames)
            {
                // Create new AssignedCourseData for each course and set Assigned = true if user already has course
                var assignedGame = new AssignedGameData
                {
                    Game_ID = c.Game_ID,
                    Game_Name = c.Game_Name,
                    Assigned = competitor.Games.FirstOrDefault(x => x.Game_ID == c.Game_ID) != null
                };

                allGames.Add(assignedGame);
            }

            competitorViewModel.Games = allGames;

            return competitorViewModel;
        }

        public static Competitor ToDomainModel(this CompetitorsViewModel competitorsViewModel)
        {
            var competitor = new Competitor();
            competitor.Competitor_ID = competitorsViewModel.Competitor_ID;
            competitor.Competitor_Salutation = competitorsViewModel.Competitor_Salutation;
            competitor.Competitor_Name = competitorsViewModel.Competitor_Name;
            competitor.Competitor_ContactNo = competitorsViewModel.Competitor_ContactNo;
            competitor.Competitor_DoB = competitorsViewModel.Competitor_DoB;
            competitor.Competitor_Email = competitorsViewModel.Competitor_Email;
            competitor.Competitor_Description = competitorsViewModel.Competitor_Description;
            competitor.Competitor_Country = competitorsViewModel.Competitor_Country;
            competitor.Competitor_Gender = competitorsViewModel.Competitor_Gender;
            competitor.Competitor_Website = competitorsViewModel.Competitor_Website;
            competitor.Competitor_Photo = competitorsViewModel.Competitor_Photo;
            return competitor;
        }
    }
}