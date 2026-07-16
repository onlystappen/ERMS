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

        public async Task<RequestDto> CreateRequestAsync(RequestDto requestDto)
        {
            var request = new Request
            {
                RequestTypeId = requestDto.RequestTypeId,
                RequesterId = requestDto.RequesterId,
                Title = requestDto.Title,
                Description = requestDto.Description,
                Status = Domain.Enums.RequestStatus.Pending,
                Priority = string.IsNullOrEmpty(requestDto.Priority) ? "Normal" : requestDto.Priority,

                StartDate = requestDto.StartDate,
                EndDate = requestDto.EndDate,
                Amount = requestDto.Amount,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow

            };

            _context.Requests.Add(request);

            await _context.SaveChangesAsync(CancellationToken.None);

            requestDto.RequestId = request.RequestId;
            requestDto.Status = request.Status;

            return requestDto;
        }

        public async Task<bool> UpdateRequestAsync(int id, RequestDto requestDto)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.RequestId == id);
            if(request == null)
            {
                return false;

            }

            request.Title = requestDto.Title;
            request.Description = requestDto.Description;
            request.Status = requestDto.Status;
            request.Priority = requestDto.Priority;
            request.StartDate = requestDto.StartDate;
            request.EndDate = requestDto.EndDate;
            request.Amount = requestDto.Amount;

            request.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(CancellationToken.None);
            return true;

        }

        public async Task<bool> DeleteRequestAsync(int id, RequestDto requestDto)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.RequestId == id);
            if (request == null)
            {
                return false;
            }

            _context.Requests.Remove(request);

            await _context.SaveChangesAsync(CancellationToken.None);
            return true;
        }

        public async Task DeleteRequestAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
