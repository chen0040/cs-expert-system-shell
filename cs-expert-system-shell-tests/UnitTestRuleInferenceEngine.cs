using chen0040.ExpertSystem;
using System;
using System.Collections.Generic;
using Xunit;

namespace cs_expert_system_shell_tests
{
    public class UnitTestRuleInferenceEngine
    {
        [Fact]
        public void TestBackwardChain()
        {
            RuleInferenceEngine rie = getInferenceEngine();
            rie.AddFact(new IsClause("num_wheels", "4"));
            rie.AddFact(new IsClause("motor", "yes"));
            rie.AddFact(new IsClause("num_doors", "3"));
            rie.AddFact(new IsClause("size", "medium"));

            Console.WriteLine("Infer: vehicle");

            List<Clause> unproved_conditions = new List<Clause>();

            Clause conclusion = rie.Infer("vehicle", unproved_conditions);

            Console.WriteLine("Conclusion: " + conclusion);

            Assert.Equal(conclusion.getValue(), "MiniVan");
        }

        public void demoBackwardChainWithNullMemory()
        {
            RuleInferenceEngine rie = getInferenceEngine();

            Console.WriteLine("Infer with All Facts Cleared:");
            rie.ClearFacts();

            List<Clause> unproved_conditions = new List<Clause>();

            Clause conclusion = null;
            while (conclusion == null)
            {
                conclusion = rie.Infer("vehicle", unproved_conditions);
                if (conclusion == null)
                {
                    if (unproved_conditions.Count == 0)
                    {
                        break;
                    }
                    Clause c = unproved_conditions[0];
                    Console.WriteLine("ask: " + c + "?");
                    unproved_conditions.Clear();
                    Console.WriteLine("What is " + c.getVariable() + "?");
                    String value = Console.ReadLine();
                    rie.AddFact(new IsClause(c.getVariable(), value));
                }
            }

            Console.WriteLine("Conclusion: " + conclusion);
            Console.WriteLine("Memory: ");
            Console.WriteLine(rie.Facts);
        }

        [Fact]
        public void demoForwardChain()
        {
            RuleInferenceEngine rie = getInferenceEngine();
            rie.AddFact(new IsClause("num_wheels", "4"));
            rie.AddFact(new IsClause("motor", "yes"));
            rie.AddFact(new IsClause("num_doors", "3"));
            rie.AddFact(new IsClause("size", "medium"));

            Console.WriteLine("before inference");
            Console.WriteLine(rie.Facts);
            Console.WriteLine();

            rie.Infer(); //forward chain

            Console.WriteLine("after inference");
            Console.WriteLine(rie.Facts);
            Console.WriteLine();
        }

        private RuleInferenceEngine getInferenceEngine()
        {
            RuleInferenceEngine rie = new RuleInferenceEngine();

            Rule rule = new Rule("Bicycle");
            rule.AddAntecedent(new IsClause("vehicleType", "cycle"));
            rule.AddAntecedent(new IsClause("num_wheels", "2"));
            rule.AddAntecedent(new IsClause("motor", "no"));
            rule.setConsequent(new IsClause("vehicle", "Bicycle"));
            rie.AddRule(rule);

            rule = new Rule("Tricycle");
            rule.AddAntecedent(new IsClause("vehicleType", "cycle"));
            rule.AddAntecedent(new IsClause("num_wheels", "3"));
            rule.AddAntecedent(new IsClause("motor", "no"));
            rule.setConsequent(new IsClause("vehicle", "Tricycle"));
            rie.AddRule(rule);

            rule = new Rule("Motorcycle");
            rule.AddAntecedent(new IsClause("vehicleType", "cycle"));
            rule.AddAntecedent(new IsClause("num_wheels", "2"));
            rule.AddAntecedent(new IsClause("motor", "yes"));
            rule.setConsequent(new IsClause("vehicle", "Motorcycle"));
            rie.AddRule(rule);

            rule = new Rule("SportsCar");
            rule.AddAntecedent(new IsClause("vehicleType", "automobile"));
            rule.AddAntecedent(new IsClause("size", "medium"));
            rule.AddAntecedent(new IsClause("num_doors", "2"));
            rule.setConsequent(new IsClause("vehicle", "Sports_Car"));
            rie.AddRule(rule);

            rule = new Rule("Sedan");
            rule.AddAntecedent(new IsClause("vehicleType", "automobile"));
            rule.AddAntecedent(new IsClause("size", "medium"));
            rule.AddAntecedent(new IsClause("num_doors", "4"));
            rule.setConsequent(new IsClause("vehicle", "Sedan"));
            rie.AddRule(rule);

            rule = new Rule("MiniVan");
            rule.AddAntecedent(new IsClause("vehicleType", "automobile"));
            rule.AddAntecedent(new IsClause("size", "medium"));
            rule.AddAntecedent(new IsClause("num_doors", "3"));
            rule.setConsequent(new IsClause("vehicle", "MiniVan"));
            rie.AddRule(rule);

            rule = new Rule("SUV");
            rule.AddAntecedent(new IsClause("vehicleType", "automobile"));
            rule.AddAntecedent(new IsClause("size", "large"));
            rule.AddAntecedent(new IsClause("num_doors", "4"));
            rule.setConsequent(new IsClause("vehicle", "SUV"));
            rie.AddRule(rule);

            rule = new Rule("Cycle");
            rule.AddAntecedent(new LessClause("num_wheels", "4"));
            rule.setConsequent(new IsClause("vehicleType", "cycle"));
            rie.AddRule(rule);

            rule = new Rule("Automobile");
            rule.AddAntecedent(new IsClause("num_wheels", "4"));
            rule.AddAntecedent(new IsClause("motor", "yes"));
            rule.setConsequent(new IsClause("vehicleType", "automobile"));
            rie.AddRule(rule);

            return rie;
        }
    }
}
