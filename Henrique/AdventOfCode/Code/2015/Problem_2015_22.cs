using AdventOfCode.Code._2015.Entities._2015_21;
using AdventOfCode.Code._2015.Entities._2015_22;
using AdventOfCode.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_22 : Problem
    {
        private const string BossAttributesPattern = @"^Hit Points: (?<hitPoints>\d+)\nDamage: (?<damage>\d+)";
        private const int MaxPcTurns = 100;
        private const int MaxSpells = 100;
        private const int MaxIterations = 2000;
        private const int nChromossomes = 100;

        internal Problem_2015_22() : base()
        {
        }

        internal override string Solve()
        {
            Boss boss;
            MagicPlayerCharacter pc = new MagicPlayerCharacter(50, 500);

            Regex pattern = new Regex(BossAttributesPattern, RegexOptions.Compiled);
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

            return $"Part 1 solution: " + part1 + "\n"
                + "Part 2 solution: " + part2;
        }

        private string SolvePart1(MagicPlayerCharacter pc, Boss boss)
        {
            SpellsLineup? bestLineup = null;

            List<SpellType> availableOptions = new List<SpellType>()
            {
                SpellType.MagicMissile,
                SpellType.Drain,
                SpellType.Shield,
                SpellType.Poison,
                SpellType.Recharge
            };

            for(int nSpells = 10; nSpells < MaxSpells; nSpells++)
            {
                List<SpellsLineup> validSpellLineups = GenerateFirstGeneration(nSpells, availableOptions, nChromossomes, pc.GetMana(), pc.GetHitPoints(), boss.GetHitPoints(), boss.GetDamage()).ToList();
                
                if (!validSpellLineups.Any())
                {
                    continue;
                }
                
                for (int nIterations = 0; nIterations < MaxIterations; nIterations++)
                {
                    foreach (var spellLineup in validSpellLineups)
                    {
                        Entity winner = SimulateFight(pc, boss, spellLineup);
                        if (winner.GetType() == typeof(MagicPlayerCharacter))
                        {
                            if (bestLineup == null || bestLineup.Spells.Sum(spell => spell.ManaCost) > spellLineup.Spells.Sum(spell => spell.ManaCost))
                            {
                                bestLineup = spellLineup;
                            }
                        }
                    }

                    validSpellLineups = SortSpellLineups(validSpellLineups, pc.GetMana(), pc.GetHitPoints(), boss.GetHitPoints(), boss.GetDamage());
                    validSpellLineups = GenerateNextGeneration(validSpellLineups, availableOptions, nChromossomes, pc.GetMana(), pc.GetHitPoints(), boss.GetHitPoints(), boss.GetDamage()).ToList();
                }
                if (bestLineup != null)
                {
                    return bestLineup.Spells.Sum(spell => spell.ManaCost).ToString();
                }
            }            

            

            throw new Exception("No solution found.");
        }

        private string SolvePart2(PlayerCharacter pc, Boss boss)
        {
            return "";
        }

        private IEnumerable<SpellsLineup> GenerateFirstGeneration(int numberOfSpells, IEnumerable<SpellType> availableOptions, int nChromossomes, int pcMana, int pcHitPoints, int bossHP, int bossDamage)
        {
            List<SpellsLineup> result = new List<SpellsLineup>();
            List<SpellType> spellTypes = availableOptions.ToList();
            Random random = new Random();

            for (int i = 0, generatedLineups = 0; i < MaxIterations && generatedLineups < nChromossomes; i++)
            {
                List<Spell> spells = new List<Spell>();
                for (int j = 0; j < numberOfSpells; j++)
                {
                    spells.Add(SpellFactory.GetSpell(spellTypes[random.Next(0, spellTypes.Count)]));
                }
                SpellsLineup lineup = new SpellsLineup(spells);
                if (lineup.IsSpellOrderValid())
                {
                    result.Add(lineup);
                    generatedLineups++;
                }
            }
            return result;
        }

        private IEnumerable<SpellsLineup> GenerateNextGeneration(IEnumerable<SpellsLineup> spellsLineups, IEnumerable<SpellType> availableOptions, int nChromossomes, int pcMana, int pcHitPoints, int bossHP, int bossDamage)
        {
            List<SpellsLineup> result = new List<SpellsLineup>();
            List<SpellsLineup> spellsLineupsList = spellsLineups.ToList();
            List<SpellType> spellTypes = availableOptions.ToList();

            Random randomGenerator = new Random();

            // Retain Elite - The 10% better spellLineups from the previous generation will automatically pass to the next generation
            var elite = spellsLineupsList.Take((int)(0.1 * nChromossomes));
            result.AddRange(elite);

            // Select Parents - The remaining 90% of the next generation will be created by reproducing the parents from the current generation
            for (int i = 0, generatedLineups = 0; i < MaxIterations && generatedLineups < (int)(0.9 * nChromossomes); i++)
            {
                SpellsLineup parent1 = spellsLineupsList[randomGenerator.Next(0, spellsLineupsList.Count)];
                SpellsLineup parent2 = spellsLineupsList[randomGenerator.Next(0, spellsLineupsList.Count)];

                List<Spell> spells = new List<Spell>();
                for (int geneN = 0; geneN < parent1.Spells.Count; geneN++)
                { 
                    int randomPercentage = randomGenerator.Next(0, 100);

                    // Select gene from parent1 with probability of 45%
                    if (randomPercentage < 45)
                    {
                        spells.Add(SpellFactory.GetSpell(parent1.Spells[geneN].Type));
                    }
                    // Select gene from parent2 with probability of 45%
                    else if (randomPercentage < 90)
                    {
                        spells.Add(SpellFactory.GetSpell(parent2.Spells[geneN].Type));
                    }
                    // Mutate with probability of 10%
                    else
                    {
                        spells.Add(SpellFactory.GetSpell(spellTypes[randomGenerator.Next(0, availableOptions.Count())]));
                    }
                }
                SpellsLineup lineup = new SpellsLineup(spells);
                if(lineup.IsSpellOrderValid())
                {
                    result.Add(lineup);
                    generatedLineups++;
                }
            }
            return result;
        }

        private Entity SimulateFight(MagicPlayerCharacter pc, Boss boss, SpellsLineup spellsLineup)
        {
            int pcInitialHitPoints = pc.GetHitPoints();
            int pcInitialArmor = pc.GetArmor();
            int pcInitialMana = pc.Mana;
            int bossInitialHitPoints = boss.GetHitPoints();
            bool playerCharacterTurn = true;
            int pcTurnNumber = 0;

            List<Spell> activeSpells = new List<Spell>();

            while (pcTurnNumber < MaxPcTurns)
            {
                ApplyActiveEffects(pc, boss, activeSpells);

                if (boss.GetHitPoints() <= 0)
                {
                    ResetEntitiesValues(pc, boss, pcInitialHitPoints, pcInitialArmor, pcInitialMana, bossInitialHitPoints);
                    spellsLineup.TurnsSurvived = pcTurnNumber;
                    return pc;
                }

                if (playerCharacterTurn)
                {
                    Spell? castedSpell;
                    castedSpell = SelectNextCastedSpell(spellsLineup.Spells, pcTurnNumber);

                    if (castedSpell == null)
                    {
                        ResetEntitiesValues(pc, boss, pcInitialHitPoints, pcInitialArmor, pcInitialMana, bossInitialHitPoints);
                        spellsLineup.TurnsSurvived = pcTurnNumber;
                        return boss;
                    }

                    castedSpell.Cast(pc);

                    if (pc.Mana < 0)
                    {
                        ResetEntitiesValues(pc, boss, pcInitialHitPoints, pcInitialArmor, pcInitialMana, bossInitialHitPoints);
                        spellsLineup.TurnsSurvived = pcTurnNumber;
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
                    pc.HitPoints = pc.HitPoints - Math.Max(1, boss.GetDamage() - pc.GetArmor());

                    if (pc.HitPoints <= 0)
                    {
                        ResetEntitiesValues(pc, boss, pcInitialHitPoints, pcInitialArmor, pcInitialMana, bossInitialHitPoints);
                        spellsLineup.TurnsSurvived = pcTurnNumber;
                        return boss;
                    }
                }
                playerCharacterTurn = !playerCharacterTurn;
            }

            ResetEntitiesValues(pc, boss, pcInitialHitPoints, pcInitialArmor, pcInitialMana, bossInitialHitPoints);
            spellsLineup.TurnsSurvived = pcTurnNumber;
            return boss;
        }

        private void ResetEntitiesValues(MagicPlayerCharacter pc, Boss boss, int pcInitialHitPoints, int pcInitialArmor, int pcInitialMana, int bossInitialHitPoints)
        {
            pc.HitPoints = pcInitialHitPoints;
            pc.Armor = pcInitialArmor;
            pc.Mana = pcInitialMana;
            boss.HitPoints = bossInitialHitPoints;
        }

        private Spell? SelectNextCastedSpell(List<Spell> orderedSpellList, int turnNumber)
        {
            if (turnNumber >= orderedSpellList.Count)
            {
                return null;
            }
            return orderedSpellList[turnNumber];
        }

        private void ApplyActiveEffects(MagicPlayerCharacter pc, Boss boss, List<Spell> activeSpells)
        {
            if (activeSpells == null)
            {
                return;
            }
            List<Spell> endedSpells = new List<Spell>();
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

        //private IEnumerable<SpellsLineup> GenerateAllPotentialSpellCombinations(int size, int pcMana, int pcHitPoints, int bossHP, int bossDamage)
        //{
        //    List<SpellType> availableOptions = new List<SpellType>()
        //    {
        //        SpellType.MagicMissile,
        //        SpellType.Drain,
        //        SpellType.Shield,
        //        SpellType.Poison,
        //        SpellType.Recharge
        //    };

        //    IEnumerable<IEnumerable<SpellType>> allGeneratedSpellTypeLists = SetsGenerator<SpellType>.GenerateCombinationsWithRepetition(size, availableOptions);
        //    IEnumerable<List<Spell>> allGeneratedSpellLists = allGeneratedSpellTypeLists.Select(spellList => spellList.Select(spellType => SpellFactory.GetSpell(spellType)).ToList());
        //    var lineups = allGeneratedSpellLists.Select(filteredSpellList => new SpellsLineup(filteredSpellList));
        //    var filteredLineups = FilterPotentialSpellLists(lineups, pcMana, pcHitPoints, bossHP, bossDamage);
        //    SortSpellLineups(filteredLineups);

        //    return filteredLineups;
        //}

        private IEnumerable<SpellsLineup> FilterPotentialSpellLists(IEnumerable<SpellsLineup> spellLineups, int pcMana, int pcHitPoints, int bossHP, int bossDamage)
        {
            // Filter by potential damage
            spellLineups = spellLineups.Where(spellLineup => spellLineup.CalculatePotentialDamage() >= bossHP);

            // Filter by potential mana spent
            spellLineups = spellLineups.Where(spellLineup => spellLineup.CalculatePotentialManaSpent() <= pcMana);

            // Filter by potential damage dealt to the player
            spellLineups = spellLineups.Where(spellLineup => spellLineup.CalculatePotentialDamageToPlayer(bossDamage) <= pcHitPoints);

            return spellLineups;
        }

        private List<SpellsLineup> SortSpellLineups(IEnumerable<SpellsLineup> spellLineups, int pcMana, int pcHitPoints, int bossHP, int bossDamage)
        {
            return spellLineups.OrderByDescending(spellLineup => spellLineup.ComputeFitness(pcMana, pcHitPoints, bossHP, bossDamage)).ThenBy(spellLineup => spellLineup.Spells.Sum(spell => spell.ManaCost)).ToList();
        }
    }
}