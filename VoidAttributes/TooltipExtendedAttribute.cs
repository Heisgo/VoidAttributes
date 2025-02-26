using UnityEngine;

public class TooltipExtendedAttribute : PropertyAttribute
{
    public string tooltip;

    public TooltipExtendedAttribute(string tooltip)
    {
        this.tooltip = tooltip;
    }
}