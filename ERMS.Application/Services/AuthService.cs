using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERMS.Application.Common.Interfaces;
using ERMS.Application.DTOs;
using ERMS.Domain.Entities;
using ERMS.Domain.Enums;

namespace ERMS.Application.Services
{
    public class AuthService
    {
        private readonly IApplicationDbContext _context;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthService(IApplicationDbContext context, IJwtTokenGenerator tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginResultDto?> LoginAsync(LoginDto loginDto)
        {
            // Kullanıcıyı e-posta ile veritabanından çekiyoruz
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || user.PasswordHash != loginDto.Password)
            {
                return null;
            }

            // Token üreticiye User nesnesini veriyoruz. 
            // Generator içerisinde ClaimTypes.Role alanına user.Role.ToString() yazıldığından emin olunmalıdır.
            var token = _tokenGenerator.GenerateToken(user);

            return new LoginResultDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token,
            };
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _context.Users.AnyAsync(u => u.Email == registerDto.Email);
            if (existingUser) return false;

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password,
                Role = (Role)registerDto.Role, // Enum dönüşümü
                DepartmentId = registerDto.DepartmentId,
                ManagerId = registerDto.ManagerId,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(CancellationToken.None);
            return true;
        }
    }
}