namespace LearningCourses.Models
{
    public class Grades
    {
        public int GradeId { get; set; }
        public int GradeValue { get; set; }
        public string ApplicationUserId { get; set; }
        public int TestId { get; set; }

        public Tests Tests { get; set; }

        public ApplicationUser ApplicationUser { get;set;}

    }
}
