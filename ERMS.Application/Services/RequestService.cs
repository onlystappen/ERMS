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

        public async Task<List<RequestDto>> GetAllRequestsAsync()
        {
            return await _context.Requests
                .Select(r => new RequestDto
                {
                    RequestId = r.RequestId,
                    Title = r.Title,
                    Description = r.Description,
                    RequesterId = r.RequesterId,
                    RequestTypeId = r.RequestTypeId,
                    Status = r.Status
                })
                .ToListAsync();
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
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync(CancellationToken.None);

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

        public async Task<bool> UpdateRequestAsync(int id, RequestDto dto)
        {
            var request = await _context.Requests
                .FirstOrDefaultAsync(r => r.RequestId == id);

            if (request == null) return false;

            request.Title = dto.Title;
            request.Description = dto.Description;
            request.RequestTypeId = dto.RequestTypeId;
            request.Status = dto.Status;
            request.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(CancellationToken.None);

            await _auditLogService.LogAsync(
                userId: dto.RequesterId,
                action: "RequestUpdated",
                entityName: "Request",
                entityId: request.RequestId,
                details: $"Talep güncellendi: '{request.Title}'"
            );

            return true;
        }
    }
}