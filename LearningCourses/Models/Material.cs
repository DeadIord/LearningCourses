
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LearningCourses.Models
{
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }

        [Required(ErrorMessage = "Укажите название!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Укажите описание!")]
        public string Contents { get; set; }
        public byte[] File { get; set; }
        public string FileUrl { get; set; }
        public string ApplicationUserId { get; set; }

        public ICollection<Tests> Tests { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
