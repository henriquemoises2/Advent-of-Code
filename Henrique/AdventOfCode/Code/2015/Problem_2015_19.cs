using AdventOfCode._2015_19;
using AdventOfCode.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public class Problem_2015_19 : Problem
    {
        private const string TransformationPattern = @"^(?<initialMolecule>\w+) => (?<finalMolecule>\w+)";
        private const string SingleElectron = "e";
        private const string MoleculePattern = @"([A-Z][a-z]*|e)+";
        private const int MaxNumberSteps = 1000;

        public Problem_2015_19() : base()
        {
        }

        public override string Solve()
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

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private string SolvePart1(string initialMedicineMolecule, List<Transformation> moleculeTransformations)
        {
            HashSet<string> distinctGeneratedMedicineMolecules = ExecuteGenerationStep(initialMedicineMolecule, moleculeTransformations);
            return distinctGeneratedMedicineMolecules.Count.ToString();
        }

        private string SolvePart2(string wantedMedicineMolecule, List<Transformation> moleculeTransformations)
        {
            HashSet<string> generatedMedicineMolecules = new HashSet<string>();
            HashSet<string> uniqueMedicineMolecules = new HashSet<string>();
            generatedMedicineMolecules.Add(wantedMedicineMolecule);
            int stepNumber = 1;

            while (stepNumber < MaxNumberSteps)
            {
                HashSet<string> newDistinctGeneratedMedicineMolecules = new HashSet<string>();
                foreach (string generatedMolecule in generatedMedicineMolecules)
                {
                    newDistinctGeneratedMedicineMolecules = ExecuteReverseGenerationStep(generatedMolecule, moleculeTransformations);
                }
                newDistinctGeneratedMedicineMolecules = SelectBestCandidates(newDistinctGeneratedMedicineMolecules, 1);
                if (newDistinctGeneratedMedicineMolecules.Any(molecule => molecule == SingleElectron))
                {
                    return stepNumber.ToString();
                }
                generatedMedicineMolecules = newDistinctGeneratedMedicineMolecules;

                stepNumber++;
            }
            throw new Exception("No solution found.");
        }

        private HashSet<string> ExecuteGenerationStep(string initialMedicineMolecule, List<Transformation> moleculeTransformations)
        {
            List<Molecule> initialMedicineMolecules = ExtractMoleculesFromString(initialMedicineMolecule).ToList();
            HashSet<string> distinctGeneratedMedicineMolecules = new HashSet<string>();

            // Filter transformations, i.e. eliminate transformations whose Initial component does not exist in the initial molecule
            moleculeTransformations = moleculeTransformations.Where(trans => initialMedicineMolecules.Select(molecule => molecule.Value).Contains(trans.InitialMolecule)).ToList();

            foreach (Transformation transformation in moleculeTransformations)
            {
                foreach (Molecule molecule in initialMedicineMolecules.Where(molecule => molecule.Value == transformation.InitialMolecule))
                {
                    initialMedicineMolecules.ElementAt(molecule.Position).Value = transformation.FinalMolecule;
                    distinctGeneratedMedicineMolecules.Add(ExtractStringFromMolecules(initialMedicineMolecules));
                    initialMedicineMolecules.ElementAt(molecule.Position).Value = transformation.InitialMolecule;
                }
            }

            return distinctGeneratedMedicineMolecules;
        }

        private HashSet<string> ExecuteReverseGenerationStep(string molecule, List<Transformation> moleculeTransformations)
        {
            List<Molecule> initialMedicineMolecules = ExtractMoleculesFromString(molecule).ToList();
            HashSet<string> distinctGeneratedMedicineMolecules = new HashSet<string>();

            // Filter transformations, i.e. eliminate transformations whose final component does not exist in the initial molecule
            moleculeTransformations = moleculeTransformations.Where(trans => molecule.Contains(trans.FinalMolecule)).ToList();

            foreach (Transformation transformation in moleculeTransformations)
            {
                Regex regex = new Regex(transformation.FinalMolecule);
                MatchCollection matches = regex.Matches(molecule);
                foreach (Match match in matches)
                {
                    string newMolecule = MoleculeReplace(molecule, match.Groups[0].Index, transformation.FinalMolecule, transformation.InitialMolecule);
                    distinctGeneratedMedicineMolecules.Add(newMolecule);
                }
            }

            return distinctGeneratedMedicineMolecules;
        }

        private HashSet<string> SelectBestCandidates(HashSet<string> generatedMolecules, int topElements)
        {
            var sortedCandidates = generatedMolecules.OrderBy(molecule => molecule.Length).Take(topElements);
            return sortedCandidates.ToHashSet();
        }

        private IEnumerable<Molecule> ExtractMoleculesFromString(string medicineMolecule)
        {
            Regex pattern = new Regex(MoleculePattern, RegexOptions.Compiled);
            List<string> capturedMolecules = pattern.Match(medicineMolecule).Groups[1].Captures.Select(capt => capt.Value).ToList();

            List<Molecule> extractedMolecules = new List<Molecule>();
            for (int i = 0; i < capturedMolecules.Count(); i++)
            {
                extractedMolecules.Add(new Molecule(i, capturedMolecules.ElementAt(i)));
            }

            return extractedMolecules;
        }

        private string ExtractStringFromMolecules(IEnumerable<Molecule> molecules)
        {
            return string.Join("", molecules.Select(molecule => molecule.Value));
        }

        private string MoleculeReplace(string molecule, int index, string replacementInitial, string replacementFinal)
        {
            return StringOperations.ReplaceAtIndex(molecule, index, replacementInitial, replacementFinal);
        }

    }
}