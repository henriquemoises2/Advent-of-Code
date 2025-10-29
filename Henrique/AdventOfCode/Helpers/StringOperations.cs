namespace AdventOfCode.Helpers
{
    internal static class StringOperations
    {
        internal static string ReplaceAtIndex(this string value, int index, string match, string replacement)
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

        internal static string RemoveAtIndex(this string value, int index, string match)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            if (string.IsNullOrWhiteSpace(match))
            {
                return value;
            }
            return value.Remove(index, match.Length);
        }

    }
}
