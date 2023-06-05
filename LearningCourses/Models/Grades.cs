using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace LearningCourses.Models
{
    public class Grades
    {
        [Key]
        public int GradeId { get; set; }

        public int GradeValue { get; set; }
        public string ApplicationUserId { get; set; }
        public int TestId { get; set; }
        public DateTime DateOfPassage { get; set; }
        public Tests Tests { get; set; }

        public ApplicationUser ApplicationUser { get;set;}

    }
}
