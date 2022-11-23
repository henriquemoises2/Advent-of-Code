using AdventOfCode.Code._2015_14;
namespace AdventOfCode.Code._2015.Entities._2015_14
{
    internal abstract class RaindeerState
    {
        internal Raindeer Raindeer;
        internal int SecondsInState;
        internal abstract void ActForN(int seconds);
        internal abstract void ActForSingle();

        internal RaindeerState(Raindeer raindeer) 
        {
            Raindeer = raindeer;
        }

    }
}
