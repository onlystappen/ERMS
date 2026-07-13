using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.DTOs
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

    }
}
