
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LearningCourses.Models
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public byte[] File { get; set; }
        public string FileUrl { get; set; }
        public string ApplicationUserId { get; set; }

        public ICollection<Tests> Tests { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
