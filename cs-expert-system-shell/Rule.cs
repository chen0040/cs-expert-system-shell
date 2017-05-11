using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chen0040.ExpertSystem
{
    public class Rule
    {
        protected Clause m_consequent = null;
        protected List<Clause> m_antecedents = new List<Clause>();
        protected bool m_fired = false;
        protected string m_name;
        protected int m_index = 0;

        public Rule(string name)
        {
            m_name = name;
        }

        public void FirstAntecedent()
        {
            m_index = 0;
        }

        public bool HasNextAntecedents()
        {
            return m_index < m_antecedents.Count;
        }

        public Clause NextAntecedent()
        {
            Clause c = m_antecedents[m_index];
            m_index++;
            return c;
        }

        public string getName()
        {
            return m_name;
        }

        public void setConsequent(Clause consequent)
        {
            m_consequent = consequent;
        }

        public void AddAntecedent(Clause antecedent)
        {
            m_antecedents.Add(antecedent);
        }

        public Clause getConsequent()
        {
            return m_consequent;
        }

        public bool isFired()
        {
            return m_fired;
        }

        public void fire(WorkingMemory wm)
        {
            if (!wm.IsFact(m_consequent))
            {
                wm.AddFact(m_consequent);
            }

            m_fired = true;
        }

        public bool isTriggered(WorkingMemory wm)
        {
            foreach (Clause antecedent in m_antecedents) 
            {
                if (!wm.IsFact(antecedent))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
