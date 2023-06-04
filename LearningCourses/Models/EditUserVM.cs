using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningCourses.Models
{
    public class EditUserVM
    {
        public EditUserVM()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }

        public string Id { get; set; }

       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Patronymic { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

       

        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }
    }
}

