using ERMS.Application.Common.Interfaces;
using ERMS.Application.DTOs;
using ERMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ERMS.Application.Services
{
    public class ApprovalService
    {
        private readonly IApplicationDbContext _context;
        private readonly AuditLogService _auditLogService;

        public ApprovalService(IApplicationDbContext context, AuditLogService auditLogService)
        {
            _context = context;
            _auditLogService = auditLogService;
        }

        // ApprovalController.cs Line 29 için
        public async Task<ApprovalDto> CreateApprovalAsync(ApprovalDto approvalDto)
        {
            var approval = new Approval
            {
                RequestId = approvalDto.RequestId,
                ApproverId = approvalDto.ApproverId,
                Decision = "Pending",
                Comment = approvalDto.Comment
            };

            _context.Approvals.Add(approval);
            await _context.SaveChangesAsync(CancellationToken.None);

            await _auditLogService.LogAsync(
                userId: approval.ApproverId,
                action: "ApprovalCreated",
                entityName: "Approval",
                entityId: approval.ApprovalId,
                details: $"Talep ID {approval.RequestId} için onay kaydı oluşturuldu."
            );

            approvalDto.ApprovalId = approval.ApprovalId;
            return approvalDto;
        }

        public async Task<bool> MakeDecisionAsync(int approvalId, string decision, string? comment)
        {
            var approval = await _context.Approvals
                .FirstOrDefaultAsync(a => a.ApprovalId == approvalId);

            if (approval == null) return false;

            approval.Decision = decision;
            approval.Comment = comment;
            approval.DecidedAt = DateTime.UtcNow;

            if (decision.Equals("Approved", StringComparison.OrdinalIgnoreCase))
            {
                approval.ApprovedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(CancellationToken.None);

            await _auditLogService.LogAsync(
                userId: approval.ApproverId,
                action: "ApprovalDecisionMade",
                entityName: "Approval",
                entityId: approval.ApprovalId,
                details: $"Onay kararı verildi: {decision}. Yorum: {comment ?? "Yok"}"
            );

            return true;
        }

        // ApprovalController.cs Line 56 için
        public async Task<List<ApprovalDto>> GetApprovalsByRequestIdAsync(int requestId)
        {
            return await _context.Approvals
                .Where(a => a.RequestId == requestId)
                .Select(a => new ApprovalDto
                {
                    ApprovalId = a.ApprovalId,
                    RequestId = a.RequestId,
                    ApproverId = a.ApproverId,
                    Decision = a.Decision,
                    Comment = a.Comment
                })
                .ToListAsync();
        }
    }
}