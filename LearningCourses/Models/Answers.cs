﻿namespace LearningCourses.Models
{
    public class Answers
    {
        public int AnswerId { get; set; }
        public string Content { get; set; }
        public string IsCorrect { get; set; }
        public int QuestionId { get; set; }

        public Questions Questions { get; set; }

    }
}