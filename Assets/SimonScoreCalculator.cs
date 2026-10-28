public class SimonScoreCalculator
{
    public static long CalculatePoints(int sequenceLength)
    {
        if (sequenceLength <= 0) return 0;
        double baseMultiplier = 1.0;
        double exponent = 1.7;
        double rawPoints = baseMultiplier * System.Math.Pow(sequenceLength, exponent);
        return (long)System.Math.Floor(rawPoints);
    }
}

[System.Serializable]
public class GameMetricPayload
{
    public System.Collections.Generic.List<MetricEntry> metrics = new System.Collections.Generic.List<MetricEntry>();
}

[System.Serializable]
public struct MetricEntry
{
    public string name;
    public long value;

    public MetricEntry(string name, long value)
    {
        this.name = name;
        this.value = value;
    }
}