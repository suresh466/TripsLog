using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TripsLog.Models;
using TripsLog.ViewModels;

namespace TripsLog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TripContext context;

        public HomeController(ILogger<HomeController> logger, TripContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index()
        {
            var trips = context.Trips.ToList();
            return View(trips);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Subhead"] = null;
            return View(new TripDetailsViewModel());
        }

        [HttpPost]
        public IActionResult Add(TripDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Destination"] = model.Destination;
                TempData["StartDate"] = model.StartDate;
                TempData["EndDate"] = model.EndDate;
                TempData["Accommodation"] = model.Accommodation;

                if (!string.IsNullOrEmpty(model.Accommodation))
                {
                    TempData.Keep("Destination");
                    TempData.Keep("StartDate");
                    TempData.Keep("EndDate");
                    TempData.Keep("Accommodation");
                    return RedirectToAction("AddAccommodation");
                }
                TempData.Keep("Destination");
                TempData.Keep("StartDate");
                TempData.Keep("EndDate");
                return RedirectToAction("AddThingsToDo");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddAccommodation()
        {
            ViewData["Subhead"] = TempData["Accommodation"]?.ToString();
            TempData.Keep("Accommodation");
            TempData.Keep("Destination");
            TempData.Keep("StartDate");
            TempData.Keep("EndDate");
            return View(new AccommodationViewModel
            {
                Accommodation = TempData["Accommodation"]?.ToString()
            });
        }

        [HttpPost]
        public IActionResult AddAccommodation(AccommodationViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["AccommodationPhone"] = model.AccommodationPhone;
                TempData["AccommodationEmail"] = model.AccommodationEmail;
                TempData.Keep("Accommodation");
                TempData.Keep("Destination");
                TempData.Keep("StartDate");
                TempData.Keep("EndDate");
                return RedirectToAction("AddThingsToDo");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddThingsToDo()
        {
            ViewData["Subhead"] = TempData["Destination"]?.ToString();
            TempData.Keep("Accommodation");
            TempData.Keep("Destination");
            TempData.Keep("StartDate");
            TempData.Keep("EndDate");
            TempData.Keep("AccommodationPhone");
            TempData.Keep("AccommodationEmail");
            return View(new ThingsToDoViewModel());
        }

        [HttpPost]
        public IActionResult AddThingsToDo(ThingsToDoViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in TempData)
                {
                    Console.WriteLine("here xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                    Console.WriteLine($"{item.Key}: {item.Value}");
                    Console.WriteLine("here xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                }
                var trip = new Trip
                {
                    // these values are required so we don't do null check we handle it in the view
                    Destination = TempData["Destination"].ToString(),
                    StartDate = DateTime.Parse(TempData["StartDate"].ToString()),
                    EndDate = DateTime.Parse(TempData["EndDate"].ToString()),
                    // accommodation data can is optional so we use null-conditionals
                    Accommodation = TempData["Accommodation"]?.ToString(),
                    AccommodationPhone = TempData["AccommodationPhone"]?.ToString(),
                    AccommodationEmail = TempData["AccommodationEmail"]?.ToString(),
                    // we don't need null conditionals here because if no value then automatically null
                    ThingToDo1 = model.ThingToDo1,
                    ThingToDo2 = model.ThingToDo2,
                    ThingToDo3 = model.ThingToDo3
                };
                context.Trips.Add(trip);
                context.SaveChanges();

                TempData.Clear();
                TempData["Message"] = "Trip added successfully!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Cancel()
        {
            TempData.Clear();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
