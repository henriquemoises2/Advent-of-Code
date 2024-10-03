using AdventOfCode._2015_21;
using AdventOfCode._2015_22;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public partial class Problem_2015_22 : Problem
    {
        private const string BossAttributesPattern = @"^Hit Points: (?<hitPoints>\d+)\nDamage: (?<damage>\d+)";
        private const int MinSpells = 8;
        private const int MaxSpells = 15;
        private const int MaxIterations = 4000;
        private const int NChromossomes = 50;
        private const int PlayerHitpoints = 50;
        private const int PlayerMana = 500;

        public Problem_2015_22() : base()
        {
        }

        public override string Solve()
        {
            Boss boss;
            MagicPlayerCharacter pc = new(PlayerHitpoints, PlayerMana);

            Regex pattern = MyRegex();
            try
            {
                Match match = pattern.Match(string.Join("\n", InputLines));
                int bossHitPoints = int.Parse(match.Groups["hitPoints"].Value);
                int bossDamage = int.Parse(match.Groups["damage"].Value);

                boss = new Boss(bossHitPoints, bossDamage, 0);
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            string part1 = SolvePart1(pc, boss);
            string part2 = SolvePart2(pc, boss);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private static string SolvePart1(MagicPlayerCharacter pc, Boss boss)
        {
            SpellsLineup? solution = SolveProblem(pc, boss);
            if (solution != null)
            {
                return solution.ManaSpent.ToString();
            }
            throw new Exception("No solution found.");
        }

        private static string SolvePart2(MagicPlayerCharacter pc, Boss boss)
        {
            SpellsLineup? solution = SolveProblem(pc, boss, hardMode: true);
            if (solution != null)
            {
                return solution.ManaSpent.ToString();
            }
            throw new Exception("No solution found.");
        }

        /// <summary>
        /// The problem was solved using a Genetic Algorithm to search for the optimal solution. The genetic algorithm itself does not guarantee the optimal solution,
        /// but with the defined parameters it gives the correct answer most times. The parameters had to be tweaked over the development time and from the trial they seem to be the ones
        /// that provide the correct answer most times.
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="boss"></param>
        /// <param name="hardMode"></param>
        /// <returns></returns>
        private static SpellsLineup? SolveProblem(MagicPlayerCharacter pc, Boss boss, bool hardMode = false)
        {
            SpellsLineup? bestLineup = null;
            Dictionary<int, SpellsLineup> results = [];


            List<SpellType> availableOptions =
            [
                SpellType.MagicMissile,
                SpellType.Drain,
                SpellType.Shield,
                SpellType.Poison,
                SpellType.Recharge
            ];

            // Tries to find spell orders that defeat the boss inside the [MinSpells,MaxSpells] interval
            for (int nSpells = MinSpells; nSpells <= MaxSpells; nSpells++)
            {
                // Generated the first generation of genomes at random, i.e. with random spells as chromossomes
                List<SpellsLineup> validSpellLineups = [.. GenerateFirstGeneration(nSpells, availableOptions, NChromossomes)];

                if (validSpellLineups.Count == 0)
                {
                    continue;
                }

                for (int nIterations = 1; nIterations < MaxIterations; nIterations++)
                {
                    foreach (var spellLineup in validSpellLineups)
                    {

                        if (spellLineup.IsSpellOrderValid())
                        {
                            Entity winner = SimulateFight(pc, boss, spellLineup, hardMode);
                            if (winner.GetType() == typeof(MagicPlayerCharacter))
                            {
                                if (results.TryGetValue(nSpells, out SpellsLineup? bestSoFar))
                                {
                                    if (bestSoFar.ManaSpent > spellLineup.ManaSpent)
                                    {
                                        results[nSpells] = spellLineup;
                                    }
                                }
                                else
                                {
                                    results.Add(nSpells, spellLineup);
                                }

                            }
                        }
                    }
                    // Sorts genomes by the following criteria: Damage dealt to boss, turns survived and mana spent
                    validSpellLineups = SortSpellLineups(validSpellLineups);
                    // Generates next generation by combining the top 10% best genomes and 90% of the remaining genomes through reproduction, with a 20% chance of mutation for each reproduced chromossome.
                    validSpellLineups = [.. GenerateNextGeneration(validSpellLineups, availableOptions, NChromossomes)];
                }

                if (results.TryGetValue(nSpells, out SpellsLineup? bestLineupThisNumberSpells) && results.TryGetValue(nSpells - 1, out SpellsLineup? bestLineupPreviousNumberSpells))
                {
                    if (bestLineupThisNumberSpells.ManaSpent >= bestLineupPreviousNumberSpells.ManaSpent)
                    {
                        break;
                    }
                }

            }
            if (results.Count != 0)
            {
                return results.OrderBy(lineup => lineup.Value.ManaSpent).First().Value;
            }

            return bestLineup;
        }


        private static List<SpellsLineup> GenerateFirstGeneration(int numberOfSpells, IEnumerable<SpellType> availableOptions, int nChromossomes)
        {
            List<SpellsLineup> result = [];
            List<SpellType> spellTypes = availableOptions.ToList();
            Random random = new();

            for (int i = 0, generatedLineups = 0; generatedLineups < nChromossomes; i++)
            {
                List<Spell> spells = [];
                for (int j = 0; j < numberOfSpells; j++)
                {
                    spells.Add(SpellFactory.GetSpell(spellTypes[random.Next(0, spellTypes.Count)]));
                }
                SpellsLineup lineup = new(spells);
                result.Add(lineup);
                generatedLineups++;
            }
            return result;
        }

        private static List<SpellsLineup> GenerateNextGeneration(IEnumerable<SpellsLineup> spellsLineups, IEnumerable<SpellType> availableOptions, int nChromossomes)
        {
            List<SpellsLineup> result = [];
            List<SpellsLineup> spellsLineupsList = spellsLineups.ToList();
            List<SpellType> spellTypes = availableOptions.ToList();

            Random randomGenerator = new();

            // Retain Elite - The 10% better spellLineups from the previous generation will automatically pass to the next generation
            var elite = spellsLineupsList.Take((int)(0.1 * nChromossomes));
            result.AddRange(elite);

            // Select Parents - The remaining 90% of the next generation will be created by reproducing the parents from the current generation
            for (int i = 0, generatedLineups = 0; generatedLineups < (int)(0.9 * nChromossomes); i++)
            {
                SpellsLineup parent1 = spellsLineupsList[randomGenerator.Next(0, spellsLineupsList.Count)];
                SpellsLineup parent2 = spellsLineupsList[randomGenerator.Next(0, spellsLineupsList.Count)];

                List<Spell> spells = [];
                for (int geneN = 0; geneN < parent1.Spells.Count; geneN++)
                {
                    int randomPercentage = randomGenerator.Next(1, 101);

                    // Select gene from parent1 with probability of 40%
                    if (randomPercentage <= 40)
                    {
                        spells.Add(SpellFactory.GetSpell(parent1.Spells[geneN].Type));
                    }
                    // Select gene from parent2 with probability of 40%
                    else if (randomPercentage <= 80)
                    {
                        spells.Add(SpellFactory.GetSpell(parent2.Spells[geneN].Type));
                    }
                    // Mutate with probability of 20%
                    else
                    {
                        spells.Add(SpellFactory.GetSpell(spellTypes[randomGenerator.Next(0, availableOptions.Count())]));
                    }
                }
                SpellsLineup lineup = new(spells);
                if (lineup.IsSpellOrderValid())
                {
                    result.Add(lineup);
                    generatedLineups++;
                }
            }
            return result;
        }

        private static Entity SimulateFight(MagicPlayerCharacter pc, Boss boss, SpellsLineup spellsLineup, bool hardMode)
        {
            int pcInitialHitPoints = pc.GetHitPoints();
            int pcInitialArmor = pc.GetArmor();
            int pcInitialMana = pc.Mana;
            int bossInitialHitPoints = boss.GetHitPoints();
            bool playerCharacterTurn = true;
            int pcTurnNumber = 0;

            List<Spell> activeSpells = [];

            while (pcTurnNumber < MaxSpells)
            {
                if (playerCharacterTurn && hardMode)
                {
                    pc.HitPoints -= 1;
                    if (pc.HitPoints <= 0)
                    {
                        UpdateSpellsLineup(spellsLineup, pcTurnNumber, bossInitialHitPoints, boss.HitPoints);
                        ResetEntitiesValues(pc, boss, pcInitialHitPoints, pcInitialArmor, pcInitialMana, bossInitialHitPoints);
                        return boss;
                    }
                }

                ApplyActiveEffects(pc, boss, activeSpells);

                if (boss.GetHitPoints() <= 0)
                {
                    UpdateSpellsLineup(spellsLineup, pcTurnNumber, bossInitialHitPoints, boss.HitPoints);
                    ResetEntitiesValues(pc, boss, pcInitialHitPoints, pcInitialArmor, pcInitialMana, bossInitialHitPoints);
                    return pc;
                }

                if (playerCharacterTurn)
                {
                    Spell? castedSpell;
                    castedSpell = SelectNextCastedSpell(spellsLineup.Spells, pcTurnNumber);

                    if (castedSpell == null)
                    {
                        UpdateSpellsLineup(spellsLineup, pcTurnNumber, bossInitialHitPoints, boss.HitPoints);
                        ResetEntitiesValues(pc, boss, pcInitialHitPoints, pcInitialArmor, pcInitialMana, bossInitialHitPoints);
                        return boss;
                    }

                    castedSpell.Cast(pc);
                    spellsLineup.ManaSpent += castedSpell.ManaCost;

                    if (pc.Mana < 0)
                    {
                        UpdateSpellsLineup(spellsLineup, pcTurnNumber, bossInitialHitPoints, boss.HitPoints);
                        ResetEntitiesValues(pc, boss, pcInitialHitPoints, pcInitialArmor, pcInitialMana, bossInitialHitPoints);
                        return boss;
                    }

                    if (castedSpell.HasImmediateEffect())
                    {
                        castedSpell.ApplyEffect(pc, boss);
                    }
                    else
                    {
                        activeSpells.Add(castedSpell);
                    }
                    pcTurnNumber++;
                }
                else
                {
                    pc.HitPoints -= Math.Max(1, boss.GetDamage() - pc.GetArmor());

                    if (pc.HitPoints <= 0)
                    {
                        UpdateSpellsLineup(spellsLineup, pcTurnNumber, bossInitialHitPoints, boss.HitPoints);
                        ResetEntitiesValues(pc, boss, pcInitialHitPoints, pcInitialArmor, pcInitialMana, bossInitialHitPoints);
                        return boss;
                    }
                }
                playerCharacterTurn = !playerCharacterTurn;
            }

            spellsLineup.TurnsSurvived = pcTurnNumber;
            spellsLineup.DamageDealtToBoss = bossInitialHitPoints - boss.HitPoints;
            ResetEntitiesValues(pc, boss, pcInitialHitPoints, pcInitialArmor, pcInitialMana, bossInitialHitPoints);
            return boss;
        }

        private static void UpdateSpellsLineup(SpellsLineup spellsLineup, int pcTurnNumber, int bossInitialHitPoints, int bossHitpoints)
        {
            spellsLineup.TurnsSurvived = pcTurnNumber;
            spellsLineup.DamageDealtToBoss = bossInitialHitPoints - bossHitpoints;
        }

        private static void ResetEntitiesValues(MagicPlayerCharacter pc, Boss boss, int pcInitialHitPoints, int pcInitialArmor, int pcInitialMana, int bossInitialHitPoints)
        {
            pc.HitPoints = pcInitialHitPoints;
            pc.Armor = pcInitialArmor;
            pc.Mana = pcInitialMana;
            boss.HitPoints = bossInitialHitPoints;
        }

        private static Spell? SelectNextCastedSpell(List<Spell> orderedSpellList, int turnNumber)
        {
            if (turnNumber >= orderedSpellList.Count)
            {
                return null;
            }
            return orderedSpellList[turnNumber];
        }

        private static void ApplyActiveEffects(MagicPlayerCharacter pc, Boss boss, List<Spell> activeSpells)
        {
            if (activeSpells == null)
            {
                return;
            }

            List<Spell> endedSpells = [];
            foreach (Spell spell in activeSpells)
            {
                spell.ApplyEffect(pc, boss);
                if (spell.Timer == 0)
                {
                    spell.RemoveEffect(pc, boss);
                    endedSpells.Add(spell);
                }
            }

            foreach (Spell spell in endedSpells)
            {
                activeSpells.Remove(spell);
            }
        }

        private static List<SpellsLineup> SortSpellLineups(IEnumerable<SpellsLineup> spellLineups)
        {
            return [.. spellLineups
                .OrderByDescending(spellLineup => spellLineup.DamageDealtToBoss)
                .OrderByDescending(spellLineup => spellLineup.TurnsSurvived)
                .ThenBy(spellLineup => spellLineup.ManaSpent)];
        }

        [GeneratedRegex(BossAttributesPattern, RegexOptions.Compiled)]
        private static partial Regex MyRegex();
    }
}