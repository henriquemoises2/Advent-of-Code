using AdventOfCode.Code._2015_14;

namespace AdventOfCode.Code._2015.Entities._2015_14
{
    internal class RestingRaindeerState : RaindeerState
    {
        public RestingRaindeerState(Raindeer raindeer) : base(raindeer)
        {
            SecondsInState = 0;
        }

        internal override void ActForN(int seconds)
        {
            int realRestingTime;
            if (seconds - Raindeer.RestTime >= 0)
            {
                realRestingTime = Raindeer.RestTime;
            }
            else
            {
                realRestingTime = seconds;
            }
            seconds = Math.Max(0, seconds - realRestingTime);

            if (seconds == 0)
            {
                return;
            }
            else
            {
                Raindeer.ChangeState(new FlyingRaindeerState(Raindeer));
                Raindeer.ActForN(seconds);
            }
        }
        internal override void ActForSingle()
        {
            SecondsInState++;
            if (SecondsInState == Raindeer.RestTime)
            {
                Raindeer.ChangeState(new FlyingRaindeerState(Raindeer));
            }
        }
    }
}
