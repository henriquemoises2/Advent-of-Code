namespace AdventOfCode._2015_19
{
    internal class Molecule : IEquatable<Molecule>
    {
        internal int Position { get; }
        internal string Value { get; set; }

        internal Molecule(int position, string value)
        {
            Position = position;
            Value = value;
        }

        public bool Equals(Molecule? other)
        {
            if (other == null)
            {
                return false;
            }
            return other.Value == Value && other.Position == Position;
        }

        public override int GetHashCode()
        {
            int hashValue = Value.GetHashCode();
            int hashPosition = Position.GetHashCode();

            return hashValue ^ hashPosition;
        }
    }

}
