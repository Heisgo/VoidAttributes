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

        Rect currentPosition = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        // Se for a primeira propriedade do grupo, desenha o foldout (mas sem mudar o nome do campo)
        if (IsFirstPropertyOfGroup(property))
        {
            foldouts[groupName] = EditorGUI.Foldout(currentPosition, foldouts[groupName], groupName, true);
            currentPosition.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        // Se o grupo está expandido, desenha a propriedade com o nome correto
        if (foldouts[groupName])
        {
            EditorGUI.PropertyField(currentPosition, property, new GUIContent(property.displayName), true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        FoldoutGroupAttribute foldoutGroup = (FoldoutGroupAttribute)attribute;
        string groupName = foldoutGroup.groupName;

        float height = 0;

        // Se for a primeira propriedade do grupo, adiciona altura do foldout
        if (IsFirstPropertyOfGroup(property))
            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        // Se o foldout está expandido, adiciona a altura do campo normalmente
        if (foldouts.ContainsKey(groupName) && foldouts[groupName])
        {
            height += EditorGUI.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing;
        }

        return height;
    }

    private bool IsFirstPropertyOfGroup(SerializedProperty property)
    {
        SerializedProperty iterator = property.serializedObject.GetIterator();
        iterator.NextVisible(true);

        while (iterator.NextVisible(false))
        {
            if (iterator.propertyPath == property.propertyPath)
                return true;

            if (iterator.GetAttribute<FoldoutGroupAttribute>()?.groupName == ((FoldoutGroupAttribute)attribute).groupName)
                return false;
        }

        return true;
    }
}
#endif
