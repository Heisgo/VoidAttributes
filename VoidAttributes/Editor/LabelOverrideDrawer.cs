using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(LabelOverrideAttribute))]
public class LabelOverrideDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        LabelOverrideAttribute labelOverride = (LabelOverrideAttribute)attribute;
        EditorGUI.PropertyField(position, property, new GUIContent(labelOverride.customLabel), true);
    }
}
#endif
