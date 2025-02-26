#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HorizontalLineAttribute))]
public class HorizontalLineDrawer : DecoratorDrawer
{
    // Converte uma string simples em uma cor básica
    private Color ColorFromString(string colorName)
    {
        return colorName.ToLower() switch
        {
            "red" => Color.red,
            "green" => Color.green,
            "blue" => Color.blue,
            "yellow" => Color.yellow,
            "black" => Color.black,
            "white" => Color.white,
            "gray" or "grey" => Color.gray,
            _ => Color.white,
        };
    }

    public override void OnGUI(Rect position)
    {
        HorizontalLineAttribute attr = (HorizontalLineAttribute)attribute;
        Rect rect = new(position.x, position.y + attr.Margin, position.width, attr.Thickness);
        EditorGUI.DrawRect(rect, ColorFromString(attr.Color));
    }

    public override float GetHeight()
    {
        HorizontalLineAttribute attr = (HorizontalLineAttribute)attribute;
        return attr.Margin * 2 + attr.Thickness;
    }
}
#endif
