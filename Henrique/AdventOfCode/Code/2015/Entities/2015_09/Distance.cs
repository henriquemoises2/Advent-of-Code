namespace AdventOfCode._2015_9
{
    internal class Distance
    {
        internal string StartingLocation { get; set; }
        internal string EndingLocation { get; set; }
        internal int Cost { get; set; }

        internal Distance(string startingLocation, string endingLocation, int cost)
        {
            StartingLocation = startingLocation;
            EndingLocation = endingLocation;
            Cost = cost;
        }
    }
}
