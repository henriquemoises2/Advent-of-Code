using AdventOfCode._2015_15;
using AdventOfCode.Code._2015.Entities._2015_19;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_19 : Problem
    {
        private const string TransformationPattern = @"^(?<initialMolecule>\w+) => (?<finalMolecule>\w+)";
        private const string SingleElectron = "e";
        private const string MoleculePattern = @"([A-Z][a-z]*|e)+";
        private const int MaxNumberSteps = 1000;

        internal Problem_2015_19() : base()
        {
        }

        internal override string Solve()
        {
            List<Transformation> moleculeTransformations = new List<Transformation>();
            string initialMedicineMolecule;

            Regex pattern = new Regex(TransformationPattern, RegexOptions.Compiled);
            try
            {
                foreach (string line in InputLines)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    Match match = pattern.Match(line);
                    moleculeTransformations.Add(new Transformation(match.Groups[1].Value, match.Groups[2].Value));
                }
                initialMedicineMolecule = InputLastLine;
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1(initialMedicineMolecule, moleculeTransformations);
            string part2 = SolvePart2(initialMedicineMolecule, moleculeTransformations);

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1(string initialMedicineMolecule, List<Transformation> moleculeTransformations)
        {
            HashSet<string> distinctGeneratedMedicineMolecules = new HashSet<string>();
            distinctGeneratedMedicineMolecules = ExecuteGenerationStep(initialMedicineMolecule, moleculeTransformations);
            return distinctGeneratedMedicineMolecules.Count.ToString();
        }



        private string SolvePart2(string wantedMedicineMolecule, List<Transformation> moleculeTransformations)
        {
            HashSet<string> allKnownMedicineMolecules = new HashSet<string>();
            HashSet<string> distinctGeneratedMedicineMolecules = new HashSet<string>();
            distinctGeneratedMedicineMolecules.Add(SingleElectron);
            int stepNumber = 1;

            while (stepNumber < MaxNumberSteps)
            {
                HashSet<string> newDistinctGeneratedMedicineMolecules = new HashSet<string>();
                foreach (string generatedMolecule in distinctGeneratedMedicineMolecules)
                {
                    var generatedMolecules = ExecuteGenerationStep(generatedMolecule, moleculeTransformations);
                    generatedMolecules.ExceptWith(allKnownMedicineMolecules);

                    newDistinctGeneratedMedicineMolecules.UnionWith(generatedMolecules);
                    allKnownMedicineMolecules.UnionWith(generatedMolecules);

                    if (newDistinctGeneratedMedicineMolecules.Contains(wantedMedicineMolecule))
                    {
                        return stepNumber.ToString();
                    }
                }
                distinctGeneratedMedicineMolecules = newDistinctGeneratedMedicineMolecules;
                stepNumber++;
            }
            throw new Exception("No solution found.");
        }

        private HashSet<string> ExecuteGenerationStep(string initialMedicineMolecule, List<Transformation> moleculeTransformations)
        {
            HashSet<string> distinctGeneratedMedicineMolecules = new HashSet<string>();
            foreach (Transformation transform in moleculeTransformations)
            {
                IEnumerable<string> newMedicineMolecules = GenerateMedicineMolecules(initialMedicineMolecule, transform);
                // Guarantees that no repeated values are added
                distinctGeneratedMedicineMolecules.UnionWith(newMedicineMolecules);
            }
            return distinctGeneratedMedicineMolecules;
        }


        private IEnumerable<string> GenerateMedicineMolecules(string initialMedicineMolecule, Transformation transformation)
        {
            List<string> medicineMolecules = ExtractMoleculesFromString(initialMedicineMolecule).ToList();
            List<string> generatedMedicineMolecules = new List<string>();

            // If molecule does not exist on medicineMolecules, we skip the search
            if (!medicineMolecules.Contains(transformation.InitialMolecule))
            {
                return generatedMedicineMolecules;
            }

            for (int i = 0; i < medicineMolecules.Count(); i++)
            {
                if (medicineMolecules.ElementAt(i) == transformation.InitialMolecule)
                {
                    string initialMolecule = medicineMolecules.ElementAt(i);
                    medicineMolecules[i] = transformation.FinalMolecule;
                    generatedMedicineMolecules.Add(string.Join("", medicineMolecules));
                    medicineMolecules[i] = initialMolecule;
                }
            }
            return generatedMedicineMolecules;
        }

        private IEnumerable<string> ExtractMoleculesFromString(string medicineMolecule)
        {
            Regex pattern = new Regex(MoleculePattern, RegexOptions.Compiled);
            return pattern.Match(medicineMolecule).Groups[1].Captures.Select(capt => capt.Value);
        }
    }
}