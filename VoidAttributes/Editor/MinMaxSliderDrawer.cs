#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
public class MinMaxSliderDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        MinMaxSliderAttribute attr = (MinMaxSliderAttribute)attribute;
        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.Vector2 || property.propertyType == SerializedPropertyType.Vector2Int)
        {
            DrawVectorSlider(position, property, label, attr);
        }
        else if (property.propertyType == SerializedPropertyType.Float || property.propertyType == SerializedPropertyType.Integer)
        {
            DrawSingleValueSlider(position, property, label, attr);
        }
        else if (!string.IsNullOrEmpty(attr.MinFieldName) && !string.IsNullOrEmpty(attr.MaxFieldName))
        {
            DrawSeparateFieldsSlider(position, property, label, attr);
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Not Supported.");
        }

        EditorGUI.EndProperty();
    }

    private void DrawVectorSlider(Rect position, SerializedProperty property, GUIContent label, MinMaxSliderAttribute attr)
    {
        Rect labelRect = new(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
        Rect sliderRect = new(position.x + EditorGUIUtility.labelWidth + 50f, position.y, position.width - EditorGUIUtility.labelWidth - 100f, position.height);
        Rect minFieldRect = new(position.x + EditorGUIUtility.labelWidth, position.y, 50f, position.height);
        Rect maxFieldRect = new(position.x + position.width - 50f, position.y, 50f, position.height);

        EditorGUI.LabelField(labelRect, label);

        EditorGUI.BeginChangeCheck();
        if (property.propertyType == SerializedPropertyType.Vector2)
        {
            Vector2 value = property.vector2Value;
            EditorGUI.MinMaxSlider(sliderRect, ref value.x, ref value.y, attr.MinValue, attr.MaxValue);
            value.x = EditorGUI.FloatField(minFieldRect, value.x);
            value.y = EditorGUI.FloatField(maxFieldRect, value.y);
            value.x = Mathf.Clamp(value.x, attr.MinValue, value.y);
            value.y = Mathf.Clamp(value.y, value.x, attr.MaxValue);

            if (EditorGUI.EndChangeCheck())
            {
                property.vector2Value = value;
            }
        }
        else if (property.propertyType == SerializedPropertyType.Vector2Int)
        {
            Vector2Int value = property.vector2IntValue;
            float min = value.x;
            float max = value.y;
            EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, attr.MinValue, attr.MaxValue);
            value.x = (int)EditorGUI.IntField(minFieldRect, (int)min);
            value.y = (int)EditorGUI.IntField(maxFieldRect, (int)max);
            value.x = Mathf.Clamp(value.x, (int)attr.MinValue, value.y);
            value.y = Mathf.Clamp(value.y, value.x, (int)attr.MaxValue);

            if (EditorGUI.EndChangeCheck())
            {
                property.vector2IntValue = value;
            }
        }
    }

    private void DrawSingleValueSlider(Rect position, SerializedProperty property, GUIContent label, MinMaxSliderAttribute attr)
    {
        EditorGUI.BeginChangeCheck();
        if (property.propertyType == SerializedPropertyType.Float)
        {
            float value = property.floatValue;
            value = EditorGUI.Slider(position, label, value, attr.MinValue, attr.MaxValue);

            if (EditorGUI.EndChangeCheck())
            {
                property.floatValue = value;
            }
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            int value = property.intValue;
            value = EditorGUI.IntSlider(position, label, value, (int)attr.MinValue, (int)attr.MaxValue);

            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = value;
            }
        }
    }

    private void DrawSeparateFieldsSlider(Rect position, SerializedProperty property, GUIContent label, MinMaxSliderAttribute attr)
    {
        SerializedProperty minProperty = property.serializedObject.FindProperty(attr.MinFieldName);
        SerializedProperty maxProperty = property.serializedObject.FindProperty(attr.MaxFieldName);

        if (minProperty == null || maxProperty == null)
        {
            EditorGUI.LabelField(position, label.text, $"'{attr.MinFieldName}' & '{attr.MaxFieldName}' not found");
            return;
        }

        Rect labelRect = new(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
        Rect sliderRect = new(position.x + EditorGUIUtility.labelWidth + 50f, position.y, position.width - EditorGUIUtility.labelWidth - 100f, position.height);
        Rect minFieldRect = new(position.x + EditorGUIUtility.labelWidth, position.y, 50f, position.height);
        Rect maxFieldRect = new(position.x + position.width - 50f, position.y, 50f, position.height);

        EditorGUI.LabelField(labelRect, label);

        EditorGUI.BeginChangeCheck();
        if (minProperty.propertyType == SerializedPropertyType.Float && maxProperty.propertyType == SerializedPropertyType.Float)
        {
            float min = minProperty.floatValue;
            float max = maxProperty.floatValue;

            EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, attr.MinValue, attr.MaxValue);
            min = EditorGUI.FloatField(minFieldRect, min);
            max = EditorGUI.FloatField(maxFieldRect, max);

            min = Mathf.Clamp(min, attr.MinValue, max);
            max = Mathf.Clamp(max, min, attr.MaxValue);

            if (EditorGUI.EndChangeCheck())
            {
                minProperty.floatValue = min;
                maxProperty.floatValue = max;
            }
        }
        else if (minProperty.propertyType == SerializedPropertyType.Integer && maxProperty.propertyType == SerializedPropertyType.Integer)
        {
            int min = minProperty.intValue;
            int max = maxProperty.intValue;

            float minFloat = min;
            float maxFloat = max;
            EditorGUI.MinMaxSlider(sliderRect, ref minFloat, ref maxFloat, attr.MinValue, attr.MaxValue);

            min = EditorGUI.IntField(minFieldRect, (int)minFloat);
            max = EditorGUI.IntField(maxFieldRect, (int)maxFloat);

            min = Mathf.Clamp(min, (int)attr.MinValue, max);
            max = Mathf.Clamp(max, min, (int)attr.MaxValue);

            if (EditorGUI.EndChangeCheck())
            {
                minProperty.intValue = min;
                maxProperty.intValue = max;
            }
        }
    }
}
#endif
