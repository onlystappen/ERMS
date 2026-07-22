using ERMS.Application.DTOs;
using ERMS.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public DepartmentsController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _departmentService.GetAllDepartmentsAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentDto dto)
        {
            var result = await _departmentService.CreateDepartmentAsync(dto);
            return Ok(result);
        }

        // --- İŞTE EKSİK VEYA HATALI OLAN YER BURASI BRA ---
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartmentAsync(int id, [FromBody] DepartmentDto dto)
        {
            var result = await _departmentService.UpdateDepartmentAsync(id, dto);
            if (!result) return NotFound();
            return Ok();
        }
    }
}