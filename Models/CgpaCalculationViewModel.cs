using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CgpaWebBaseApp.Models
{
        public class CgpaCalculationViewModel
    {

        [Required(ErrorMessage = "Please add at least one course.")]
        public List<Course> Courses { get; set; } = new List<Course>(); // Holds a list of Course objects entered by the user.
                                                                        
        // Properties to store and display the final calculated results.
        public double Cgpa { get; set; }
        public int TotalUnits { get; set; }
        public int TotalGradePoints { get; set; }

        // Constructor: This special method is called when a new instance of CgpaCalculationViewModel is created.
        public CgpaCalculationViewModel()
        {
            // When the page first loads, we want to provide the user with at least one empty
            // course entry field so they can start typing immediately without needing to click "Add Course" first.
            // This ensures there's always one 'Course' object in the 'Courses' list.
            Courses.Add(new Course());
        }

     
        /// Performs the CGPA calculation based on the Courses list within this ViewModel.
        /// This encapsulates the calculation logic within the ViewModel itself, making the Controller cleaner.
        public void CalculateCgpa()
        {
            TotalGradePoints = 0; // Reset totals before calculation
            TotalUnits = 0;

            // Loop through each course submitted by the user
            foreach (var course in Courses)
            {
                // Include courses with valid units and scores in the calculation.
                // This handles cases where a user might leave a field empty or enter invalid data.
                if (course.Unit > 0 && course.Score >= 0 && course.Score <= 100)
                {
                    TotalGradePoints += course.GradePoint * course.Unit; // Accumulate weighted grade points
                    TotalUnits += course.Unit;                           // Accumulate total units
                }
            }

            // Calculate CGPA, preventing division by zero if no valid units were entered.
            Cgpa = TotalUnits > 0 ? (double)TotalGradePoints / TotalUnits : 0.0;
        }
    }
}
