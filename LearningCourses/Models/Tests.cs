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
    public class TestResultViewModel
    {
        public string TestTitle { get; set; }
        public double CorrectPercentage { get; set; }
        public int CorrectCount { get; set; }
        public int IncorrectCount { get; set; }
        public int Grade { get; set; }
    }
    public class QuestionViewModel
    {
        public int TestId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public List<AnswerViewModel> Answers { get; set; } = new List<AnswerViewModel>();
        public int? SelectedAnswerId { get; set; } // Идентификатор выбранного ответа
    }


    public class AnswerViewModel
    {
        public string Content { get; set; }
        public string IsSelected { get; set; }
        public string IsCorrect { get; set; }
        public bool IsCorrectAnswerSelected { get; set; }
    }




}
