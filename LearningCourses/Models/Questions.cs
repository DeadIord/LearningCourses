using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningCourses.Models
{
    public class Questions
    {
        [Key]
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public string Content { get; set; }

        public Tests Tests { get; set; }
        public ICollection<Answers> Answers { get; set; }
    }
}
