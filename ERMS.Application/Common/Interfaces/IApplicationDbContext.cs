using ERMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        

        DbSet<Department> Departments { get; set; }

        DbSet<Request> Requests { get; set; }

        DbSet<Approval> Approvals { get; set; }



        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
