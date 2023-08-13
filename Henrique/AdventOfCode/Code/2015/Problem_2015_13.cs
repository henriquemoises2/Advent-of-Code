using AdventOfCode._2015_13;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public class Problem_2015_13 : Problem
    {

        private const string DispositionPattern = @"^(?<person1>[A-Za-z]+) would (?<signal>[A-Za-z]+) (?<value>\d+) happiness units by sitting next to (?<person2>[A-Za-z]+).";
        private int[,] DispositionMatrix = new int[0, 0];

        public Problem_2015_13() : base()
        {
        }

        public override string Solve()
        {
            List<DispositionChange> dispositionList = ParseDispositionList();
            Dictionary<int, string> people = new Dictionary<int, string>();

            string[] uniquePeople = dispositionList.Select(d => d.Person1).Distinct().ToArray();
            for (int i = 0; i < uniquePeople.Count(); i++)
            {
                people.Add(i, uniquePeople[i]);
            }
            BuildDispositionMatrix(dispositionList, people);
            string part1 = SolvePart1(people);

            // Update all necessary structures with myself present
            IncludeMyself(dispositionList, people);
            string part2 = SolvePart2(people);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private void IncludeMyself(List<DispositionChange> dispositionList, Dictionary<int, string> people)
        {
            foreach (var elem in people)
            {
                dispositionList.Add(new DispositionChange("Myself", elem.Value, 1, 0));
                dispositionList.Add(new DispositionChange(elem.Value, "Myself", 1, 0));
            }
            people.Add(people.Keys.Count, "Myself");

            BuildDispositionMatrix(dispositionList, people);
        }

        private string SolvePart1(Dictionary<int, string> people)
        {
            int maxDisposition = ComputeTotalDisposition(people);
            return maxDisposition.ToString();
        }

        private string SolvePart2(Dictionary<int, string> people)
        {
            int maxDisposition = ComputeTotalDisposition(people);
            return maxDisposition.ToString();
        }

        private List<DispositionChange> ParseDispositionList()
        {
            Regex regexDispositionChangeInput = new Regex(DispositionPattern, RegexOptions.Compiled);
            List<DispositionChange> dispositionsList = new List<DispositionChange>();

            foreach (var line in InputLines)
            {
                Match match = regexDispositionChangeInput.Match(line);

                string person1 = match.Groups[1].Value;
                string signalAsString = match.Groups[2].Value;
                string valueAsString = match.Groups[3].Value;
                string person2 = match.Groups[4].Value;

                // Parse signal
                int signal = 0;
                if (signalAsString == "gain")
                {
                    signal = 1;
                }
                else if (signalAsString == "lose")
                {
                    signal = -1;
                }
                else
                {
                    throw new Exception("Invalid line in input.");
                }

                // Parse value
                int value = 0;
                if (!int.TryParse(valueAsString, out value))
                {
                    throw new Exception("Invalid line in input.");
                }

                dispositionsList.Add(new DispositionChange(person1, person2, signal, value));
            }
            return dispositionsList;
        }

        private void BuildDispositionMatrix(List<DispositionChange> dispositionList, Dictionary<int, string> people)
        {
            int size = people.Keys.Count;
            DispositionMatrix = new int[size, size];

            foreach (var disposition in dispositionList)
            {
                DispositionMatrix[people.Single(p => p.Value == disposition.Person1).Key, people.Single(p => p.Value == disposition.Person2).Key] = disposition.Signal * disposition.Value;
            }
        }

        private int ComputeTotalDisposition(Dictionary<int, string> original)
        {
            List<int> results = new List<int>();
            for (int startingPerson = 0; startingPerson < original.Count; startingPerson++)
            {
                Dictionary<int, string> people = new Dictionary<int, string>(original);
                int totalDisposition = 0;

                int currentPersonIndex = startingPerson;
                int maxRoundDisposition = int.MinValue;
                int currentRoundWinner = int.MinValue;
                people.Remove(startingPerson);

                while (people.Count > 0)
                {
                    foreach (var p in people)
                    {
                        int currentRoundDisposition = DispositionMatrix[currentPersonIndex, p.Key] + DispositionMatrix[p.Key, currentPersonIndex];
                        if (currentRoundDisposition > maxRoundDisposition)
                        {
                            currentRoundWinner = p.Key;
                            maxRoundDisposition = currentRoundDisposition;
                        }
                    }
                    totalDisposition += maxRoundDisposition;
                    maxRoundDisposition = int.MinValue;
                    people.Remove(currentRoundWinner);
                    currentPersonIndex = currentRoundWinner;
                    currentRoundWinner = int.MinValue;
                }

                // Add disposition change for seating last person next to the first one
                int lastRoundDisposition = DispositionMatrix[currentPersonIndex, startingPerson] + DispositionMatrix[startingPerson, currentPersonIndex];
                totalDisposition += lastRoundDisposition;

                results.Add(totalDisposition);
            }

            return results.Max();
        }

    }
}
