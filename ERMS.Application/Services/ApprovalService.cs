using ERMS.Application.Common.Interfaces;
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


    }
}
