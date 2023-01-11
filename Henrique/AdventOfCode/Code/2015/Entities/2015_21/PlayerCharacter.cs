namespace AdventOfCode.Code._2015.Entities._2015_21
{
    internal class PlayerCharacter : Entity
    {
        private const int InitialHitPoints = 100;

        internal Inventory Inventory { get; set; } = new Inventory();

        internal PlayerCharacter(int hitPoints = InitialHitPoints) : base(hitPoints, 0, 0)
        {
        }

        internal PlayerCharacter(int hitPoints, int damage, int armor) : base(hitPoints, damage, armor)
        {
        }

        internal override int GetHitPoints()
        {
            return HitPoints;
        }

        internal override int GetDamage()
        {
            return Damage + Inventory.GetDamage();
        }

        internal override int GetArmor()
        {
            return Armor + Inventory.GetArmor();
        }
    }
}
