namespace AdventOfCode._2022_07
{
    internal class FileSystemItem
    {
        internal long Size { get; set; }
        internal string Name { get; set; }
        internal bool IsFolder { get; set; }

        internal FileSystemItem(string name, bool isFolder, long size = 0)
        {
            Name = name;
            Size = size;
            IsFolder = isFolder;
        }
    }
}
