# cs-expert-system-shell
C# implementation of an expert system shell, targeting .Net Core 1.1

[![Build Status](https://travis-ci.org/chen0040/cs-expert-system-shell.svg?branch=master)](https://travis-ci.org/chen0040/cs-expert-system-shell)

# Install

Run the following command to install:

Install-Package cs-expert-system-shell

# Usage

The sample code below shows how to create a rule engine and initialize it with a set of rules:

```cs 
using chen0040.ExpertSystem;
public RuleInferenceEngine getInferenceEngine()
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
```

The sample code below shows how to use forward chaining in the rule engine to derive more facts from the known facts using rules:

```cs 
RuleInferenceEngine rie = getInferenceEngine();
rie.AddFact(new IsClause("num_wheels", "4"));
rie.AddFact(new IsClause("motor", "yes"));
rie.AddFact(new IsClause("num_doors", "3"));
rie.AddFact(new IsClause("size", "medium"));

console.WriteLine("before inference");
console.WriteLine("{0}", rie.Facts);
console.WriteLine("");

rie.Infer(); //forward chain

console.WriteLine("after inference");
console.WriteLine("{0}", rie.Facts);
console.WriteLine("");
```

The sample code below shows how to use the backward chaining to reach conclusion for a target variable given a set of known facts:

```cs
RuleInferenceEngine rie = getInferenceEngine();
rie.AddFact(new IsClause("num_wheels", "4"));
rie.AddFact(new IsClause("motor", "yes"));
rie.AddFact(new IsClause("num_doors", "3"));
rie.AddFact(new IsClause("size", "medium"));

console.WriteLine("Infer: vehicle");

List<Clause> unproved_conditions = new List<Clause>();

Clause conclusion = rie.Infer("vehicle", unproved_conditions);

console.WriteLine("Conclusion: " + conclusion);

Assert.Equal(conclusion.Value, "MiniVan");
```

The sample code below shows how to use the rule engine to ask more questions when it fails to reach conclusion for the target variable given a limited set of known facts:

```cs
RuleInferenceEngine rie = getInferenceEngine();

console.WriteLine("Infer with All Facts Cleared:");
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
		console.WriteLine("ask: " + c + "?");
		unproved_conditions.Clear();
		console.WriteLine("What is " + c.Variable + "?");
		String value = Console.ReadLine();
		rie.AddFact(new IsClause(c.Variable, value));
	}
}

console.WriteLine("Conclusion: " + conclusion);
console.WriteLine("Memory: ");
console.WriteLine("{0}", rie.Facts);
```
