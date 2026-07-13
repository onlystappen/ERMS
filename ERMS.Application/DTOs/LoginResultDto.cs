using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.DTOs
{
    public class LoginResultDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

    }
}
