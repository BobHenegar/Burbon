using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Helpers
{
    public static class MigrationHelper
    {
        public static void RunMigrations(this ApplicationDbContext context, List<string> migrationsApplied)
        {
            // Place newest migrations at the bottom!
            if (migrationsApplied.Contains("20181009161808_CreateCustomerFirst"))
            { }
        }
    }
}
