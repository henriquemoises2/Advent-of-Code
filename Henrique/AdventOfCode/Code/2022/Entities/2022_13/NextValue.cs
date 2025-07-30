namespace AdventOfCode.Code._2022.Entities._2022_13;

internal class NextValue(ValueType valueType, string value)
{
    internal ValueType ValueType { get; } = valueType;
    internal string Value { get; } = value;
}