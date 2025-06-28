using System.Collections.Generic; // Required for List<T>
using System.ComponentModel.DataAnnotations; // Required for [Required] and other data annotations

namespace CgpaWebBaseApp.Models
{
    // This ViewModel is designed to hold all the data needed for the CGPA calculation page.
    // It serves two purposes:
    // 1. To send initial data (like an empty course form) from the Controller to the View.
    // 2. To receive submitted data (the list of courses) from the View back to the Controller.
    // 3. To hold the calculated results (CGPA, TotalUnits, TotalGradePoints) to display back to the View.
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