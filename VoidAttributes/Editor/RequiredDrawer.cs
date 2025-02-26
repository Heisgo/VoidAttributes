using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        bool isReference = property.propertyType == SerializedPropertyType.ObjectReference;
        bool isValid = isReference && property.objectReferenceValue != null;

        float baseHeight = EditorGUI.GetPropertyHeight(property, label, true);

        Rect propertyRect = new(position.x, position.y, position.width, baseHeight);

        if (!isReference)
        {
            EditorGUI.HelpBox(propertyRect,
                "[Required] only works for gameObjects or ScriptableObjects",
                MessageType.Warning
            );
        }
        else
        {
            if (!isValid)
            {
                Color oldColor = GUI.color;
                GUI.color = Color.red;
                EditorGUI.PropertyField(propertyRect, property, label, true);
                GUI.color = oldColor;

                Rect helpBoxRect = new(position.x, position.y + baseHeight + 2, position.width, 40);
                EditorGUI.HelpBox(helpBoxRect, "This component is required", MessageType.Error);
            }
            else
            {
                EditorGUI.PropertyField(propertyRect, property, label, true);
            }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        bool isReference = property.propertyType == SerializedPropertyType.ObjectReference;
        bool isValid = isReference && property.objectReferenceValue != null;

        float baseHeight = EditorGUI.GetPropertyHeight(property, label, true);

        if (!isReference)
        {
            return baseHeight;
        }

        if (!isValid)
        {
            return baseHeight + 42f; 
        }

        return baseHeight;
    }
}
#endif
