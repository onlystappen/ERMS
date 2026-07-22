using ERMS.Application.Common.Interfaces;
using ERMS.Application.DTOs;
using ERMS.Domain.Entities;
using ERMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ERMS.Application.Services
{
    public class RequestService
    {
        private readonly IApplicationDbContext _context;
        private readonly AuditLogService _auditLogService; 

        public RequestService(IApplicationDbContext context, AuditLogService auditLogService)
        {
            _context = context;
            _auditLogService = auditLogService;
        }

        public async Task<RequestDto> CreateRequestAsync(RequestDto dto)
        {
            var request = new Request
            {
                Title = dto.Title,
                Description = dto.Description,
                RequesterId = dto.RequesterId,
                RequestTypeId = dto.RequestTypeId,
                Status = RequestStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync(CancellationToken.None);

            // Talep açıldığı an log kaydı!
            await _auditLogService.LogAsync(
                userId: dto.RequesterId,
                action: "RequestCreated",
                entityName: "Request",
                entityId: request.RequestId,
                details: $"Yeni talep oluşturuldu: '{request.Title}'"
            );

            dto.RequestId = request.RequestId;
            return dto;
        }

        
    }
}