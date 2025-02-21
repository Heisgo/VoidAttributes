using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(FoldoutGroupAttribute))]
public class FoldoutGroupDrawer : PropertyDrawer
{
    private static Dictionary<string, bool> foldouts = new();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        FoldoutGroupAttribute foldoutGroup = (FoldoutGroupAttribute)attribute;
        string groupName = foldoutGroup.groupName;

        if (!foldouts.ContainsKey(groupName))
            foldouts[groupName] = true;

        Rect foldoutRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        foldouts[groupName] = EditorGUI.Foldout(foldoutRect, foldouts[groupName], groupName, true);

        if (foldouts[groupName])
        {
            Rect propertyRect = new(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUI.GetPropertyHeight(property));
            EditorGUI.PropertyField(propertyRect, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        FoldoutGroupAttribute foldoutGroup = (FoldoutGroupAttribute)attribute;
        bool isExpanded = foldouts.ContainsKey(foldoutGroup.groupName) ? foldouts[foldoutGroup.groupName] : true;
        float height = EditorGUIUtility.singleLineHeight;
        if (isExpanded)
        {
            height += EditorGUI.GetPropertyHeight(property);
        }
        return height;
    }
}
#endif
