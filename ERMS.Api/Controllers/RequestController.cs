using ERMS.Application.Common.Interfaces;
using ERMS.Application.DTOs;
using ERMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Tüm talep işlemleri için JWT Token zorunlu
    public class RequestController : ControllerBase
    {
        private readonly RequestService _requestService;
        private readonly IApplicationDbContext _context;

        public RequestController(RequestService requestService, IApplicationDbContext context)
        {
            _requestService = requestService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            var requests = await _requestService.GetAllRequestsAsync();
            return Ok(requests);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] RequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Gönderilen talep verisi boş olamaz");
            }

            var createdRequest = await _requestService.CreateRequestAsync(requestDto);
            return Ok(createdRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] RequestDto requestDto)
        {
            var isUpdated = await _requestService.UpdateRequestAsync(id, requestDto);
            if (!isUpdated)
            {
                return NotFound($"{id} numaralı talep bulunamadı");
            }
            return Ok(new { message = "Talep Başarıyla Güncellendi" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestAsync(int id)
        {
            var request = await _context.Requests
                .FirstOrDefaultAsync(r => r.RequestId == id);

            if (request == null)
            {
                return NotFound($"{id} numaralı talep bulunamadı.");
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync(CancellationToken.None);

            return Ok(new { message = "Talep başarıyla silindi." });
        }
    }
}