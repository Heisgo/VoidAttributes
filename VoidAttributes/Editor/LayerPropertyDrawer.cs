#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom PropertyDrawer that handles the [Layer] attribute.
/// </summary>
[CustomPropertyDrawer(typeof(LayerAttribute))]
public class LayerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.String)
        {
            // If it's a string, treat it as a layer name
            string[] layers = UnityEditorInternal.InternalEditorUtility.layers;
            int currentIndex = Mathf.Max(0, System.Array.IndexOf(layers, property.stringValue));
            int newIndex = EditorGUI.Popup(position, label.text, currentIndex, layers);

            if (newIndex != currentIndex)
            {
                property.stringValue = layers[newIndex];
            }
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            // If it's an int, treat it as a layer index
            string[] layers = UnityEditorInternal.InternalEditorUtility.layers;

            // Convert the integer (layer index) to the layer name
            string currentLayerName = LayerMask.LayerToName(property.intValue);
            int currentIndex = Mathf.Max(0, System.Array.IndexOf(layers, currentLayerName));

            int newIndex = EditorGUI.Popup(position, label.text, currentIndex, layers);

            if (newIndex != currentIndex)
                property.intValue = LayerMask.NameToLayer(layers[newIndex]);
        }
        else
        {
            // If it's neither a string nor an int, show a warning
            EditorGUI.LabelField(position, label.text, "Use [Layer] only on int or string fields.");
        }

        EditorGUI.EndProperty();
    }
}
#endif
