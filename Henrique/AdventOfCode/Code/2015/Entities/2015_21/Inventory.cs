namespace AdventOfCode._2015_21
{
    internal class Inventory : IEquatable<Inventory>
    {
        internal List<Item> Items = [];

        internal Inventory()
        {
            Items = [];
        }

        internal Inventory(IEnumerable<Item> items)
        {
            Items = [.. items];
        }

        internal int GetInventoryValue()
        {
            return Items.Sum(item => item.Price);
        }

        internal int GetDamage()
        {
            return Items.Sum(item => item.Damage);
        }

        internal int GetArmor()
        {
            return Items.Sum(item => item.Armor);
        }

        public bool Equals(Inventory? other)
        {
            return Items.Equals(other?.Items);
        }

        public override int GetHashCode()
        {
            return Items.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Inventory);
        }
    }
}
