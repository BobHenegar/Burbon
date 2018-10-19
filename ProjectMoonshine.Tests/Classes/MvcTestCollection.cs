using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProjectMoonshine.Tests.Classes
{
    [CollectionDefinition("ProjectMoonshineTestCollection")]
    public class CollectionClass : ICollectionFixture<BaseTestFixture>
    { }
}
