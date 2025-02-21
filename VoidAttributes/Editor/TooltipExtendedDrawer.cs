#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TooltipExtendedAttribute))]
public class TooltipExtendedDrawer : PropertyDrawer
{
    private const float MAX_WIDTH = 300f;
    private GUIStyle tooltipStyle;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label.tooltip = "";

        EditorGUI.PropertyField(position, property, label, true);

        Rect labelRect = new(position.x, position.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);

        if (labelRect.Contains(Event.current.mousePosition))
        {
            ShowTooltip(((TooltipExtendedAttribute)attribute).tooltip);
            EditorWindow.focusedWindow?.Repaint();
        }
    }

    private void ShowTooltip(string text)
    {
        tooltipStyle ??= new GUIStyle(GUI.skin.box)
            {
                wordWrap = true,
                alignment = TextAnchor.UpperLeft,
                fontSize = 12,
                padding = new RectOffset(10, 10, 5, 5),
                normal =
                {
                    textColor = Color.white,
                    background = MakeSolidTexture(1, 1, new Color(0, 0, 0, 0.75f))
                }
            };

        GUIContent content = new(text);

        float height = tooltipStyle.CalcHeight(content, MAX_WIDTH);

        Vector2 mousePos = Event.current.mousePosition;

        Rect tooltipRect = new(mousePos.x + 15, mousePos.y - height - 10, MAX_WIDTH, height);

        float inspectorWidth = EditorGUIUtility.currentViewWidth;

        if (tooltipRect.xMax > inspectorWidth)
        {
            float overflow = tooltipRect.xMax - inspectorWidth;
            tooltipRect.x -= overflow;
        }

        if (tooltipRect.y < 0)
        {
            tooltipRect.y = mousePos.y + 20;
        }

        GUI.Box(tooltipRect, content, tooltipStyle);
    }

    private Texture2D MakeSolidTexture(int width, int height, Color color)
    {
        Texture2D texture = new(width, height);
        Color[] pixels = new Color[width * height];

        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = color;

        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }
}
#endif
