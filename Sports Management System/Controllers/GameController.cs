using Sports_Management_System.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sports_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]

    public class GameController : ApplicationBaseController
    {
        private readonly ApplicationDbContext _context;

        public GameController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: Game
        public ActionResult Index()
        {
            var games = _context.Games.ToList();
            return View(games);
        }

        // GET: Game/Details/5
        public ActionResult Details(int id)
        {
            var game = _context.Games.Where(g => g.Game_ID == id).FirstOrDefault();
            return View(game);
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        [HttpPost]
        public ActionResult Create(Game game)
        {
            try
            {
                HttpFileCollectionBase hpf = Request.Files;
                HttpPostedFileBase file = hpf[0];
                string fname = System.IO.Path.GetFileName(file.FileName.ToString());
                if (file != null && file.ContentLength > 0)
                {
                    string pathToNewFolder = Path.Combine(Server.MapPath("~/"), "RulesBooklet");
                    DirectoryInfo directory = Directory.CreateDirectory(pathToNewFolder);

                    string filePath = System.IO.Path.Combine(Server.MapPath("~/RulesBooklet/") + System.IO.Path.GetFileName(file.FileName.ToString()));

                    file.SaveAs(filePath);
                    game.Game_RulesBooklet = filePath;
                }

                _context.Games.Add(game);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ee)
            {
                if (ee.InnerException != null)
                    ModelState.AddModelError(string.Empty, ee.InnerException.InnerException.Message);
                //else
                // ModelState.AddModelError(string.Empty, ee.Message);

                return View();
            }
        }


        // GET: Game/Edit/5
        public ActionResult Edit(int id)
        {
            var game = _context.Games.Where(g => g.Game_ID == id).FirstOrDefault();
            return View(game);
        }

        // POST: Game/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Game gameEdit)
        {
            try
            {
                HttpFileCollectionBase hpf = Request.Files;
                HttpPostedFileBase file = hpf[0];
                string fname = System.IO.Path.GetFileName(file.FileName.ToString());
                if (file != null && file.ContentLength > 0)
                {
                    string pathToNewFolder = Path.Combine(Server.MapPath("~/"), "RulesBooklet");
                    DirectoryInfo directory = Directory.CreateDirectory(pathToNewFolder);

                    string filePath = System.IO.Path.Combine(Server.MapPath("~/RulesBooklet/") + System.IO.Path.GetFileName(file.FileName.ToString()));

                    file.SaveAs(filePath);
                    gameEdit.Game_RulesBooklet = filePath;
                }

                // TODO: Add update logic here
                Game game = _context.Games.Where(g => g.Game_ID == id).FirstOrDefault();
                game.Game_Name = gameEdit.Game_Name;
                game.Game_Code = gameEdit.Game_Code;
                game.Game_DurationInHours = gameEdit.Game_DurationInHours;
                game.Game_Description = gameEdit.Game_Description;
                game.Game_RulesBooklet = gameEdit.Game_RulesBooklet;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Delete/5
        public ActionResult Delete(int id)
        {
            var game = _context.Games.Where(g => g.Game_ID == id).FirstOrDefault();
            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var game = _context.Games.Where(g => g.Game_ID == id).FirstOrDefault();
                _context.Games.Remove(game);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Download(string path)
        {

            return File(path, "application/pdf");

        }

    }
}
