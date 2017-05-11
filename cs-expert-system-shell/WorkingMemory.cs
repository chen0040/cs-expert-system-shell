using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chen0040.ExpertSystem
{
    public class WorkingMemory
    {
        protected List<Clause> _facts = new List<Clause>();

        public WorkingMemory()
        {

        }

        public void AddFact(Clause fact)
        {
            _facts.Add(fact);
        }

        public bool IsNotFact(Clause c)
        {
            foreach(Clause fact in _facts)
            {
                if (fact.MatchClause(c) == IntersectionType.MUTUALLY_EXCLUDE)
                {
                    return true;
                }
            }

            return false;
        }

        public void ClearFacts()
        {
            _facts.Clear();
        }

        public bool IsFact(Clause c)
        {
            foreach(Clause fact in _facts)
            {
                if (fact.MatchClause(c) == IntersectionType.INCLUDE)
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
            foreach(Clause cc in _facts)
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
            get { return _facts.Count;  }
        }
    }
}
