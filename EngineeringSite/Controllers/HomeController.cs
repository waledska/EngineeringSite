using EngineeringSite.Data;
using EngineeringSite.Models;
using EngineeringSite.viewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EngineeringSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext db;
        public result result = new result();

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext _context)
        {
            _logger = logger;
            db = _context;
            result.Listservice = db.services.ToList();
        }

        public IActionResult Index()
        {
            // bag for layout
            ViewBag.Services = db.services.ToList();

            var comments = db.reviews.Where(x => x.showReview == true).ToList();

            return View(comments);
        }

        public IActionResult About()
        {
            // bag for layout
            ViewBag.Services = db.services.ToList();
            return View();
        }

        public IActionResult ContactUs()
        {
            // bag for layout
            ViewBag.Services = db.services.ToList();
            return View();
        }

        public IActionResult Services()
        {
            // bag for layout
            ViewBag.Services = db.services.ToList();
            // to get services
            var serv = db.services.ToList();
            return View(serv);
        }

        
        public IActionResult TeamMembers()
        {
            // bag for layout
            ViewBag.Services = db.services.ToList();
            // to get  teammembers
            var teams = db.TeamMembers.ToList();
            return View(teams);
        }
        public IActionResult Portfolio()
        {
            // bag for layout
            ViewBag.Services = db.services.ToList();

            // get service by the id
            var servs = db.services
                        .Include(x => x.jobsFinisheds)
                        .ThenInclude(jf => jf.photo)
                        .ToList();

            return View(servs);
        }
        public IActionResult service(int ID)
        {
            // bag for layout
            ViewBag.Services = db.services.ToList();
            // to get jobs finished on this category
            //ViewBag.servToPhoto = db.services
            //                         .Include(x => x.jobsFinisheds)
            //                         .ThenInclude(jf => jf.photo)
            //                         .FirstOrDefault(s => s.id == ID);
            
            // get service by the id
            var serv = db.services
                        .Include(x => x.jobsFinisheds)
                        .ThenInclude(jf => jf.photo)
                        .FirstOrDefault(s => s.id == ID);
            return View(serv);
        }

        public IActionResult JobDetails(int ID)
        {
            // bag for layout
            ViewBag.Services = db.services.ToList();

            // get service by the id
            var jobsFinished = db.jobsFinisheds
                        .Include(f => f.photo)
                        .FirstOrDefault(s => s.id == ID);

            //.Include(x => x.service)
            //.ThenInclude(jf => jf.photo)

            return View(jobsFinished);
        }

        [HttpPost]
        public IActionResult SaveContact(message NewMessage)
        {
            // bag for layout
            ViewBag.Services = db.services.ToList();
            NewMessage.time = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.messages.Add(NewMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("ContactUs", NewMessage);
        }

        [HttpPost]
        public IActionResult SaveReview(review NewReview)
        {
            // bag for layout
            ViewBag.Services = db.services.ToList();

            // save the NewReview
                NewReview.showReview = false;
                db.reviews.Add(NewReview);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}