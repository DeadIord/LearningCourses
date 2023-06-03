using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningCourses.Models
{
    public class Questions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Добавьте эту аннотацию

        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public string Content { get; set; }

        public Tests Test { get; set; }
        public ICollection<Answers> Answers { get; set; }
    }
}
