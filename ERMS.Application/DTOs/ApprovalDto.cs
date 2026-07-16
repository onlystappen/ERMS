using System;

namespace ERMS.Application.DTOs
{
    public class ApprovalDto
    {
        public int ApprovalId { get; set; }
        public int RequestId { get; set; }
        public int ApproverId { get; set; }
        public string Decision { get; set; } = string.Empty;
        public string? Comment { get; set; }
        public DateTime? DecidedAt { get; set; }

        
        public string? ApproverName { get; set; }
    }
}