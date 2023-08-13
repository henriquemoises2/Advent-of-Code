namespace AdventOfCode._2015_16
{
    internal class MFCSAM
    {
        private const int Children = 3;
        private const int Cats = 7;
        private const int Samoyeds = 2;
        private const int Pomeranians = 3;
        private const int Akitas = 0;
        private const int Vizslas = 0;
        private const int Goldfish = 5;
        private const int Trees = 3;
        private const int Cars = 2;
        private const int Perfumes = 1;

        internal IEnumerable<Compound> Result
        {
            get;
        }
        private IEnumerable<Compound> GetResult()
        {
            return new List<Compound>()
            {
                new Compound("children", Children),
                new Compound("cats", Cats),
                new Compound("samoyeds", Samoyeds),
                new Compound("pomeranians", Pomeranians),
                new Compound("akitas", Akitas),
                new Compound("vizslas", Vizslas),
                new Compound("goldfish", Goldfish),
                new Compound("trees", Trees),
                new Compound("cars", Cars),
                new Compound("perfumes", Perfumes),
            };
        }

        internal MFCSAM()
        {
            Result = GetResult();
        }

        internal bool ValidateSample(AuntSue auntSueSample)
        {
            foreach (Compound compound in auntSueSample.Compounds)
            {
                if (!IsEqual(compound.Name.ToLower(), compound.Quantity))
                {
                    return false;
                }
            }
            return true;
        }

        internal bool ValidateSampleWithOutdatedRetroencabulator(AuntSue auntSueSample)
        {
            foreach (Compound compound in auntSueSample.Compounds)
            {
                switch (compound.Name.ToLower())
                {
                    case "cats":
                        if (!IsGreaterThan("cats", compound.Quantity))
                        {
                            return false;
                        }
                        break;
                    case "trees":
                        if (!IsGreaterThan("trees", compound.Quantity))
                        {
                            return false;
                        }
                        break;
                    case "pomeranians":
                        if (!IsLesserThan("pomeranians", compound.Quantity))
                        {
                            return false;
                        }
                        break;
                    case "goldfish":
                        if (!IsLesserThan("goldfish", compound.Quantity))
                        {
                            return false;
                        }
                        break;
                    default:
                        if (!IsEqual(compound.Name.ToLower(), compound.Quantity))
                        {
                            return false;
                        }
                        break;
                }

            }
            return true;
        }

        private bool IsEqual(string compoundName, int quantity)
        {
            return quantity == Result.SingleOrDefault(res => res.Name == compoundName)?.Quantity;
        }

        private bool IsGreaterThan(string compoundName, int quantity)
        {
            return quantity > Result.SingleOrDefault(res => res.Name == compoundName)?.Quantity;
        }

        private bool IsLesserThan(string compoundName, int quantity)
        {
            return quantity < Result.SingleOrDefault(res => res.Name == compoundName)?.Quantity;
        }

    }
}
