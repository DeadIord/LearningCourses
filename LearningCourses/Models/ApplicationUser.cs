using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace LearningCourses.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public byte[] ProfilePicture { get; set; }
        public DateTime Date_birth { get; set; }
    }
}
