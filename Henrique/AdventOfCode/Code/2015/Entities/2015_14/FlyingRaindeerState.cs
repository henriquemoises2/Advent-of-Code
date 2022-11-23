using AdventOfCode.Code._2015_14;

namespace AdventOfCode.Code._2015.Entities._2015_14
{
    internal class FlyingRaindeerState : RaindeerState
    {
        public FlyingRaindeerState(Raindeer raindeer) : base(raindeer)
        {
            SecondsInState = 0;
        }

        internal override void ActForN(int seconds)
        {
            int realFlyTime;

            if (seconds - Raindeer.FlyTime >= 0)
            {
                realFlyTime = Raindeer.FlyTime;
            }
            else
            {
                realFlyTime = seconds;
            }

            Raindeer.TraveledDistance += Raindeer.Speed * realFlyTime;
            seconds = Math.Max(0, seconds - realFlyTime);

            if (seconds == 0)
            {
                return;
            }
            else
            {
                Raindeer.ChangeState(new RestingRaindeerState(Raindeer));
                Raindeer.ActForN(seconds);
            }

        }

        internal override void ActForSingle()
        {
            SecondsInState++;
            Raindeer.TraveledDistance += Raindeer.Speed;
            if (SecondsInState == Raindeer.FlyTime)
            {
                Raindeer.ChangeState(new RestingRaindeerState(Raindeer));
            }
        }
    }
}
