namespace AdventOfCode._2015_15
{
    internal class Ingredient : IComparable<Ingredient>
    {
        internal Guid ID { get; }
        internal string Name { get; set; }
        internal int Quantity { get; set; }
        internal int Capacity { get; set; }
        internal int Durability { get; set; }
        internal int Flavor { get; set; }
        internal int Texture { get; set; }
        internal int Calories { get; set; }

        internal Ingredient(string name, int capacity, int durability, int flavor, int texture, int calories)
        {
            Quantity = 0;
            Name = name;
            Capacity = capacity;
            Durability = durability;
            Flavor = flavor;
            Texture = texture;
            Calories = calories;
            ID = Guid.NewGuid();
        }

        internal Ingredient Clone()
        {
            Ingredient clone = new(Name, Capacity, Durability, Flavor, Texture, Calories)
            {
                Quantity = Quantity
            };
            return clone;
        }

        internal int GetContributingFactor()
        {
            return Capacity + Durability + Flavor + Texture + Calories;
        }

        int IComparable<Ingredient>.CompareTo(Ingredient? other)
        {
            if (other == null) return 1;

            int thisContributingFactor = GetContributingFactor();
            int otherContributingFactor = other.GetContributingFactor();

            if (thisContributingFactor > otherContributingFactor)
            {
                return 1;
            }
            else if (thisContributingFactor < otherContributingFactor)
            {
                return -1;
            }
            else return 0;
        }
    }
}
