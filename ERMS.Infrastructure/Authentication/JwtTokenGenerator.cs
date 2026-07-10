using ERMS.Application.Common.Interfaces;
using ERMS.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ERMS.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken()
        {
            throw new NotImplementedException();
        }

        //Token Oluşturma kısmı
        public string GenerateToken(User user)
        {
            var secretKey = _configuration["Jwt:SecretKey"]; // appsettings.json dosyasındaki secret key'i alıyoruz
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)); // Secret key'i byte dizisine çeviriyoruz
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Token imzalama algoritmasını belirliyoruz

            DateTime expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings: ExpiryInMinutes"]));
            var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("firstName", user.FirstName),
        new Claim("lastName", user.LastName)
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryInMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
