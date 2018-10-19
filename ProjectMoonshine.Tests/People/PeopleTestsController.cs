using System;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.Tests.Classes;
using Xunit;

namespace ProjectMoonshine.Tests
{
    [Collection("ProjectMoonshineTestCollection")]
    public class PeopleTestsController
    {
        private readonly BaseTestFixture _fixture;
        private readonly ApplicationDbContext _context;
        public PeopleTestsController(ApplicationDbContext context,
            BaseTestFixture fixture)
        {
            _context = context;
            _fixture = fixture;
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
