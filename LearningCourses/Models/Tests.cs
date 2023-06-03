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
        public List<Questions> Questions { get; set; }

    }

    public class QuestionViewModel
    {
        public int TestId { get; set; }
        public string Content { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }

    public class AnswerViewModel
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }

}
