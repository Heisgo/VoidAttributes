using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(DisplayIfAttribute))]
public class DisplayIfDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DisplayIfAttribute displayIf = (DisplayIfAttribute)attribute;
        SerializedProperty conditionProperty = property.serializedObject.FindProperty(displayIf.conditionField);

        if (conditionProperty != null && conditionProperty.boolValue == displayIf.showIfTrue)
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        DisplayIfAttribute displayIf = (DisplayIfAttribute)attribute;
        SerializedProperty conditionProperty = property.serializedObject.FindProperty(displayIf.conditionField);

        if (conditionProperty != null && conditionProperty.boolValue == displayIf.showIfTrue)
        {
            return base.GetPropertyHeight(property, label);
        }
        return 0;
    }
}
#endif
