using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chen0040.ExpertSystem
{
    public class Clause
    {
        protected string m_variable;
        protected string m_value;
        public string m_condition { get; set; } = "=";

        public Clause(string variable, string value)
        {
            m_variable = variable;
            m_value = value;
        }

        public Clause(string variable, string condition, string value)
        {
            m_variable = variable;
            m_value = value;
            m_condition = condition;
        }

        public string getVariable()
        {
            return m_variable;
        }

        public string getValue()
        {
            return m_value;
        }

        public IntersectionType matchClause(Clause rhs)
        {
            if (m_variable!=rhs.getVariable())
            {
                return IntersectionType.UNKNOWN;
            }

            return intersect(rhs);
        }

        protected virtual IntersectionType intersect(Clause rhs)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return m_variable + " " + m_condition + " " + m_value;
        }
    }
}
