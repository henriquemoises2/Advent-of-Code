namespace AdventOfCode._2015_21
{
    internal static class ItemsStore
    {
        internal static IEnumerable<Item> AvailableItems
        {
            get
            {
                return new List<Item>
                {
                    new("Dagger", ItemType.Weapon, 8, 4, 0),
                    new("Shortsword", ItemType.Weapon, 10, 5, 0),
                    new("Warhammer", ItemType.Weapon, 25, 6, 0),
                    new("Longsword", ItemType.Weapon, 40, 7, 0),
                    new("Greataxe", ItemType.Weapon, 74, 8, 0),

                    new("Leather", ItemType.Armor, 13, 0, 1),
                    new("Chainmail", ItemType.Armor, 31, 0, 2),
                    new("Splintmail", ItemType.Armor, 53, 0, 3),
                    new("Bandedmail", ItemType.Armor, 75, 0, 4),
                    new("Platemail", ItemType.Armor, 102, 0, 5),

                    new("Damage+1", ItemType.Ring, 25, 1, 0),
                    new("Damage+2", ItemType.Ring, 50, 2, 0),
                    new("Damage+3", ItemType.Ring, 100, 3, 0),
                    new("Defense+1", ItemType.Ring, 20, 0, 1),
                    new("Defense+2", ItemType.Ring, 40, 0, 2),
                    new("Defense+3", ItemType.Ring, 80, 0, 3)
                };
            }

        }
    }
}
