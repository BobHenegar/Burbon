using System;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using Xunit;

namespace ProjectMoonshine.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData()]
        public void Test1()
        {
            try
            {
                var context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
                context.TestOnModelCreating();
            }
            catch (Exception ex)
            {
                Assert.True(ex.Message == null, ex.Message);
            }
        }
    }
}
