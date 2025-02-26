#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(InfoBoxAttribute))]
public class InfoBoxDrawer : DecoratorDrawer
{
    private const float MARGIN = 5f;
    private const float ICON_SIZE = 42f;
    private const float MIN_HEIGHT = 50f;
    private const float MAX_HEIGHT = 200f;

    public override void OnGUI(Rect position)
    {
        InfoBoxAttribute attr = (InfoBoxAttribute)attribute;

        GUIStyle style = new(EditorStyles.label)
        {
            wordWrap = true,
            normal = { textColor = Color.black }
        };

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

        if (!string.IsNullOrEmpty(attr.Icon))
        {
            Texture customIcon = EditorGUIUtility.IconContent(attr.Icon).image;
            if (customIcon != null)
            {
                iconTexture = customIcon;
            }
        }

        float finalHeight = CalcInfoBoxHeight(attr.Message, position.width, style);

        Rect boxRect = new(position.x, position.y, position.width, finalHeight);
        EditorGUI.DrawRect(boxRect, backgroundColor);

        Rect iconRect = new(position.x + 8, position.y + 6, ICON_SIZE, ICON_SIZE);
        if (iconTexture != null)
        {
            GUI.DrawTexture(iconRect, iconTexture, ScaleMode.ScaleToFit);
        }

        float textX = iconTexture != null ? iconRect.xMax + 5f : (position.x + MARGIN);
        float textWidth = position.width - (textX - position.x) - MARGIN;
        Rect textRect = new(textX, position.y + MARGIN, textWidth, finalHeight - (2 * MARGIN));
        EditorGUI.LabelField(textRect, attr.Message, style);
    }

    public override float GetHeight()
    {
        InfoBoxAttribute attr = (InfoBoxAttribute)attribute;

        float estimatedWidth = EditorGUIUtility.currentViewWidth - 40f;

        GUIStyle style = new(EditorStyles.label)
        {
            wordWrap = true
        };

        return CalcInfoBoxHeight(attr.Message, estimatedWidth, style) + 5;
    }

    /// <summary>
    /// Calculates the necessary height to display the text with word wrapping,
    /// considering the icon, margins, and min/max limits.
    /// </summary>
    private float CalcInfoBoxHeight(string message, float boxWidth, GUIStyle style)
    {
        float textWidth = boxWidth - ICON_SIZE - (2 * MARGIN) - 5f - 8f;
        if (textWidth < 0f) textWidth = boxWidth;

        float textHeight = style.CalcHeight(new GUIContent(message), textWidth);

        float rawHeight = Mathf.Max(textHeight + 2 * MARGIN, ICON_SIZE + 2 * MARGIN);

        float finalHeight = Mathf.Clamp(rawHeight, MIN_HEIGHT, MAX_HEIGHT);
        return finalHeight;
    }

    /// <summary>
    /// Converts string to color.
    /// </summary>
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
}
#endif
