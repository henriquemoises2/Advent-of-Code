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
            distinctGeneratedMedicineMolecules = ExecuteGenerationStep(initialMedicineMolecule, null, moleculeTransformations);
            return distinctGeneratedMedicineMolecules.Count.ToString();
        }

        private string SolvePart2(string wantedMedicineMolecule, List<Transformation> moleculeTransformations)
        {
            HashSet<string> distinctGeneratedMedicineMolecules = new HashSet<string>();
            List<string> wantedMedicineMolecules = ExtractMoleculesFromString(wantedMedicineMolecule).ToList();
            distinctGeneratedMedicineMolecules.Add(SingleElectron);
            int stepNumber = 1;

            while (stepNumber < MaxNumberSteps)
            {
                HashSet<string> newDistinctGeneratedMedicineMolecules = new HashSet<string>();
                foreach (string generatedMolecule in distinctGeneratedMedicineMolecules)
                {
                    var generatedMolecules = ExecuteGenerationStep(generatedMolecule, wantedMedicineMolecules, moleculeTransformations, true);
                    newDistinctGeneratedMedicineMolecules.UnionWith(generatedMolecules);

                    if (generatedMolecules.Any(molecule => molecule == wantedMedicineMolecule))
                    {
                        return stepNumber.ToString();
                    }
                }

                var bestCandidates = SelectBestCandidates(wantedMedicineMolecules, newDistinctGeneratedMedicineMolecules);
                distinctGeneratedMedicineMolecules = bestCandidates;

                stepNumber++;
            }
            throw new Exception("No solution found.");
        }

        private HashSet<string> ExecuteGenerationStep(string initialMedicineMolecule, List<string>? wantedMedicineMolecules, List<Transformation> moleculeTransformations, bool justNextMolecule = false)
        {
            List<string> initialMedicineMolecules = ExtractMoleculesFromString(initialMedicineMolecule).ToList();
            HashSet<string> distinctGeneratedMedicineMolecules = new HashSet<string>();

            for (int i = 0, iterations = 0; i < initialMedicineMolecules.Count; i++, iterations++)
            {
                string molecule = initialMedicineMolecules[i];

                if (wantedMedicineMolecules != null)
                {
                    if (initialMedicineMolecules[i] == wantedMedicineMolecules[i])
                    {
                        continue;
                    }

                    i = Math.Max(0, i - 1);
                }

                foreach (Transformation transformation in moleculeTransformations.Where(trans => trans.InitialMolecule == molecule))
                {
                    initialMedicineMolecules[i] = transformation.FinalMolecule;

                    string newMoleculeAsString = string.Join("", initialMedicineMolecules);
                    // Guarantees that no repeated values are added
                    distinctGeneratedMedicineMolecules.Add(newMoleculeAsString);

                    initialMedicineMolecules[i] = molecule;
                }
                if(iterations == 2)
                {
                    break;
                }
            }
            return distinctGeneratedMedicineMolecules;
        }


        private HashSet<string> SelectBestCandidates(IEnumerable<string> wantedMedicineMolecule, HashSet<string> medicineMolecules)
        {
            List<Tuple<int, string>> candidates = new List<Tuple<int, string>>();
            foreach (string medicineMolecule in medicineMolecules)
            {
                List<string> initialMedicineMolecules = ExtractMoleculesFromString(medicineMolecule).ToList();
                int numberSimilarMolecules = 0;
                for (int i = 0; i < initialMedicineMolecules.Count; i++)
                {
                    if (wantedMedicineMolecule.ElementAt(numberSimilarMolecules) == initialMedicineMolecules[numberSimilarMolecules])
                    {
                        numberSimilarMolecules++;
                    }
                }
                candidates.Add(new Tuple<int, string>(numberSimilarMolecules, medicineMolecule));
            }
            return candidates.Where(cand => cand.Item1 == candidates.Max(cand => cand.Item1)).Select(cand => cand.Item2).ToHashSet();
        }

        private IEnumerable<string> ExtractMoleculesFromString(string medicineMolecule)
        {
            Regex pattern = new Regex(MoleculePattern, RegexOptions.Compiled);
            return pattern.Match(medicineMolecule).Groups[1].Captures.Select(capt => capt.Value);
        }

    }
}