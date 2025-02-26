using UnityEngine;

public class LabelOverrideAttribute : PropertyAttribute
{
    public string customLabel;

    public LabelOverrideAttribute(string customLabel)
    {
        this.customLabel = customLabel;
    }
}
