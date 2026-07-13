using ERMS.Application.Common.Interfaces;
using ERMS.Application.DTOs;
using Microsoft.EntityFrameworkCore;

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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if(user == null || user.PasswordHash != loginDto.Password)
            {
                return null;
            }

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
    }
}
