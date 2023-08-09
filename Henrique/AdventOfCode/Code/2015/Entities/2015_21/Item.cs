namespace AdventOfCode._2015_21
{
    internal class Item : IEquatable<Item>
    {
        internal ItemType Type { get; set; }
        internal string Name { get; set; }
        internal int Price { get; set; }
        internal int Damage { get; set; }
        internal int Armor { get; set; }

        internal Item(string name, ItemType type, int price, int damage, int armor)
        {
            Name = name;
            Type = type;
            Price = price;
            Damage = damage;
            Armor = armor;
        }

        public bool Equals(Item? other)
        {
            return Name == other?.Name;
        }
    }
}
