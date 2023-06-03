using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningCourses.Models
{
    public class Tests
    {
        [Key]
        public int TestId { get; set; }
        public int MaterialId { get; set; }
        public string Title { get; set; }

        public ICollection<Grades> Grades { get; set; }
        public Material Material { get; set; }
        public ICollection<Questions> Questions { get; set; }

    }
}
