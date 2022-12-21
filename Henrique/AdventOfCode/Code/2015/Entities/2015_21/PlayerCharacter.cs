namespace AdventOfCode.Code._2015.Entities._2015_21
{
    internal class PlayerCharacter : Entity
    {
        private const int InitialHitPoints = 100;
        internal IEnumerable<Item> Inventory { get; set; } = new List<Item>();

        public PlayerCharacter(int hitPoints = InitialHitPoints) : base(hitPoints, 0, 0)
        {
        }

        public PlayerCharacter(int hitPoints, int damage, int armor) : base(hitPoints, damage, armor)
        {
        }
    }
}
