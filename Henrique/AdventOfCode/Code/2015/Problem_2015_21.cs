using AdventOfCode._2015_21;
using AdventOfCode.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode.Code
{
    public class Problem_2015_21 : Problem
    {
        private const string BossAttributesPattern = @"^Hit Points: (?<hitPoints>\d+)\nDamage: (?<damage>\d+)\nArmor: (?<armor>\d+)";

        public Problem_2015_21() : base()
        {
        }

        public override string Solve()
        {
            Boss boss;
            PlayerCharacter pc = new PlayerCharacter();

            Regex pattern = new Regex(BossAttributesPattern, RegexOptions.Compiled);
            try
            {
                Match match = pattern.Match(string.Join("\n", InputLines));
                int bossHitPoints = int.Parse(match.Groups["hitPoints"].Value);
                int bossDamage = int.Parse(match.Groups["damage"].Value);
                int bossArmor = int.Parse(match.Groups["armor"].Value);

                boss = new Boss(bossHitPoints, bossDamage, bossArmor);
            }
            catch
            {
                throw new Exception("Invalid line in input.");
            }

            HashSet<Inventory> allGeneratedInventoryCombinations = GenerateAllInventoryCombinations();

            string part1 = SolvePart1(pc, boss, allGeneratedInventoryCombinations);
            string part2 = SolvePart2(pc, boss, allGeneratedInventoryCombinations);

            return $"Part 1 solution: {part1}\nPart 2 solution: {part2}";

        }

        private string SolvePart1(PlayerCharacter pc, Boss boss, HashSet<Inventory> inventoryCombinations)
        {

            foreach (var inventory in inventoryCombinations.OrderBy(inventory => inventory.GetInventoryValue()))
            {
                pc.Inventory = inventory;
                Entity winner = SimulateFight(pc, boss);
                if (winner.GetType() == typeof(PlayerCharacter))
                {
                    return ((PlayerCharacter)winner).Inventory.GetInventoryValue().ToString();
                }
            }
            return "No solution found.";
        }

        private string SolvePart2(PlayerCharacter pc, Boss boss, HashSet<Inventory> inventoryCombinations)
        {
            foreach (var inventory in inventoryCombinations.OrderByDescending(inventory => inventory.GetInventoryValue()))
            {
                pc.Inventory = inventory;
                Entity winner = SimulateFight(pc, boss);
                if (winner.GetType() == typeof(Boss))
                {
                    return ((PlayerCharacter)pc).Inventory.GetInventoryValue().ToString();
                }
            }
            return "No solution found.";
        }

        private Entity SimulateFight(PlayerCharacter pc, Boss boss)
        {
            int pcInitialHitPoints = pc.GetHitPoints();
            int bossInitialHitPoints = boss.GetHitPoints();
            bool playerCharacterTurn = true;

            while (pc.GetHitPoints() > 0 && boss.GetHitPoints() > 0)
            {
                if (playerCharacterTurn)
                {
                    boss.HitPoints = boss.HitPoints - Math.Max(1, pc.GetDamage() - boss.GetArmor());
                }
                else
                {
                    pc.HitPoints = pc.HitPoints - Math.Max(1, boss.GetDamage() - pc.GetArmor());
                }
                playerCharacterTurn = !playerCharacterTurn;
            }

            Entity winner = pc.HitPoints > 0 ? pc : boss;
            pc.HitPoints = pcInitialHitPoints;
            boss.HitPoints = bossInitialHitPoints;
            return winner;
        }

        private HashSet<Inventory> GenerateAllInventoryCombinations()
        {
            IEnumerable<Item> availableWeapons = ItemsStore.AvailableItems.Where(item => item.Type == ItemType.Weapon);
            IEnumerable<Item> availableArmor = ItemsStore.AvailableItems.Where(item => item.Type == ItemType.Armor);
            IEnumerable<Item> availableRings = ItemsStore.AvailableItems.Where(item => item.Type == ItemType.Ring);

            Inventory newInventory = new Inventory();

            HashSet<Inventory> allCombinationsWithWeapons = new HashSet<Inventory>();
            HashSet<Inventory> allCombinationsWithArmor = new HashSet<Inventory>();
            HashSet<Inventory> allCombinationsWithRings = new HashSet<Inventory>();

            allCombinationsWithWeapons.UnionWith(GenerateWeaponCombinations(newInventory).ToList());

            foreach (var weapon in allCombinationsWithWeapons)
            {
                allCombinationsWithArmor.UnionWith(GenerateArmorCombinations(weapon).ToList());
                foreach (var armor in allCombinationsWithArmor)
                {
                    allCombinationsWithRings.UnionWith(GenerateRingCombinations(armor).ToList());
                }
            }
            return allCombinationsWithRings;
        }

        private HashSet<Inventory> GenerateWeaponCombinations(Inventory originalInvontory)
        {
            return GenerateCombinations(1, 1, ItemType.Weapon, originalInvontory);
        }

        private HashSet<Inventory> GenerateArmorCombinations(Inventory originalInvontory)
        {
            return GenerateCombinations(0, 1, ItemType.Armor, originalInvontory);
        }

        private HashSet<Inventory> GenerateRingCombinations(Inventory originalInvontory)
        {
            return GenerateCombinations(0, 2, ItemType.Ring, originalInvontory);
        }

        private HashSet<Inventory> GenerateCombinations(int minItems, int maxItems, ItemType itemType, Inventory originalInvontory)
        {
            HashSet<Inventory> generatedInventories = new();
            Inventory newInventory;
            List<Item> newInventoryItem;
            List<IEnumerable<Item>> allRingCombinations = new List<IEnumerable<Item>>();
            allRingCombinations = SetsGenerator<Item>.GenerateAllSets(minItems, maxItems, ItemsStore.AvailableItems.Where(item => item.Type == itemType)).ToList();

            if (minItems == 0)
            {
                newInventoryItem = new List<Item>(originalInvontory.Items);
                newInventory = new Inventory(newInventoryItem);
                generatedInventories.Add(newInventory);
            }

            foreach (var combination in allRingCombinations)
            {
                newInventoryItem = new List<Item>(originalInvontory.Items);
                newInventoryItem.AddRange(combination);
                newInventory = new Inventory(newInventoryItem);
                generatedInventories.Add(newInventory);
            }

            return generatedInventories;
        }


    }
}