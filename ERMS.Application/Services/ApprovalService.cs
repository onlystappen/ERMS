using ERMS.Application.Common.Interfaces;
using ERMS.Application.DTOs;
using ERMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Services
{
    public class ApprovalService
    {
        private readonly IApplicationDbContext _context;

        public ApprovalService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApprovalDto> CreateApprovalAsync(ApprovalDto approvalDto)
        {
            var approval = new Approval
            {
                RequestId = approvalDto.RequestId,
                ApproverId = approvalDto.ApproverId,
                Decision = "Pending",
                Comment = approvalDto.Comment,
                DecidedAt = null
            };

            _context.Approvals.Add(approval);

            await _context.SaveChangesAsync(CancellationToken.None);

            approvalDto.ApprovalId = approval.ApprovalId;
            approvalDto.Decision = approval.Decision;
            return approvalDto;
        }
    }
}
