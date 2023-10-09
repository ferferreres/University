using System.ComponentModel.DataAnnotations;

namespace University.BL.DTOs
{
    public enum Grade
    {
        A, B, C, D, E
    }
    public class EnrollmentDTO
    {
        public int EnrollmentID { get; set; }
        [Required(ErrorMessage = "The field CourseID is required")]
        public int CourseID { get; set; }
        [Required(ErrorMessage = "The field StudentID is required")]
        public int StudentID { get; set; }
        [Required(ErrorMessage = "The field Grade is required")]
        public Grade Grade { get; set; }
        public CourseDTO Course { get; set; }
        public StudentDTO Student { get; set; }
    }
}
