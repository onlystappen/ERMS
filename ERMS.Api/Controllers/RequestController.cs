using ERMS.Application.DTOs;
using ERMS.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly RequestService _requestService;

        public RequestController(RequestService requestService)
        {
            _requestService = requestService;
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
            if(requestDto == null)
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
    }
}
