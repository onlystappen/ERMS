using ERMS.Application.Common.Interfaces;
using ERMS.Application.DTOs;
using ERMS.Domain.Entities;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ERMS.Application.Services
{
    public class DepartmentService
    {
        private readonly IApplicationDbContext _context;

        public DepartmentService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DepartmentDto>> GetAllDepartmentsAsync()
        {
            return await _context.Departments
                .Select(d => new DepartmentDto
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name,
                    Description = d.Description
                })
                .ToListAsync(); 
        }

        public async Task<DepartmentDto> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = new Department 
            {
                Name = departmentDto.Name, 
                Description = departmentDto.Description
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync(CancellationToken.None);

            departmentDto.DepartmentId = department.DepartmentId;
            return departmentDto;
        }
    }
}