using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WudFilmApp.Models;

namespace WudFilmApp.Controllers
{
    public class TestController : Controller
    {
        private readonly WudFilmAppContext _context;

        public TestController(WudFilmAppContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // runs all tests
        public async Task<IActionResult> RunAllTests()
        {
            // movie tests
            var result = await TestAddMovie();

            // showtime tests

            // user tests

            
            return result;  
        }


        // completes a test
        public IActionResult Complete(string message)
        {
            SetTestResult(message);
            var view = View("Index");
            return view;
        }



        public async Task<IActionResult> TestAddMovie()
        {
            // create a new movie to be added
            var title = $"test_{DateTime.Now}";
            var result = "";
            var movie = new Movie()
            {
                Title = title,
                Subcommittee = Subcommittee.Alternative
            };

            try
            {
                var entry = _context.Add(movie);
                await _context.SaveChangesAsync();

                result = $"Truck added successfully.";

                _context.Remove(entry.Entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result = $"Failed to add truck {e.Message};";

            }

            return RedirectToAction("Complete", routeValues: new { message = result });

        }

        private void SetTestResult(string result)
        {
            if (ViewData.ContainsKey("TestResult"))
            {
                ViewData.Remove("TestResult");
            }

            ViewData.Add("TestResult", result);
        }
    }

}