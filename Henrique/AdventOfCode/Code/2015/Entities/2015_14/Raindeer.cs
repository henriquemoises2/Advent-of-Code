using AdventOfCode.Code._2015.Entities._2015_14;

namespace AdventOfCode.Code._2015_14
{
    internal class Raindeer
    {
        internal string Name { get; set; }
        internal int Speed { get; set; }
        internal int FlyTime { get; set; }
        internal int RestTime { get; set; }
        internal int TraveledDistance { get; set; }
        internal int AccumulatedPoints { get; set; }


        private RaindeerState _state { get; set; }

        internal Raindeer(string name, int speed, int flyTime, int restTime)
        {
            Name = name;
            Speed = speed;
            FlyTime = flyTime;
            RestTime = restTime;
            _state = new FlyingRaindeerState(this);
            TraveledDistance = 0;
            AccumulatedPoints = 0;
        }

        internal void ChangeState(RaindeerState state)
        {
            _state = state;
            _state.Raindeer = this;
        }

        internal void ActForN(int seconds)
        {
            _state.ActForN(seconds);
        }

        internal void ActForSingle()
        {
            _state.ActForSingle();
        }

    }
}
