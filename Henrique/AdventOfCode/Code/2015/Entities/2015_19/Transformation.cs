namespace AdventOfCode.Code._2015.Entities._2015_19
{
    internal class Transformation
    {
        internal string InitialMolecule { get; set; }
        internal string FinalMolecule { get; set; }

        internal Transformation(string initialMolecule, string finalMolecule)
        {
            InitialMolecule = initialMolecule;
            FinalMolecule = finalMolecule;
        }

    }
}
