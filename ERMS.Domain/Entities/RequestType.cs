using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Entities
{
    public class RequestType
    {
        public int RequestTypeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public bool RequiresApproval { get; set; }

        public  bool IsActive { get; set; }
    }
}
