using System.Globalization;

namespace LearningCourses.Models
{
    public class Material
    {
        public int MaterialId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; } 
        public string FileUrl { get; set; }
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
