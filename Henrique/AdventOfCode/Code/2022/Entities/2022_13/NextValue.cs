namespace AdventOfCode.Code._2022.Entities._2022_13;

internal class NextValue(NextValueType valueType, string value, string remainingValue)
{
    internal NextValueType ValueType { get; } = valueType;
    internal string Value { get; } = value;
    internal string RemainingValue { get; } = remainingValue;
}