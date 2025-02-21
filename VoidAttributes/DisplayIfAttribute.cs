using UnityEngine;

public class DisplayIfAttribute : PropertyAttribute
{
    public string conditionField;
    public bool showIfTrue;

    public DisplayIfAttribute(string conditionField, bool showIfTrue = true)
    {
        this.conditionField = conditionField;
        this.showIfTrue = showIfTrue;
    }
}
