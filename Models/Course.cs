namespace CgpaWebBaseApp.Models
{
    // This class represents a single course.
    // holding data for one academic course.
    public class Course
    {
        public string Name { get; set; } // For Course Name (e.g., "Mathematics")
        public int Score { get; set; }   // For Score (e.g., 75)
        public int Unit { get; set; }    // For Course Unit (e.g., 3)

        public string GradeLetter => GetGradeLetter(Score); // Uses the static method below
        public int GradePoint => GetGradePoint(Score);     // Uses the static method below

      
        /// Calculates the grade letter based on the given score.
        /// And returns the corresponding grade letter (A, B, C, D, E, F).
        public static string GetGradeLetter(int score)
        {
            if (score >= 70) return "A";
            else if (score >= 60) return "B";
            else if (score >= 50) return "C";
            else if (score >= 45) return "D";
            else if (score >= 40) return "E";
            else return "F";
        }

        /// Calculates the grade point based on the given score.
 
        /// and returns the corresponding grade point (5, 4, 3, 2, 1, 0).
        public static int GetGradePoint(int score)
        {
            if (score >= 70) return 5;
            else if (score >= 60) return 4;
            else if (score >= 50) return 3;
            else if (score >= 45) return 2;
            else if (score >= 40) return 1;
            else return 0;
        }
    }
}