using AdventOfCode._2015_16;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public class Problem_2015_16 : Problem
    {

        private const string AuntSuePattern = @"^Sue (?<auntSueNumber>\d+): ((?<compoundName>\w+): (?<compoundQuantity>\d+)(, )*)+";
        private List<AuntSue> auntsSue = new List<AuntSue>();

        public Problem_2015_16() : base()
        { }

        public override string Solve()
        {
            Regex pattern = new Regex(AuntSuePattern, RegexOptions.Compiled);
            foreach (string line in InputLines)
            {
                Match match = pattern.Match(line);
                if (!match.Success)
                {
                    throw new Exception("Invalid line in input.");
                }
                else
                {

                    try
                    {
                        List<Compound> compounds = new List<Compound>();
                        int auntNumber = int.Parse(match.Groups["auntSueNumber"].Value);
                        for (int i = 0; i < match.Groups["compoundName"].Captures.Count; i++)
                        {
                            string compoundName = match.Groups["compoundName"].Captures[i].Value;
                            int compoundQuantity = int.Parse(match.Groups["compoundQuantity"].Captures[i].Value);

                            compounds.Add(new Compound(compoundName, compoundQuantity));
                        }
                        auntsSue.Add(new AuntSue(auntNumber, compounds));
                    }
                    catch
                    {
                        throw new Exception("Invalid line in input.");
                    }
                }
            }

            MFCSAM mfcsam = new MFCSAM();
            string part1 = SolvePart1(mfcsam);
            string part2 = SolvePart2(mfcsam);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private string SolvePart1(MFCSAM mfcsam)
        {
            AuntSue? validSamples = auntsSue.SingleOrDefault(aunt => mfcsam.ValidateSample(aunt));
            if (validSamples == null)
            {
                throw new Exception("Inconclusive MFCSAM result!");
            }
            return validSamples.Number.ToString();
        }

        private string SolvePart2(MFCSAM mfcsam)
        {
            AuntSue? validSamples = auntsSue.SingleOrDefault(aunt => mfcsam.ValidateSampleWithOutdatedRetroencabulator(aunt));
            if (validSamples == null)
            {
                throw new Exception("Inconclusive MFCSAM result!");
            }
            return validSamples.Number.ToString();
        }
    }
}