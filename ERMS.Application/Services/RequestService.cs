using DocumentFormat.OpenXml.Wordprocessing;
using ERMS.Application.Common.Interfaces;
using ERMS.Application.DTOs;
using ERMS.Domain.Entities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ERMS.Application.Services
{
    public class RequestService
    {
        private readonly IApplicationDbContext _context;
        public RequestService(IApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<List<RequestDto>> GetAllRequestsAsync()
        {
            return await _context.Requests.Select( r => new RequestDto{
                    RequestId = r.RequestId,
                    RequestTypeId = r.RequestTypeId,
                    RequesterId = r.RequesterId,
                    Title = r.Title,
                    Description = r.Description,
                    Status = r.Status,
                    Priority = r.Priority,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Amount = r.Amount
            })
            .ToListAsync();
        }
    }
}
