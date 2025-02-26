using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class HorizontalLineAttribute : PropertyAttribute
{
    public string Color;
    public float Thickness;
    public float Margin;

    public HorizontalLineAttribute()
    {
        Color = "gray";
        Thickness = 2f;
        Margin = 5f;
    }
}

