using ERMS.Application.Common.Interfaces;
using ERMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Services
{
    public class AuditLogService
    {
        private readonly IApplicationDbContext _context;

        public AuditLogService(IApplicationDbContext context) 
        {
            _context = context;

        }

        public async Task LogAsync(int? userId,string action, string entityName, int entityId, string details)
        {
            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                Details = details,
                Timestamp = DateTime.UtcNow

            };
            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
