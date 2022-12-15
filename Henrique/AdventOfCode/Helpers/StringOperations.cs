namespace AdventOfCode.Helpers
{
    internal static class StringOperations
    {
        internal static string ReplaceAtIndex(string value, int index, string match, string replacement)
        {
            if (value == null)
            {
                return "";
            }
            if (match == null || replacement == null)
            {
                return value;
            }
            return value.Remove(index, match.Length).Insert(index, replacement);
        }

    }
}
