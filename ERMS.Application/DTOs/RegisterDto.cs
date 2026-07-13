using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.DTOs
{
    public class RegisterDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }
        public int DepartmentId { get; set; }
        public int ManagerId { get; set; }


    }
}
