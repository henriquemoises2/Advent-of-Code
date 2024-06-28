namespace AdventOfCode._2022.Entities
{
    internal class FileSystemItem
    {
        internal long Size { get; set; }
        internal string Name { get; set; }

        internal FileSystemItem(string name, long size = 0)
        {
            Name = name;
            Size = size;
        }
    }
}
