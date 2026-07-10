using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Domain.Entities
{
    public class RequestHistory
    {
        public int RequestHistoryId { get; set; }
        public int RequestId { get; set; }

        public int ChangedById { get; set; }
        public string OldStatus { get; set; } = string.Empty;
        public string NewStatus { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; }


    }
}
