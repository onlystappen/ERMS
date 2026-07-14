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


        

    }
}
