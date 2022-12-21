using AdventOfCode.Code._2015.Entities._2015_19;
using AdventOfCode.Code._2015.Entities._2015_21;
using AdventOfCode.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_21 : Problem
    {
        private const string BossAttributesPattern = @"^Hit Points: (?<hitPoints>\d+)\nDamage: (?<damage>\d+)\nArmor: (?<armor>\d+)";

        internal Problem_2015_21() : base()
        {
        }

        internal override string Solve()
        {
            Boss boss;
            PlayerCharacter pc = new PlayerCharacter();

            Regex pattern = new Regex(BossAttributesPattern, RegexOptions.Compiled);
            try
            {
                Match match = pattern.Match(string.Join("\n", InputLines);
                int bossHitPoints = int.Parse(match.Groups["hitPoints"].Value);
                int bossDamage = int.Parse(match.Groups["damage"].Value);
                int bossArmor = int.Parse(match.Groups["armor"].Value);

                boss = new Boss(bossHitPoints, bossDamage, bossArmor);
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1(pc, boss);
            string part2 = SolvePart2();

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }
        
        private string SolvePart1(PlayerCharacter pc, Boss boss)
        {
            Entity result = SimulateFight(pc, boss);
            return "";
        }

        private string SolvePart2()
        {
            return "";
        }

        private Entity SimulateFight(PlayerCharacter pc, Boss boss)
        {
            int pcInitialHitPoints = pc.HitPoints;
            int bossInitialHitPoints = boss.HitPoints;
            bool playerCharacterTurn = true;

            while (pc.HitPoints > 0 || boss.HitPoints > 0)
            {
                if(playerCharacterTurn)
                {
                    boss.HitPoints = Math.Min(1, pc.Damage - boss.Armor);
                }
                else
                {
                    pc.HitPoints = Math.Min(1, boss.Damage - pc.Armor);
                }
                playerCharacterTurn = !playerCharacterTurn;
            }
            return pc.HitPoints > 0 ? pc : boss;
        }
    }
}