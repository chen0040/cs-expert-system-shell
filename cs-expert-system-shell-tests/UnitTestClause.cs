using chen0040.ExpertSystem;
using System;
using Xunit;

namespace cs_expert_system_shell_tests
{
    public class UnitTestClause
    {
        [Fact]
        public void TestNewClause()
        {
            Clause c = new Clause("name", "chen0040");
            Assert.Equal(c.Variable, "name");
            Assert.Equal(c.Value, "chen0040");
            Assert.Equal(c.Condition, "=");
        }
    }
}
