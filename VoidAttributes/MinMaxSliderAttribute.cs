using UnityEngine;

public class MinMaxSliderAttribute : PropertyAttribute
{
    public float MinValue { get; }
    public float MaxValue { get; }

    public string MinFieldName { get; }
    public string MaxFieldName { get; }

    // Para Vector2 e Vector2Int
    public MinMaxSliderAttribute(float minValue, float maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    // Para float ou int
    public MinMaxSliderAttribute(string minFieldName, string maxFieldName, float minValue, float maxValue)
    {
        MinFieldName = minFieldName;
        MaxFieldName = maxFieldName;
        MinValue = minValue;
        MaxValue = maxValue;
    }
}
