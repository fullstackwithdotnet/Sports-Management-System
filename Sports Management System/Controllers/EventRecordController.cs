using Sports_Management_System.Models;
using Sports_Management_System.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Sports_Management_System.Controllers
{
    [Authorize(Roles = "ScoreManager")]
    public class EventRecordController : ApplicationBaseController
    {
        private readonly ApplicationDbContext _context;
        public EventRecordController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: EventRecord
        public ActionResult Index(int? id)
        {
            var eventRecords = _context.Event_Records
                                .Where(e => e.Event_ID == id)
                                 .Select(s => new EventRecordViewModel
                                 {
                                     EventRecord = s,
                                     Competitor = s.Competitor,
                                     Event = s.Event
                                 }).ToList();
            ViewBag.EventId = id;
            return View(eventRecords);
        }

        // GET: EventRecord/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventRecord/Create
        public ActionResult Create(int? eventId)
        {
            var eventDetails = _context.Events.Where(e => e.Event_ID == eventId).FirstOrDefault();
            int gameID = eventDetails.Game_ID;
            var game = _context.Games.Include("Competitors").FirstOrDefault(x => x.Game_ID == gameID);

            var competitors = game.Competitors.ToList();
            TempData["competitorsList"] = competitors;

            Event_Record record = new Event_Record()
            {
                Event_ID = eventDetails.Event_ID,
                Event = eventDetails
            };



            return View(record);
        }

        // POST: EventRecord/Create
        [HttpPost]
        public ActionResult Create(Event_Record record)
        {
            try
            {
                if (record.Competitor_Position == 0 || record.Competitor_Position == 0)
                {
                    var eventDetails = _context.Events.Where(e => e.Event_ID == record.Event_ID).FirstOrDefault();
                    int gameID = eventDetails.Game_ID;
                    var game = _context.Games.Include("Competitors").FirstOrDefault(x => x.Game_ID == gameID);

                    var competitors = game.Competitors.ToList();
                    TempData["competitorsList"] = competitors;

                    ModelState.AddModelError(string.Empty, "Please select position and medal");
                    return View(record);
                }
                _context.Event_Records.Add(record);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = record.Event_ID });
            }
            catch
            {
                return View();
            }
        }

        // GET: EventRecord/Edit/5
        public ActionResult Edit(int id, int eventId)
        {
            var eventRecordDetails = _context.Event_Records.Where(e => e.ID == id).FirstOrDefault();
            var eventDetails = _context.Events.Where(e => e.Event_ID == eventId).FirstOrDefault();

            eventRecordDetails.Event = eventDetails;

            int gameID = eventDetails.Game_ID;
            var game = _context.Games.Include("Competitors").FirstOrDefault(x => x.Game_ID == gameID);
            var competitors = game.Competitors.ToList();
            TempData["competitorsList"] = competitors;
            return View(eventRecordDetails);
        }

        // POST: EventRecord/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Event_Record recordEdit)
        {
            try
            {
                var record = _context.Event_Records.Where(r => r.ID == id).FirstOrDefault();
                record.Competitor_ID = recordEdit.Competitor_ID;
                record.Competitor_Position = recordEdit.Competitor_Position;
                record.Competitor_Medal = recordEdit.Competitor_Medal;
                record.WorldRecord = recordEdit.WorldRecord;
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = record.Event_ID });
            }
            catch
            {
                return View();
            }
        }

        // GET: EventRecord/Delete/5
        public ActionResult Delete(int id)
        {
            var eventRecord = _context.Event_Records
                              .Where(e => e.ID == id)
                               .Select(s => new EventRecordViewModel
                               {
                                   EventRecord = s,
                                   Competitor = s.Competitor,
                                   Event = s.Event
                               }).FirstOrDefault();
            return View(eventRecord);
        }

        // POST: EventRecord/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var record = _context.Event_Records.Where(e => e.ID == id).FirstOrDefault();
                _context.Event_Records.Remove(record);
                _context.SaveChanges();
                return RedirectToAction("Index", new { id = record.Event_ID });
            }
            catch
            {
                return View();
            }
        }
    }
}
