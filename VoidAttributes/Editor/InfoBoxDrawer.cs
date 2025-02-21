#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InfoBoxAttribute))]
public class InfoBoxDrawer : DecoratorDrawer
{
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
            _ => Color.black,
        };
    }

    public override void OnGUI(Rect position)
    {
        InfoBoxAttribute attr = (InfoBoxAttribute)attribute;

        Color backgroundColor;
        Texture iconTexture = null;

        switch (attr.InfoType)
        {
            case InfoType.Warning:
                backgroundColor = new Color(1f, 0.9f, 0.6f);
                iconTexture = EditorGUIUtility.IconContent("console.warnicon").image;
                break;
            case InfoType.Error:
                backgroundColor = new Color(1f, 0.7f, 0.7f);
                iconTexture = EditorGUIUtility.IconContent("console.erroricon").image;
                break;
            default:
                backgroundColor = new Color(0.55f, 0.88f, 1f);
                iconTexture = EditorGUIUtility.IconContent("console.infoicon").image;
                break;
        }

        if (!string.IsNullOrEmpty(attr.Color))
        {
            backgroundColor = ColorFromString(attr.Color);
        }

        EditorGUI.DrawRect(position, backgroundColor);

        float iconSize = position.height - 4;
        Rect iconRect = new(position.x + 5, position.y + 2, iconSize, iconSize);
        if (!string.IsNullOrEmpty(attr.Icon))
        {
            Texture customIcon = EditorGUIUtility.IconContent(attr.Icon).image;
            if (customIcon != null)
                iconTexture = customIcon;
        }
        if (iconTexture != null)
        {
            GUI.DrawTexture(iconRect, iconTexture, ScaleMode.ScaleToFit);
        }

        Rect textRect = new(iconRect.xMax + 5, position.y, position.width - iconRect.width - 10, position.height);
        GUIStyle style = new(EditorStyles.label);
        style.normal.textColor = Color.black;
        EditorGUI.LabelField(textRect, attr.Message, style);
    }

    public override float GetHeight()
    {
        return 40f;
    }
}
#endif
