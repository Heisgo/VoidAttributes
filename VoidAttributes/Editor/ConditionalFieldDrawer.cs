using UnityEditor;
using UnityEngine;
using System;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
public class ConditionalFieldDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute attr = (ConditionalFieldAttribute)attribute;
        SerializedProperty conditionProp = property.serializedObject.FindProperty(attr.conditionField);

        if (conditionProp != null && ShouldShow(conditionProp, attr.compareValue))
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute attr = (ConditionalFieldAttribute)attribute;
        SerializedProperty conditionProp = property.serializedObject.FindProperty(attr.conditionField);

        if (conditionProp != null && ShouldShow(conditionProp, attr.compareValue))
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        return 0;
    }

    private bool ShouldShow(SerializedProperty conditionProp, object compareValue)
    {
        return conditionProp.propertyType switch
        {
            SerializedPropertyType.Integer => conditionProp.intValue == Convert.ToInt32(compareValue),
            SerializedPropertyType.Float => Mathf.Approximately(conditionProp.floatValue, Convert.ToSingle(compareValue)),
            _ => false,
        };
    }
}
#endif
