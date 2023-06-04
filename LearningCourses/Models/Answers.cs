using System.ComponentModel.DataAnnotations;

namespace LearningCourses.Models
{
    public class Answers 
    {
        [Key]
        public int AnswerId { get; set; }
        public string Content { get; set; }
        public string IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public Questions Question { get; set; }
    }
}
