using ERMS.Application.Common.Interfaces;
using ERMS.Application.DTOs;
using ERMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERMS.Application.Services
{
    public class ApprovalService
    {
        private readonly IApplicationDbContext _context;
        private readonly AuditLogService _auditLogService; // 1. Log Servisi eklendi

        public ApprovalService(IApplicationDbContext context, AuditLogService auditLogService)
        {
            _context = context;
            _auditLogService = auditLogService;
        }

        public async Task<bool> MakeDecisionAsync(int approvalId, string decision, string? comment)
        {
            var approval = await _context.Approvals
                .FirstOrDefaultAsync(a => a.ApprovalId == approvalId);

            if (approval == null) return false;

            approval.Decision = decision;
            approval.Comment = comment;
            approval.ApprovedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(CancellationToken.None);

            
            await _auditLogService.LogAsync(
                userId: approval.ApproverId,
                action: "ApprovalDecisionMade",
                entityName: "Approval",
                entityId: approval.ApprovalId,
                details: $"Talep için karar verildi: {decision}. Yorum: {comment ?? "Yok"}"
            );

            return true;
        }

        
    }
}