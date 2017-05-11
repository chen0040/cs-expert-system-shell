using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chen0040.ExpertSystem
{
    public class RuleInferenceEngine
    {
        protected List<Rule> m_rules = new List<Rule>();
        protected WorkingMemory m_wm = new WorkingMemory();

        public RuleInferenceEngine()
        {

        }

        public void AddRule(Rule rule)
        {
            m_rules.Add(rule);
        }

        public void ClearRules()
        {
            m_rules.Clear();
        }

        //forward chain
        public void Infer()
        {
            List<Rule> cs = null;
            do
            {
                cs = Match();
                if (cs.Count > 0)
                {
                    if (!FireRule(cs))
                    {
                        break;
                    }
                }
            } while (cs.Count > 0);
        }

        //backward chain
        public Clause Infer(string goal_variable, List<Clause> unproved_conditions)
        {
            Clause conclusion = null;
            List<Rule> goal_stack = new List<Rule>();

            foreach(Rule rule in m_rules)
            {
                Clause consequent = rule.getConsequent();
                if (consequent.Variable==goal_variable)
                {
                    goal_stack.Add(rule);
                }
            }

            foreach(Rule rule in m_rules)
            {
                rule.FirstAntecedent();
                bool goal_reached = true;
                while (rule.HasNextAntecedents())
                {
                    Clause antecedent = rule.NextAntecedent();
                    if (!m_wm.IsFact(antecedent))
                    {
                        if (m_wm.IsNotFact(antecedent)) //conflict with memory
                        {
                            goal_reached = false;
                            break;
                        }
                        else if (IsFact(antecedent, unproved_conditions)) //deduce to be a fact
                        {
                            m_wm.AddFact(antecedent);
                        }
                        else //deduce to not be a fact
                        {
                            goal_reached = false;
                            break;
                        }
                    }
                }

                if (goal_reached)
                {
                    conclusion = rule.getConsequent();
                    break;
                }
            }

            return conclusion;
        }

        public void ClearFacts()
        {
            m_wm.ClearFacts();
        }

        protected bool IsFact(Clause goal, List<Clause> unproved_conditions)
        {
            List<Rule> goal_stack = new List<Rule>();

            foreach(Rule rule in m_rules)
            {
                Clause consequent = rule.getConsequent();
                IntersectionType it = consequent.MatchClause(goal);
                if (it == IntersectionType.INCLUDE)
                {
                    goal_stack.Add(rule);
                }
            }

            if (goal_stack.Count == 0)
            {
                unproved_conditions.Add(goal);
            }
            else
            {
                foreach(Rule rule in goal_stack)
                {
                    rule.FirstAntecedent();
                    bool goal_reached = true;
                    while (rule.HasNextAntecedents())
                    {
                        Clause antecedent = rule.NextAntecedent();
                        if (!m_wm.IsFact(antecedent))
                        {
                            if (m_wm.IsNotFact(antecedent))
                            {
                                goal_reached = false;
                                break;
                            }
                            else if (IsFact(antecedent, unproved_conditions))
                            {
                                m_wm.AddFact(antecedent);
                            }
                            else
                            {
                                goal_reached = false;
                                break;
                            }
                        }
                    }

                    if (goal_reached)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected bool FireRule(List<Rule> conflictingRules)
        {
            bool hasRule2Fire = false;
            foreach(Rule rule in conflictingRules)
            {
                if (!rule.isFired())
                {
                    hasRule2Fire = true;
                    rule.fire(m_wm);
                }
            }

            return hasRule2Fire;

        }

        /// <summary>
        /// property indicating the known facts in the current working memory
        /// </summary>
        public WorkingMemory Facts
        {
            get
            {
                return m_wm;
            }
        }

        /// <summary>
        /// Add another know fact into the working memory
        /// </summary>
        /// <param name="c"></param>
        public void AddFact(Clause c)
        {
            m_wm.AddFact(c);
        }

        /// <summary>
        /// Method that return the set of rules whose antecedents match with the working memory
        /// </summary>
        /// <returns></returns>
        protected List<Rule> Match()
        {
            List<Rule> cs = new List<Rule>();
            foreach(Rule rule in m_rules)
            {
                if (rule.isTriggered(m_wm))
                {
                    cs.Add(rule);
                }
            }
            return cs;
        }
    }
}
