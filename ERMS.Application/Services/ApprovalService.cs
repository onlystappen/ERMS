using ERMS.Application.Common.Interfaces;
using ERMS.Application.DTOs;
using ERMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ERMS.Domain.Enums;
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

        public async Task<bool> MakeDecisionAsync(int approvalId, string decision, string? comment)
        {
            var approval = await _context.Approvals
            .Include(a => a.Request)
                .FirstOrDefaultAsync(a => a.ApprovalId == approvalId);

            if (approval == null)
            {
                return false;
            }

            approval.Decision = decision;
            approval.Comment = comment;
            approval.DecidedAt = DateTime.UtcNow;

            
            if (approval.Request != null)
            {
                if (decision.Equals("Approved", StringComparison.OrdinalIgnoreCase))
                {
                    approval.Request.Status = RequestStatus.Approved; 
                }
                else if (decision.Equals("Rejected", StringComparison.OrdinalIgnoreCase))
                {
                    approval.Request.Status = RequestStatus.Rejected; 
                }
            }

            await _context.SaveChangesAsync(CancellationToken.None);
            return true;

        }


    }
}
