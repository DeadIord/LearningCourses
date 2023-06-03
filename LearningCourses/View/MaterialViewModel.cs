using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LearningCourses.View
{
    public class MaterialViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Contents { get; set; }

        public IFormFile File { get; set; }

        public string FileUrl { get; set; }
    }

}
