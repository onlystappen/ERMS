using ERMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.DTOs
{
    public class RequestDto
    {
        public int RequestId { get; set; }

        public int RequestTypeId { get; set; }

        public int RequesterId { get; set; }

        public string Title { get; set; } 

        public string Description { get; set; } 

        public RequestStatus Status { get; set; }

        public string Priority { get; set; } 

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? Amount { get; set; }
    }
}
