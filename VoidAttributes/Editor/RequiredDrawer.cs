using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.objectReferenceValue == null)
        {
            GUI.color = Color.red;
            EditorGUI.PropertyField(position, property, label);
            GUI.color = Color.white;
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif
