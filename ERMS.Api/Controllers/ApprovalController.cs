using ERMS.Application.DTOs;
using ERMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Tüm controller için geçerli JWT Token şartı
    public class ApprovalController : ControllerBase
    {
        private readonly ApprovalService _approvalService;

        public ApprovalController(ApprovalService approvalService)
        {
            _approvalService = approvalService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApprovalDto approvalDto)
        {
            if (approvalDto == null)
            {
                return BadRequest("Geçersiz Onay Verisi");
            }

            var result = await _approvalService.CreateApprovalAsync(approvalDto);

            return Ok(result);
        }

        [HttpPut("{id}/decision")]
        [Authorize(Roles = "Manager, Admin")] // 2. Güvenlik Katmanı: Sadece Yöneticiler ve Adminler karar verebilir
        public async Task<IActionResult> MakeDecision(int id, [FromBody] ApprovalDto decisionDto)
        {
            if (decisionDto == null)
            {
                return BadRequest("Karar Verisi Boş Olamaz");
            }

            var result = await _approvalService.MakeDecisionAsync(id, decisionDto.Decision, decisionDto.Comment);

            if (!result)
            {
                return NotFound("Güncellenmek istenen onay kaydı bulunamadı");
            }

            return Ok(new { message = "Karar başarıyla kaydedildi" });
        }

        [HttpGet("request/{requestId}")]
        public async Task<IActionResult> GetByRequestId(int requestId)
        {
            var approvals = await _approvalService.GetApprovalsByRequestIdAsync(requestId);
            return Ok(approvals);
        }
    }
}