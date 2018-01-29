using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext _context;

        private User ActiveUser 
        {
            get{ return _context.users.Where(u => u.userId == HttpContext.Session.GetInt32("UserId")).FirstOrDefault();}
        }

        public WeddingController(WeddingContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                Dashboard dashData = new Dashboard
                {
                    Weddings = _context.weddings.Include(wed => wed.guests).ToList(),
                    User = ActiveUser
                };
                return View(dashData);
            }
            
        }

        [HttpGet]
        [Route("show/{wedId}")]
        public IActionResult Show(int wedId)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Weddings weddingCouple = _context.weddings
                .Include(w => w.guests)
                .ThenInclude(guest => guest.Guest)
                .Where(wed => wed.weddingId == wedId).SingleOrDefault();
            return View(weddingCouple);
        }


        [HttpGet]
        [Route("plan")]
        public IActionResult Planner()
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        [Route("Create")]
        public IActionResult Create(ViewWedding newWed)
        {
            if(ActiveUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if(ModelState.IsValid)
            {
                Weddings thisWedding = new Weddings
                {
                    bride = newWed.bride,
                    groom = newWed.groom,
                    date = newWed.date,
                    address = newWed.address,
                    userId = ActiveUser.userId
                };
                _context.weddings.Add(thisWedding);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Planner");
            
        }
         [HttpGet]
         [Route("/remove/{wedId}")]
         public IActionResult Remove(int wedId)
         {
             if(ActiveUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

             Weddings thisWedding = _context.weddings.SingleOrDefault(wed => wed.weddingId == wedId);
             _context.weddings.Remove(thisWedding);
             _context.SaveChanges();
             return RedirectToAction("Index");
         }

         [HttpGet]
         [Route("/rsvp/{wedId}")]
         public IActionResult Rsvp(int wedId)
         {
            if(ActiveUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            RSVP thisRsvp = new RSVP();
            {
                Weddings selectedWedding = _context.weddings.SingleOrDefault(w => w.weddingId == wedId);

                thisRsvp.weddingId = selectedWedding.weddingId;
                thisRsvp.userId = ActiveUser.userId;
                thisRsvp.Guest = ActiveUser;
                thisRsvp.Wedding = selectedWedding;
                _context.rsvps.Add(thisRsvp);
                _context.SaveChanges();
            }
           
            return RedirectToAction("Index");
         }

         [HttpGet]
         [Route("/unrsvp/{wedId}")]
         public IActionResult UnRsvp(int wedId)
         {
             if(ActiveUser == null)
            {
                return RedirectToAction("Index", "Home");
            }

            RSVP thisRsvp = _context.rsvps.Where(rem => rem.weddingId == wedId)
            .Where(rem => rem.userId == ActiveUser.userId)
            .SingleOrDefault();
            _context.rsvps.Remove(thisRsvp);
            _context.SaveChanges();
            return RedirectToAction("Index");
         }


        [HttpGet]
        [Route("show")]
        public IActionResult Show()
        {
            return View();
        }

        // [Route("logout")]
        // IActionResult Logout()
        // {
        //     HttpContext.Session.Clear();
        //     return View("Index", "Home");
        // }
    }
}