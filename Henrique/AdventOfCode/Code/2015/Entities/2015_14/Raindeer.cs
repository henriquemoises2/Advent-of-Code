namespace AdventOfCode._2015_14
{
    internal class Raindeer
    {
        internal string Name { get; set; }
        internal int Speed { get; set; }
        internal int FlyTime { get; set; }
        internal int RestTime { get; set; }
        internal int TraveledDistance { get; set; }
        internal int AccumulatedPoints { get; set; }


        private RaindeerState State { get; set; }

        internal Raindeer(string name, int speed, int flyTime, int restTime)
        {
            Name = name;
            Speed = speed;
            FlyTime = flyTime;
            RestTime = restTime;
            State = new FlyingRaindeerState(this);
            TraveledDistance = 0;
            AccumulatedPoints = 0;
        }

        internal void ChangeState(RaindeerState state)
        {
            State = state;
            State.Raindeer = this;
        }

        internal void ActForN(int seconds)
        {
            State.ActForN(seconds);
        }

        internal void ActForSingle()
        {
            State.ActForSingle();
        }

    }
}
