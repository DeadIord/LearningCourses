using System.Collections.Generic;

namespace LearningCourses.Models
{
    public class UserRolesVM
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname{ get; set; }

        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
