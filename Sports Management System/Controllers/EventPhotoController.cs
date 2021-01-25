using Sports_Management_System.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sports_Management_System.Controllers
{
    [Authorize(Roles = "ScoreManager")]
    public class EventPhotoController : ApplicationBaseController
    {
        private readonly ApplicationDbContext _context;
        public EventPhotoController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: EventPhoto
        public ActionResult Index(int? id)
        {
            var eventRecords = _context.Event_Photos
                               .Where(e => e.Event_ID == id).ToList();
            ViewBag.EventId = id;
            return View(eventRecords);
        }

        // GET: EventPhoto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventPhoto/Create
        public ActionResult Create(int? eventId)
        {
            var eventDetails = _context.Events.Where(e => e.Event_ID == eventId).FirstOrDefault();
            Event_Photos photo = new Event_Photos()
            {
                Event_ID = eventDetails.Event_ID,
                Event = eventDetails
            };
            return View(photo);
        }

        // POST: EventPhoto/Create
        [HttpPost]
        public ActionResult Create(Event_Photos photo)
        {
            try
            {
                var supportedTypes = new[] { "jpg", "png", "JPG", "PNG" };
                HttpFileCollectionBase hpf = Request.Files;
                HttpPostedFileBase file = hpf[0];

                if (file.ContentLength == 0)
                {
                    ModelState.AddModelError(string.Empty, "Please attach an image.");
                    return View(photo);
                }


                string fname = System.IO.Path.GetFileName(file.FileName.ToString());
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    ModelState.AddModelError(string.Empty, "Please attach only images. Support file types are .jpg and .png");
                    return View(photo);
                }

                if (file != null && file.ContentLength > 0)
                {
                    string pathToNewFolder = Path.Combine(Server.MapPath("~/"), "EventPhotos");
                    DirectoryInfo directory = Directory.CreateDirectory(pathToNewFolder);

                    string filePath = System.IO.Path.Combine(Server.MapPath("~/EventPhotos/") + System.IO.Path.GetFileName(file.FileName.ToString()));

                    file.SaveAs(filePath);
                    photo.Event_Photo = filePath;
                }

                _context.Event_Photos.Add(photo);
                _context.SaveChanges();

                return RedirectToAction("Index", new { id = photo.Event_ID });
            }
            catch
            {
                return View();
            }
        }

        // GET: EventPhoto/Edit/5
        public ActionResult Edit(int id, int eventId)
        {
            var photo = _context.Event_Photos.Where(e => e.Photo_ID == id).FirstOrDefault();
            var eventDetails = _context.Events.Where(e => e.Event_ID == eventId).FirstOrDefault();
            photo.Event = eventDetails;
            return View(photo);
        }

        // POST: EventPhoto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Event_Photos photoEdit)
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
                        return View(photoEdit);
                    }
                }

                if (file != null && file.ContentLength > 0)
                {
                    string pathToNewFolder = Path.Combine(Server.MapPath("~/"), "EventPhotos");
                    DirectoryInfo directory = Directory.CreateDirectory(pathToNewFolder);

                    string filePath = System.IO.Path.Combine(Server.MapPath("~/EventPhotos/") + System.IO.Path.GetFileName(file.FileName.ToString()));

                    file.SaveAs(filePath);
                    photoEdit.Event_Photo = filePath;
                }
                var photo = _context.Event_Photos.Where(e => e.Photo_ID == id).FirstOrDefault();
                if (photoEdit.Event_Photo != null)
                    photo.Event_Photo = photoEdit.Event_Photo;
                photo.Event_PhotoTags = photoEdit.Event_PhotoTags;
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = photo.Event_ID });
            }
            catch
            {
                return View();
            }
        }

        // GET: EventPhoto/Delete/5
        public ActionResult Delete(int id)
        {
            var photo = _context.Event_Photos.Where(e => e.Photo_ID == id).FirstOrDefault();
            return View(photo);
        }

        // POST: EventPhoto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var photo = _context.Event_Photos.Where(e => e.Photo_ID == id).FirstOrDefault();
                _context.Event_Photos.Remove(photo);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = photo.Event_ID });
            }
            catch
            {
                return View();
            }
        }

    }
}
