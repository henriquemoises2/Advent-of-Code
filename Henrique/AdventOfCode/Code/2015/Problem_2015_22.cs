using AdventOfCode.Code._2015.Entities._2015_21;
using AdventOfCode.Code._2015.Entities._2015_22;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    internal class Problem_2015_22 : Problem
    {
        private const string BossAttributesPattern = @"^Hit Points: (?<hitPoints>\d+)\nDamage: (?<damage>\d+)";
        private const int MaxPcTurns = 1000;

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
            List<Spell> spellList = GenerateValidRandomSpellList(5).ToList();
            Entity winner = SimulateFight(pc, boss, spellList);
            if (winner.GetType() == typeof(MagicPlayerCharacter))
            {
                return spellList.Sum(spell => spell.ManaCost).ToString();
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
                    Spell castedSpell;
                    if (orderedSpellList.Any())
                    {
                        castedSpell = SelectNextCastedSpell(orderedSpellList, pcTurnNumber);
                    }
                    else
                    {
                        castedSpell = GenerateNextValidRandomSpell(pc, orderedSpellList);
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

            Entity winner = pc.HitPoints > 0 ? pc : boss;

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

        private Spell SelectNextCastedSpell(List<Spell> orderedSpellList, int turnNumber)
        {
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

        private IEnumerable<Spell> GenerateValidRandomSpellList(int size)
        {
            List<Spell> spellList = new List<Spell>();
            while (size > 0)
            {
                Spell? spell = GenerateRandomSpell();
                spellList.Add(spell);
                size--;
            }
            return spellList;
        }

        private Spell GenerateRandomSpell()
        {
            List<AvailableSpells> availableOptions = new List<AvailableSpells>()
            {
                AvailableSpells.Recharge,
                AvailableSpells.Shield,
                AvailableSpells.Drain,
                AvailableSpells.Poison,
                AvailableSpells.MagicMissile
            };

            Random random = new Random();
            AvailableSpells spellType = availableOptions.ElementAt(random.Next(0, availableOptions.Count));
            Spell? spell = SpellFactory.GetSpell(spellType);
            return spell;
        }

        private Spell? GenerateNextValidRandomSpell(MagicPlayerCharacter pc, IEnumerable<Spell> currentSpellList)
        {
            List<AvailableSpells> availableOptions = new List<AvailableSpells>()
            {
                AvailableSpells.Recharge,
                AvailableSpells.Shield,
                AvailableSpells.Drain,
                AvailableSpells.Poison,
                AvailableSpells.MagicMissile
            };

            Random random = new Random();
            
            while(availableOptions.Any())
            {
                AvailableSpells spellType = availableOptions.ElementAt(random.Next(0, availableOptions.Count));

                if (!IsValidSpell(pc, currentSpellList))
                {
                    availableOptions.Remove(spellType);
                }
                else
                {
                    Spell? spell = SpellFactory.GetSpell(spellType);
                    return spell;
                }
            }

            return null;
            
        }

        private bool IsValidSpell(MagicPlayerCharacter pc, IEnumerable<Spell> currentSpellList)
        {
            return true;
        }
    }
}