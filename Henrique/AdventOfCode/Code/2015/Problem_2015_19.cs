﻿using AdventOfCode._2015_19;
using AdventOfCode.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2015_19 : Problem
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
            List<Transformation> moleculeTransformations = [];
            string initialMedicineMolecule;

            Regex pattern = TransformationRegex();
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

            return string.Format(SolutionFormat, part1, part2);

        }

        private static string SolvePart1(string initialMedicineMolecule, List<Transformation> moleculeTransformations)
        {
            HashSet<string> distinctGeneratedMedicineMolecules = ExecuteGenerationStep(initialMedicineMolecule, moleculeTransformations);
            return distinctGeneratedMedicineMolecules.Count.ToString();
        }

        private static string SolvePart2(string wantedMedicineMolecule, List<Transformation> moleculeTransformations)
        {
            HashSet<string> generatedMedicineMolecules = [];
            HashSet<string> uniqueMedicineMolecules = [];
            generatedMedicineMolecules.Add(wantedMedicineMolecule);
            int stepNumber = 1;

            while (stepNumber < MaxNumberSteps)
            {
                HashSet<string> newDistinctGeneratedMedicineMolecules = [];
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

        private static HashSet<string> ExecuteGenerationStep(string initialMedicineMolecule, List<Transformation> moleculeTransformations)
        {
            List<Molecule> initialMedicineMolecules = [.. ExtractMoleculesFromString(initialMedicineMolecule)];
            HashSet<string> distinctGeneratedMedicineMolecules = [];

            // Filter transformations, i.e. eliminate transformations whose Initial component does not exist in the initial molecule
            moleculeTransformations = [.. moleculeTransformations.Where(trans => initialMedicineMolecules.Select(molecule => molecule.Value).Contains(trans.InitialMolecule))];

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

        private static HashSet<string> ExecuteReverseGenerationStep(string molecule, List<Transformation> moleculeTransformations)
        {
            List<Molecule> initialMedicineMolecules = [.. ExtractMoleculesFromString(molecule)];
            HashSet<string> distinctGeneratedMedicineMolecules = [];

            // Filter transformations, i.e. eliminate transformations whose final component does not exist in the initial molecule
            moleculeTransformations = [.. moleculeTransformations.Where(trans => molecule.Contains(trans.FinalMolecule))];

            foreach (Transformation transformation in moleculeTransformations)
            {
                Regex regex = new(transformation.FinalMolecule);
                MatchCollection matches = regex.Matches(molecule);
                foreach (Match match in matches.Cast<Match>())
                {
                    string newMolecule = MoleculeReplace(molecule, match.Groups[0].Index, transformation.FinalMolecule, transformation.InitialMolecule);
                    distinctGeneratedMedicineMolecules.Add(newMolecule);
                }
            }

            return distinctGeneratedMedicineMolecules;
        }

        private static HashSet<string> SelectBestCandidates(HashSet<string> generatedMolecules, int topElements)
        {
            var sortedCandidates = generatedMolecules.OrderBy(molecule => molecule.Length).Take(topElements);
            return [.. sortedCandidates];
        }

        private static List<Molecule> ExtractMoleculesFromString(string medicineMolecule)
        {
            Regex pattern = MoleculeRegex();
            List<string> capturedMolecules = [.. pattern.Match(medicineMolecule).Groups[1].Captures.Select(capt => capt.Value)];

            List<Molecule> extractedMolecules = [];
            for (int i = 0; i < capturedMolecules.Count; i++)
            {
                extractedMolecules.Add(new Molecule(i, capturedMolecules.ElementAt(i)));
            }

            return extractedMolecules;
        }

        private static string ExtractStringFromMolecules(IEnumerable<Molecule> molecules)
        {
            return string.Join("", molecules.Select(molecule => molecule.Value));
        }

        private static string MoleculeReplace(string molecule, int index, string replacementInitial, string replacementFinal)
        {
            return StringOperations.ReplaceAtIndex(molecule, index, replacementInitial, replacementFinal);
        }

        [GeneratedRegex(TransformationPattern, RegexOptions.Compiled)]
        private static partial Regex TransformationRegex();
        [GeneratedRegex(MoleculePattern, RegexOptions.Compiled)]
        private static partial Regex MoleculeRegex();
    }
}