using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer
{
    private static readonly Dictionary<SerializedPropertyType, Func<SerializedProperty, string>> typeConverters =
        new Dictionary<SerializedPropertyType, Func<SerializedProperty, string>>
    {
        { SerializedPropertyType.Boolean, prop => prop.boolValue.ToString() },
        { SerializedPropertyType.String, prop => prop.stringValue },
        { SerializedPropertyType.Integer, prop => prop.intValue.ToString() },
        { SerializedPropertyType.Float, prop => prop.floatValue.ToString("0.0000") },
        { SerializedPropertyType.Vector2, prop => prop.vector2Value.ToString() },
        { SerializedPropertyType.Vector3, prop => prop.vector3Value.ToString() },
        { SerializedPropertyType.Color, prop => prop.colorValue.ToString() },
        { SerializedPropertyType.Enum, prop => prop.enumDisplayNames[prop.enumValueIndex] }
    };

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        string valueString = GetValueString(property);
        EditorGUI.LabelField(position, label.text, valueString);
    }

    private string GetValueString(SerializedProperty property)
    {
        if (typeConverters.TryGetValue(property.propertyType, out var converter))
        {
            return converter(property);
        }
        return "Not Supported Type.";
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }
}
#endif