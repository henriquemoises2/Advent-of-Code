namespace AdventOfCode.Code._2015.Entities._2015_21
{
    internal static class ItemsStore
    {
        internal static IEnumerable<Item> AvailableItems
        {
            get
            {
                return new List<Item>
                {
                    new Item("Dagger", ItemType.Weapon, 8, 4, 0),
                    new Item("Shortsword", ItemType.Weapon, 10, 5, 0),
                    new Item("Warhammer", ItemType.Weapon, 25, 6, 0),
                    new Item("Longsword", ItemType.Weapon, 40, 7, 0),
                    new Item("Greataxe", ItemType.Weapon, 74, 8, 0),

                    new Item("Leather", ItemType.Armor, 13, 0, 1),
                    new Item("Chainmail", ItemType.Armor, 31, 0, 2),
                    new Item("Splintmail", ItemType.Armor, 53, 0, 3),
                    new Item("Bandedmail", ItemType.Armor, 75, 0, 4),
                    new Item("Platemail", ItemType.Armor, 102, 0, 5),

                    new Item("Damage+1", ItemType.Ring, 25, 1, 0),
                    new Item("Damage+2", ItemType.Ring, 50, 2, 0),
                    new Item("Damage+3", ItemType.Ring, 100, 3, 0),
                    new Item("Defense+1", ItemType.Ring, 20, 0, 1),
                    new Item("Defense+2", ItemType.Ring, 40, 0, 2),
                    new Item("Defense+3", ItemType.Ring, 80, 0, 3)
                };
            }

        }
    }
}
