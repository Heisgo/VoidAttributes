#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomPropertyDrawer(typeof(HeaderImprovedAttribute))]
public class HeaderImprovedDrawer : DecoratorDrawer
{
    private GUIStyle headerStyle;

    public override void OnGUI(Rect position)
    {
        var headerAttribute = (HeaderImprovedAttribute)attribute;       

        headerStyle ??= new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = headerAttribute.fontSize,
                wordWrap = true,
                padding = new RectOffset(0, 0, 5, 5),
            };

        Color textColor;
        Color bgColor;

        if (headerAttribute.customColors)
        {
            // If parsing fails, revert to default
            if (!ColorUtility.TryParseHtmlString(headerAttribute.textColorHex, out textColor))
                textColor = Color.white;

            if (!ColorUtility.TryParseHtmlString(headerAttribute.backgroundColorHex, out bgColor))
                bgColor = new Color(0.207f, 0.207f, 0.207f, 1f);
        }
        else
        {
            // Default colors
            textColor = Color.white;
            bgColor = new Color(0.207f, 0.207f, 0.207f, 1f);
        }

        headerStyle.normal.textColor = textColor;
        float height = headerStyle.CalcHeight(new GUIContent(headerAttribute.headerText), position.width);
        Rect headerRect = new(position.x, position.y, position.width, height);

        EditorGUI.DrawRect(headerRect, bgColor);

        EditorGUI.LabelField(headerRect, headerAttribute.headerText, headerStyle);
    }

    public override float GetHeight()   
    {
        var headerAttribute = (HeaderImprovedAttribute)attribute;
        headerStyle ??= new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                wordWrap = true,
                fontStyle = FontStyle.Bold,
                fontSize = headerAttribute.fontSize,
                padding = new RectOffset(0, 2, 2, 2)
            };
        float height = headerStyle.CalcHeight(new GUIContent(headerAttribute.headerText), EditorGUIUtility.currentViewWidth);
        return height + 5f;
    }
}
#endif
