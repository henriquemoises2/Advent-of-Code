namespace AdventOfCode.Code._2015.Entities._2015_21
{
    internal abstract class Entity
    {
        internal int HitPoints { get; set; }
        internal int Damage { get; set; }
        internal int Armor { get; set; }

        internal Entity(int hitPoints, int damage, int armor)
        {
            HitPoints = hitPoints;
            Damage = damage;
            Armor = armor;
        }
    }
}
