using System.Collections.Generic;

namespace LearningCourses.Models
{
    public class Tests
    {
        public int TestId { get; set; }
        public int MaterialId { get; set; }
        public string Title { get; set; }

        public ICollection<Grades> Grades { get; set; }
        public ICollection<Questions> Questions { get; set; }

    }
}
