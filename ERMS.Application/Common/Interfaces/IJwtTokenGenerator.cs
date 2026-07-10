using ERMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace ERMS.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken();
        string GenerateToken(User user);
        string GenerateToken(User user);
    }
}
