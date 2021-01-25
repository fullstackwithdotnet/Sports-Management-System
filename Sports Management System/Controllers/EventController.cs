using Sports_Management_System.Models;
using Sports_Management_System.Models.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sports_Management_System.Controllers
{
    [Authorize(Roles = "ScoreManager")]
    public class EventController : ApplicationBaseController
    {
        private readonly ApplicationDbContext _context;
        public EventController()
        {
            _context = new ApplicationDbContext();

        }

        // GET: Event
        public ActionResult Index()
        {
            var events = _context.Events
                        .Select(s => new EventViewModel
                        {
                            Event = s,
                            Game = s.Game
                        }).ToList();
            return View(events);
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            var eventItem = _context.Events
                .Where(e => e.Event_ID == id)
                .FirstOrDefault();
            return View(eventItem);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            var games = _context.Games.ToList();
            TempData["gamesList"] = games;
            Event ev = new Event()
            {
                EventDate = DateTime.Now.Date,
                EventStartTime = new DateTime(DateTime.Now.TimeOfDay.Ticks, DateTime.Now.Kind)
            };

            EventViewModel viewModel = new EventViewModel()
            {
                Event = ev
            };

            return View(viewModel);
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(EventViewModel eventDetails)
        {
            try
            {
                var supportedTypes = new[] { "jpg", "png", "JPG", "PNG" };
                HttpFileCollectionBase hpf = Request.Files;
                HttpPostedFileBase file = hpf[0];

                if (file.ContentLength == 0)
                {
                    var games = _context.Games.ToList();
                    TempData["gamesList"] = games;
                    ModelState.AddModelError(string.Empty, "Please attach an image.");
                    return View(eventDetails);
                }


                string fname = System.IO.Path.GetFileName(file.FileName.ToString());
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    var games = _context.Games.ToList();
                    TempData["gamesList"] = games;
                    ModelState.AddModelError(string.Empty, "Please attach only images. Support file types are .jpg and .png");
                    return View(eventDetails);
                }

                if (file != null && file.ContentLength > 0)
                {
                    string pathToNewFolder = Path.Combine(Server.MapPath("~/"), "EventPhotos");
                    DirectoryInfo directory = Directory.CreateDirectory(pathToNewFolder);

                    string filePath = System.IO.Path.Combine(Server.MapPath("~/EventPhotos/") + System.IO.Path.GetFileName(file.FileName.ToString()));

                    file.SaveAs(filePath);
                    eventDetails.EventPhoto.Event_Photo = filePath;
                }

                eventDetails.EventPhoto.Event = eventDetails.Event;
                eventDetails.Event.EventStartTime = eventDetails.EventStartTime;
                eventDetails.Event.EventEndTime = eventDetails.EventEndTime;


                _context.Events.Add(eventDetails.Event);
                _context.Event_Photos.Add(eventDetails.EventPhoto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                var games = _context.Games.ToList();
                TempData["gamesList"] = games;
                return View(eventDetails);
            }
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            var games = _context.Games.ToList();
            TempData["gamesList"] = games;
            var eventItem = _context.Events.Where(e => e.Event_ID == id).FirstOrDefault();
            var photo = _context.Event_Photos.Where(p => p.Event_ID == id).FirstOrDefault();

            EventViewModel viewModel = new EventViewModel()
            {
                Event = eventItem,
                EventPhoto = photo
            };
            return View(viewModel);
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EventViewModel eventEdit)
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
                        var games = _context.Games.ToList();
                        TempData["gamesList"] = games;
                        ModelState.AddModelError(string.Empty, "Please attach only images. Support file types are .jpg and .png");
                        return View(eventEdit);
                    }
                }



                if (file != null && file.ContentLength > 0)
                {
                    string pathToNewFolder = Path.Combine(Server.MapPath("~/"), "EventPhotos");
                    DirectoryInfo directory = Directory.CreateDirectory(pathToNewFolder);

                    string filePath = System.IO.Path.Combine(Server.MapPath("~/EventPhotos/") + System.IO.Path.GetFileName(file.FileName.ToString()));

                    file.SaveAs(filePath);
                    eventEdit.EventPhoto.Event_Photo = filePath;
                }

                // TODO: Add update logic here
                Event eventItem = _context.Events.Where(g => g.Event_ID == id).FirstOrDefault();
                eventItem.FeatureEvent = eventEdit.Event.FeatureEvent;
                eventItem.EventVenue = eventEdit.Event.EventVenue;
                eventItem.EventDate = eventEdit.Event.EventDate;
                eventItem.EventStartTime = eventEdit.EventStartTime;
                eventItem.EventEndTime = eventEdit.EventEndTime;
                eventItem.EventDescription = eventEdit.Event.EventDescription;
                eventItem.Game_ID = eventEdit.Event.Game_ID;

                Event_Photos photo = _context.Event_Photos.Where(g => g.Photo_ID == eventEdit.EventPhoto.Photo_ID).FirstOrDefault();
                if (eventEdit.EventPhoto.Event_Photo != null)
                    photo.Event_Photo = eventEdit.EventPhoto.Event_Photo;
                photo.Event_PhotoTags = eventEdit.EventPhoto.Event_PhotoTags;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                var photo = _context.Event_Photos.Where(p => p.Event_ID == id).FirstOrDefault();
                var games = _context.Games.ToList();
                TempData["gamesList"] = games;
                eventEdit.Event.EventStartTime = eventEdit.EventStartTime;
                eventEdit.Event.EventEndTime = eventEdit.EventEndTime;
                eventEdit.EventPhoto = photo;
                return View(eventEdit);
            }
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            var eventItem = _context.Events.Where(e => e.Event_ID == id).FirstOrDefault();
            return View(eventItem);
        }

        // POST: Event/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var eventItem = _context.Events.Where(e => e.Event_ID == id).FirstOrDefault();
                _context.Events.Remove(eventItem);
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
