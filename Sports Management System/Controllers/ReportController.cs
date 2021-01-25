using Sports_Management_System.Models;
using Sports_Management_System.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sports_Management_System.Controllers
{
    [Authorize(Roles = "ScoreManager")]
    public class ReportController : ApplicationBaseController
    {
        private readonly ApplicationDbContext _context;

        public ReportController()
        {
            _context = new ApplicationDbContext();

        }
        // GET: Report
        public ActionResult Index()
        {


            var events = (from e in _context.Events
                          join er in _context.Event_Records on e.Event_ID equals er.Event_ID
                          join c in _context.Competitors on er.Competitor_ID equals c.Competitor_ID
                          orderby e.Event_ID ascending
                          select new { e, c.Competitor_Country, er.Competitor_Medal } into x
                          group x by new { x.e });

            List<EventDataViewModel> eventData = new List<EventDataViewModel>();
            foreach (var eventGroup in events)
            {
                var eventListGrouped = eventGroup
                            .GroupBy(c => c.Competitor_Country)
                            .Select(e => new EventDataViewModel
                            {
                                Country = e.Key,
                                GoldMedals = e.Where(m => m.Competitor_Medal == Medals.Gold).Count(),
                                SilverMedals = e.Where(m => m.Competitor_Medal == Medals.Silver).Count(),
                                BronzMedals = e.Where(m => m.Competitor_Medal == Medals.Bronze).Count(),
                                TotalMedals = e.Count(),
                                Event = eventGroup.Key.e
                            }).ToList();

                eventData.AddRange(eventListGrouped);
            }



            var wRecords = (from er in _context.Event_Records
                            join e in _context.Events on er.Event_ID equals e.Event_ID
                            join g in _context.Games on e.Game_ID equals g.Game_ID
                            join c in _context.Competitors on er.Competitor_ID equals c.Competitor_ID
                            where er.WorldRecord == true
                            select new WorldRecordsViewModel
                            {
                                Name = c.Competitor_Name,
                                Country = c.Competitor_Country,
                                Game = g.Game_Name
                            }).ToList();


            ReportsViewModel reportsViewModel = new ReportsViewModel()
            {
                EventData = eventData,
                WorldRecords = wRecords

            };
            return View(reportsViewModel);
        }


        public ActionResult Search()
        {
            var eventsList = _context.Events.ToList();
            AdvanceSearchViewModel viewModel = new AdvanceSearchViewModel()
            {
                Events = eventsList
            };

            return View(viewModel);
        }

        [HttpPost]
        public PartialViewResult Search(int eventId, string searchType)
        {
            if (searchType.Equals("photos"))
            {
                if (eventId == 0)
                {


                    var photos = (from ep in _context.Event_Photos
                                  join e in _context.Events on ep.Event_ID equals e.Event_ID
                                  select new AdvanceSearchViewModel
                                  {
                                      Event = e,
                                      Photos = ep
                                  }).ToList();

                    return PartialView("SearchPhotosPartial", photos);
                }
                else
                {
                    var photos = (from ep in _context.Event_Photos
                                  join e in _context.Events on ep.Event_ID equals e.Event_ID
                                  where e.Event_ID == eventId
                                  select new AdvanceSearchViewModel
                                  {
                                      Event = e,
                                      Photos = ep
                                  }).ToList();

                    return PartialView("SearchPhotosPartial", photos);
                }
            }
            else
            {

                if (eventId == 0)
                {
                    var events = (from e in _context.Events
                                  join er in _context.Event_Records on e.Event_ID equals er.Event_ID
                                  join c in _context.Competitors on er.Competitor_ID equals c.Competitor_ID
                                  orderby e.Event_ID ascending
                                  select new { e, c.Competitor_Country, er.Competitor_Medal } into x
                                  group x by new { x.e });



                    List<EventDataViewModel> eventData = new List<EventDataViewModel>();
                    foreach (var eventGroup in events)
                    {
                        var eventListGrouped = eventGroup
                                    .GroupBy(c => c.Competitor_Country)
                                    .Select(e => new EventDataViewModel
                                    {
                                        Country = e.Key,
                                        GoldMedals = e.Where(m => m.Competitor_Medal == Medals.Gold).Count(),
                                        SilverMedals = e.Where(m => m.Competitor_Medal == Medals.Silver).Count(),
                                        BronzMedals = e.Where(m => m.Competitor_Medal == Medals.Bronze).Count(),
                                        TotalMedals = e.Count(),
                                        Event = eventGroup.Key.e
                                    }).ToList();

                        eventData.AddRange(eventListGrouped);
                    }


                    ReportsViewModel reportsViewModel = new ReportsViewModel()
                    {
                        EventData = eventData,
                    };

                    return PartialView("SearchEventsPartial", reportsViewModel);
                }

                else
                {
                    var events = (from e in _context.Events
                                  join er in _context.Event_Records on e.Event_ID equals er.Event_ID
                                  join c in _context.Competitors on er.Competitor_ID equals c.Competitor_ID
                                  where e.Event_ID == eventId
                                  orderby e.Event_ID ascending
                                  select new { e, c.Competitor_Country, er.Competitor_Medal } into x
                                  group x by new { x.e });



                    List<EventDataViewModel> eventData = new List<EventDataViewModel>();
                    foreach (var eventGroup in events)
                    {
                        var eventListGrouped = eventGroup
                                    .GroupBy(c => c.Competitor_Country)
                                    .Select(e => new EventDataViewModel
                                    {
                                        Country = e.Key,
                                        GoldMedals = e.Where(m => m.Competitor_Medal == Medals.Gold).Count(),
                                        SilverMedals = e.Where(m => m.Competitor_Medal == Medals.Silver).Count(),
                                        BronzMedals = e.Where(m => m.Competitor_Medal == Medals.Bronze).Count(),
                                        TotalMedals = e.Count(),
                                        Event = eventGroup.Key.e
                                    }).ToList();

                        eventData.AddRange(eventListGrouped);
                    }


                    ReportsViewModel reportsViewModel = new ReportsViewModel()
                    {
                        EventData = eventData,
                    };

                    return PartialView("SearchEventsPartial", reportsViewModel);
                }

            }



        }



        // GET: Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
