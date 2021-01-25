using Sports_Management_System.Models;
using Sports_Management_System.Models.ViewModels;
using Sports_Management_System.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sports_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CompetitorController : ApplicationBaseController
    {
        private readonly ApplicationDbContext _context;

        public CompetitorController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: Competitor
        public ActionResult Index()
        {
            var competitors = _context.Competitors.ToList();
            return View(competitors);
        }

        // GET: Competitor/Details/5
        public ActionResult Details(int id)
        {
            var com = _context.Competitors.Where(c => c.Competitor_ID == id).FirstOrDefault();
            return View(com);
        }

        // GET: Competitor/Create
        public ActionResult Create()
        {
            var competitorsViewModel = new CompetitorsViewModel { Games = PopulateGameData() };
            return View(competitorsViewModel);
        }

        private ICollection<AssignedGameData> PopulateGameData()
        {
            var games = _context.Games;
            var assignedGames = new List<AssignedGameData>();

            foreach (var item in games)
            {
                assignedGames.Add(new AssignedGameData
                {
                    Game_ID = item.Game_ID,
                    Game_Name = item.Game_Name,
                    Assigned = false
                });
            }
            return assignedGames;
        }

        // POST: Competitor/Create
        [HttpPost]
        public ActionResult Create(CompetitorsViewModel competitorsViewModel)
        {
            try
            {
                var supportedTypes = new[] { "jpg", "png", "JPG", "PNG" };
                HttpFileCollectionBase hpf = Request.Files;
                HttpPostedFileBase file = hpf[0];

                if (file.ContentLength == 0)
                {
                    ModelState.AddModelError(string.Empty, "Please attach an image.");
                    competitorsViewModel.Games = PopulateGameData();
                    return View(competitorsViewModel);
                }


                string fname = System.IO.Path.GetFileName(file.FileName.ToString());
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    ModelState.AddModelError(string.Empty, "Invalid image type. Only allowed .jpj , .png files");
                    competitorsViewModel.Games = PopulateGameData();
                    return View(competitorsViewModel);
                }

                if (file != null && file.ContentLength > 0)
                {
                    string pathToNewFolder = Path.Combine(Server.MapPath("~/"), "CompetitorPhotos");
                    DirectoryInfo directory = Directory.CreateDirectory(pathToNewFolder);

                    string filePath = System.IO.Path.Combine(Server.MapPath("~/CompetitorPhotos/") + System.IO.Path.GetFileName(file.FileName.ToString()));

                    file.SaveAs(filePath);
                    competitorsViewModel.Competitor_Photo = filePath;
                }

                if (competitorsViewModel.Games.All(g => g.Assigned != true))
                {
                    ModelState.AddModelError(string.Empty, "Please select at lease one game");
                    competitorsViewModel.Games = PopulateGameData();
                    return View(competitorsViewModel);
                }

                var competitor = ViewModelHelpers.ToDomainModel(competitorsViewModel);
                AddOrUpdateGames(competitor, competitorsViewModel.Games);
                _context.Competitors.Add(competitor);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ee)
            {
                if (ee.InnerException != null)
                    ModelState.AddModelError(string.Empty, ee.InnerException.InnerException.Message);
                competitorsViewModel.Games = PopulateGameData();
                return View(competitorsViewModel);
            }
        }

        private void AddOrUpdateGames(Competitor competitor, ICollection<AssignedGameData> assignedGames)
        {
            if (assignedGames == null)
                return;

            if (competitor.Competitor_ID != 0)
            {
                // Existing user - drop existing games and add the new ones if any
                foreach (var game in competitor.Games.ToList())
                {
                    competitor.Games.Remove(game);
                }

                foreach (var game in assignedGames.Where(c => c.Assigned))
                {
                    competitor.Games.Add(_context.Games.Find(game.Game_ID));
                }
            }

            else
            {
                foreach (var assignedGame in assignedGames.Where(c => c.Assigned))
                {
                    var game = new Game { Game_ID = assignedGame.Game_ID };
                    _context.Games.Attach(game);
                    competitor.Games.Add(game);
                }
            }
        }

        // GET: Competitor/Edit/5
        public ActionResult Edit(int id)
        {
            var games = _context.Games.ToList();

            var competitor = _context.Competitors.Include("Games").FirstOrDefault(x => x.Competitor_ID == id);
            var competitorsViewModel = ViewModelHelpers.ToViewModel(competitor, games);

            return View(competitorsViewModel);
        }

        // POST: Competitor/Edit/5
        [HttpPost]
        public ActionResult Edit(CompetitorsViewModel competitorsViewModel)
        {
            try
            {
                var supportedTypes = new[] { "jpg", "png", "JPG", "PNG" };

                HttpFileCollectionBase hpf = Request.Files;
                HttpPostedFileBase file = hpf[0];

                string fname = System.IO.Path.GetFileName(file.FileName.ToString());
                if (!String.IsNullOrEmpty(file.FileName))
                {
                    var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ModelState.AddModelError(string.Empty, "Please attach only images. Support file types are .jpg and .png");
                        return View(competitorsViewModel);
                    }
                }

                if (file != null && file.ContentLength > 0)
                {
                    string pathToNewFolder = Path.Combine(Server.MapPath("~/"), "CompetitorPhotos");
                    DirectoryInfo directory = Directory.CreateDirectory(pathToNewFolder);

                    string filePath = System.IO.Path.Combine(Server.MapPath("~/CompetitorPhotos/") + System.IO.Path.GetFileName(file.FileName.ToString()));

                    file.SaveAs(filePath);
                    competitorsViewModel.Competitor_Photo = filePath;
                }

                if (competitorsViewModel.Games.All(g => g.Assigned != true))
                {
                    ModelState.AddModelError(string.Empty, "Please select at lease one game");
                    competitorsViewModel.Games = PopulateGameData();
                    return View(competitorsViewModel);
                }

                var originalCompetitor = _context.Competitors.Find(competitorsViewModel.Competitor_ID);

                if (competitorsViewModel.Competitor_Photo == null)
                    competitorsViewModel.Competitor_Photo = originalCompetitor.Competitor_Photo;

                AddOrUpdateGames(originalCompetitor, competitorsViewModel.Games);
                _context.Entry(originalCompetitor).CurrentValues.SetValues(competitorsViewModel);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ee)
            {
                if (ee.InnerException != null)
                    ModelState.AddModelError(string.Empty, ee.InnerException.InnerException.Message);
                return View(competitorsViewModel);
            }
        }

        // GET: Competitor/Delete/5
        public ActionResult Delete(int id)
        {
            var com = _context.Competitors.Where(c => c.Competitor_ID == id).FirstOrDefault();
            return View(com);
        }

        // POST: Competitor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var com = _context.Competitors.Where(c => c.Competitor_ID == id).FirstOrDefault();
                _context.Competitors.Remove(com);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
