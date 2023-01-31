using AdventOfCode.Code._2015.Entities._2015_21;
using AdventOfCode.Code._2015.Entities._2015_22;
using AdventOfCode.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_22 : Problem
    {
        private const string BossAttributesPattern = @"^Hit Points: (?<hitPoints>\d+)\nDamage: (?<damage>\d+)";
        private const int MaxPcTurns = 100;
        private const int MaxSpells = 20;

        internal Problem_2015_22() : base()
        {
        }

        internal override string Solve()
        {
            Boss boss;
            MagicPlayerCharacter pc = new MagicPlayerCharacter(10, 250);

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
            for (int i = 1; i <= MaxSpells; i++)
            {
                var validSpellLists = GenerateAllValidSpellLineups(i, boss.HitPoints, pc.Mana);

                foreach (var spellList in validSpellLists)
                {
                    Entity winner = SimulateFight(pc, boss, spellList.Spells);
                    if (winner.GetType() == typeof(MagicPlayerCharacter))
                    {
                        return spellList.Spells.Sum(spell => spell.ManaCost).ToString();
                    }
                }
            }
            throw new Exception("No solution found.");
        }

        private string SolvePart2(PlayerCharacter pc, Boss boss)
        {
            return "";
        }

        private Entity SimulateFight(MagicPlayerCharacter pc, Boss boss, List<Spell> orderedSpellList)
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

                if (pc.GetHitPoints() <= 0 || boss.GetHitPoints() <= 0 || pc.Mana <= 0)
                {
                    break;
                }

                if (playerCharacterTurn)
                {
                    Spell? castedSpell;
                    castedSpell = SelectNextCastedSpell(orderedSpellList, pcTurnNumber);

                    if(castedSpell == null)
                    {
                        break;
                    }

                    castedSpell.Cast(pc);
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
                }
                playerCharacterTurn = !playerCharacterTurn;
            }

            Entity winner = boss.HitPoints > 0 ? boss : pc;

            if (pc.Mana <= 0)
            {
                winner = boss;
            }

            pc.HitPoints = pcInitialHitPoints;
            pc.Armor = pcInitialArmor;
            pc.Mana = pcInitialMana;
            boss.HitPoints = bossInitialHitPoints;
            return winner;
        }

        private Spell? SelectNextCastedSpell(List<Spell> orderedSpellList, int turnNumber)
        {
            if(turnNumber >= orderedSpellList.Count)
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
                    endedSpells.Add(spell);
                }
            }

            foreach (Spell spell in endedSpells)
            {
                activeSpells.Remove(spell);
            }
        }

        private IEnumerable<SpellsLineup> GenerateAllValidSpellLineups(int size, int bossHP, int pcMana)
        {
            List<AvailableSpell> availableOptions = new List<AvailableSpell>()
            {
                AvailableSpell.MagicMissile,
                AvailableSpell.Drain,
                AvailableSpell.Shield,
                AvailableSpell.Poison,
                AvailableSpell.Recharge
            };
            IEnumerable<List<AvailableSpell>> allGeneratedSpellTypeLists = SetsGenerator<AvailableSpell>.GeneratePermutedSets(size, availableOptions);

            IEnumerable<List<Spell>> allGeneratedSpellLists = allGeneratedSpellTypeLists.Select(spellList => spellList.Select(spellType => SpellFactory.GetSpell(spellType)).ToList());

            var lineups = allGeneratedSpellLists.Select(filteredSpellList => new SpellsLineup(filteredSpellList));

            var filteredLineups = FilterValidSpellLists(lineups, bossHP, pcMana);
            SortSpellsByManaCost(filteredLineups);
            return filteredLineups;
        }

        private IEnumerable<SpellsLineup> FilterValidSpellLists(IEnumerable<SpellsLineup> spellLineups, int bossHP, int pcMana)
        {
            List<SpellsLineup> filteredLineups = new List<SpellsLineup>();

            // Filter by potential damage
            spellLineups = spellLineups.Where(spellLineup => spellLineup.CalculatePotentialDamage() >= bossHP);

            // Filter by potential mana lost
            spellLineups = spellLineups.Where(spellLineup => spellLineup.CalculatePotentialManaLost() <= pcMana);

            foreach (var lineup in spellLineups)
            {
                bool isSpellOrderValid = true;

                for (int i = 0; i < lineup.Spells.Count; i++)
                {
                    Spell spell = lineup.Spells[i];
                    if (!spell.HasImmediateEffect())
                    {
                        List<Type> previousSpells = lineup.Spells.Where((value, index) => index >= Math.Max(0, i - spell.TotalDuration) && index < i).Select(spell => spell.GetType()).ToList();
                        if (previousSpells.Contains(spell.GetType()))
                        {
                            isSpellOrderValid = false;
                            break;
                        }
                    }
                }

                if (isSpellOrderValid)
                {
                    filteredLineups.Add(lineup);
                }
            }
            return filteredLineups;
        }

        private void SortSpellsByManaCost(IEnumerable<SpellsLineup> spellLineups)
        {
            spellLineups = spellLineups.OrderBy(spellLineup => spellLineup.Spells.Sum(spell => spell.ManaCost)).ToList();
        }
    }
}