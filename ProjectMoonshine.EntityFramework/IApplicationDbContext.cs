using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework
{
    public interface IApplicationDbContext
    {
        DbSet<ErrorLog> ErrorLogs { get; set; }
    }
}
