using UnityEngine;

public class ConditionalFieldAttribute : PropertyAttribute
{
    public string conditionField;
    public object compareValue;

    public ConditionalFieldAttribute(string conditionField, object compareValue)
    {
        this.conditionField = conditionField;
        this.compareValue = compareValue;
    }
}
