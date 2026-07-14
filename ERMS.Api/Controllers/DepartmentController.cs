using ERMS.Application.DTOs;
using ERMS.Application.Services;
using Microsoft.AspNetCore.Mvc;
using ERMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ERMS.Application.Common.Interfaces;

namespace ERMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentService _departmentService;
        private readonly IApplicationDbContext _context;

        
        public DepartmentsController(DepartmentService departmentService, IApplicationDbContext context)
        {
            _departmentService = departmentService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> GetAll()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> Create([FromBody] DepartmentDto departmentDto)
        {
            var createdDepartment = await _departmentService.CreateDepartmentAsync(departmentDto);
            return Ok(createdDepartment);
        }

        public async Task<bool> UpdateDepartmentAsync(int id, DepartmentDto departmentDto)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
            {
                return false;
            }

            department.Name = departmentDto.Name;
            department.Description = departmentDto.Description;

            await _context.SaveChangesAsync(CancellationToken.None);
            return true;


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentDto departmentDto)
        {
            bool isUpdated = await _departmentService.UpdateDepartmentAsync(id, departmentDto);

            if (!isUpdated)
            {
                return NotFound("Güncellemek Istenen Departman Bulunamadı.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isDeleted = await _departmentService.DeleteDepartmentAsync(id);

            if (!isDeleted)
            {
                return NotFound("Silinmek Istenen Departman Bulunamadı");
            }

            return NoContent();
        }


    }
}
