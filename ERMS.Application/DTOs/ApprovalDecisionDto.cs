using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.DTOs
{
    public class ApprovalDecisionDto
    {
        public string Decision { get; set; } = string.Empty;

        public string? Comment { get; set; }
    }
}
