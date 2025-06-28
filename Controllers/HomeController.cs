using CgpaWebBaseApp.Models; 
using Microsoft.AspNetCore.Mvc;   // Provides core MVC functionalities (like Controller and IActionResult)
using System.Diagnostics;         // For ErrorViewModel, part of the template
using System.Collections.Generic;


namespace CgpaCalculatorWebApp.Controllers
{
    // This class  Controller inherits from 'Controller', which provides
    // useful methods and properties for handling web requests.
    public class HomeController : Controller
    {
        // ILogger is a built-in ASP.NET Core service for logging messages (e.g., to console, files).
        
        private readonly ILogger<HomeController> _logger;

 
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // When a GET request comes to the URL "/Home" or "/Home/Index",
        // this method is executed. It's responsible for displaying the initial form to the user.
        public IActionResult Index()
        {
            // The constructor of CgpaCalculationViewModel will automatically add one empty Course
            // so the user has an initial row to fill out.
            var model = new CgpaCalculationViewModel();

            // This tells ASP.NET Core to render the "Index.cshtml" view
            // located at Views/Home/Index.cshtml  and pass our 'model' object to it
            return View(model);
        }


        // This action method will be called when the user submits the form.
        // [HttpPost] attribute ensures this method only responds to POST requests
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Calculate(CgpaCalculationViewModel model)
        {
            
            // ModelState.IsValid checks if the data submitted from the form conforms to any
            // Data Annotations (like [Required]) defined on our ViewModel or Model classes.
            // If, for example, the [Required] attribute on List<Course> in CgpaCalculationViewModel
            // fails (meaning no courses were submitted), ModelState.IsValid will be false.
            if (!ModelState.IsValid)
            {
                 // This will display validation error messages to the user.
                // The view will retain the data the user previously entered.
                return View("Index", model);
            }

            //  This Javascript line cleans up empty entries
            // before calculation. It removes any Course object from the list where Name is null/empty
            // AND Score is 0 AND Unit is 0.
            model.Courses.RemoveAll(c => string.IsNullOrWhiteSpace(c.Name) && c.Score == 0 && c.Unit == 0);

            // After removing empty courses, we should re-check if there are any valid courses left.
            // This handles the edge case where a user clicks "Calculate" with an empty form or after removing all entries.
            if (model.Courses == null || model.Courses.Count == 0)
            {
                // Add a model error to display a message to the user.
                ModelState.AddModelError("", "Please enter details for at least one course to calculate CGPA.");
                // Return the view with the model (which might now be empty if all were removed),
                // so the user can see the error and add courses.
                return View("Index", model);
            }

            // Call the CalculateCgpa method on the ViewModel.
            model.CalculateCgpa();
            return View("Index", model);
        }

        // Default actions provided by the MVC template (Error and Privacy)
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
