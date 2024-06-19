namespace AdventOfCode.Helpers
{
    internal static class StringOperations
    {
        internal static string ReplaceAtIndex(string value, int index, string match, string replacement)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            if (string.IsNullOrWhiteSpace(match) || string.IsNullOrWhiteSpace(replacement))
            {
                return value;
            }
            return value.Remove(index, match.Length).Insert(index, replacement);
        }

    }
}
