using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chen0040.ExpertSystem
{
    public class WorkingMemory
    {
        protected List<Clause> m_facts = new List<Clause>();

        public WorkingMemory()
        {

        }

        public void addFact(Clause fact)
        {
            m_facts.Add(fact);
        }

        public bool isNotFact(Clause c)
        {
            foreach(Clause fact in m_facts)
            {
                if (fact.matchClause(c) == IntersectionType.MUTUALLY_EXCLUDE)
                {
                    return true;
                }
            }

            return false;
        }

        public void clearFacts()
        {
            m_facts.Clear();
        }

        public bool isFact(Clause c)
        {
            foreach(Clause fact in m_facts)
            {
                if (fact.matchClause(c) == IntersectionType.INCLUDE)
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder message = new StringBuilder();

            bool first_clause = true;
            foreach(Clause cc in m_facts)
            {
                if (first_clause)
                {
                    message.Append(cc.ToString());
                    first_clause = false;
                }
                else
                {
                    message.Append("\n"+cc.ToString());
                }
            }

            return message.ToString();
        }

        public int Count
        {
            get { return m_facts.Count;  }
        }
    }
}
