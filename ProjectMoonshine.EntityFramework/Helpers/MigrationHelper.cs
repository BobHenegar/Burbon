using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework.Helpers
{
    public static class MigrationHelper
    {
        public static void RunMigrations(this ApplicationDbContext context, List<string> migrationsApplied)
        {
            // Place newest migrations at the bottom!
        }
        public static void SeedData(this ApplicationDbContext context)
        {
        }
    }
}
