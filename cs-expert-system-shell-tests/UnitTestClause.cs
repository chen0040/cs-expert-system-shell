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
            Assert.Equal(c.getVariable(), "name");
            Assert.Equal(c.getValue(), "chen0040");
            Assert.Equal(c.m_condition, "=");
        }
    }
}
