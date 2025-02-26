#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(DropdownAttribute))]
public class DropdownDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DropdownAttribute dropdownAttribute = (DropdownAttribute)attribute;
        object targetObject = property.serializedObject.targetObject;

        object valuesObject = GetValuesObject(targetObject, dropdownAttribute.ValuesName);
        if (valuesObject == null)
        {
            EditorGUI.LabelField(position, label.text, $"'{dropdownAttribute.ValuesName}' wasn't found!");
            return;
        }

        IList list = ConvertToIList(valuesObject);
        if (list == null)
        {
            EditorGUI.LabelField(position, label.text,
                $"'{dropdownAttribute.ValuesName}' isn't an IList nor a supported array");
            return;
        }

        List<string> displayOptions = new();
        for (int i = 0; i < list.Count; i++)
        {
            object element = list[i];
            displayOptions.Add(element != null ? element.ToString() : "<null>");
        }

        DrawDropdownForProperty(position, property, label, list, displayOptions);
    }

    /// <summary>
    /// If the object's IList, return it
    /// If it's a one dimensional array, converts it to List<object>.
    /// Otherwise, returns null
    /// </summary>
    private IList ConvertToIList(object valuesObject)
    {
        if (valuesObject is IList asIList)
            return asIList;
        else
        {
            // Verifies if it's 1 dimensional array
            Type objType = valuesObject.GetType();
            if (objType.IsArray && objType.GetArrayRank() == 1)
            {
                Array array = (Array)valuesObject;
                List<object> arrayAsList = new();
                foreach (var item in array)
                {
                    arrayAsList.Add(item);
                }
                return arrayAsList;
            }
        }

        return null;
    }

    private object GetValuesObject(object target, string valuesName)
    {
        if (target == null) return null;

        Type targetType = target.GetType();

        FieldInfo fieldInfo = targetType.GetField(valuesName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (fieldInfo != null)
        {
            return fieldInfo.GetValue(target);
        }

        PropertyInfo propInfo = targetType.GetProperty(valuesName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (propInfo != null && propInfo.CanRead)
        {
            return propInfo.GetValue(target);
        }

        MethodInfo methodInfo = targetType.GetMethod(valuesName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (methodInfo != null
            && methodInfo.ReturnType != typeof(void)
            && methodInfo.GetParameters().Length == 0)
        {
            return methodInfo.Invoke(target, null);
        }

        return null;
    }

    private void DrawDropdownForProperty(
        Rect position,
        SerializedProperty property,
        GUIContent label,
        IList list,
        List<string> displayOptions)
    {
        switch (property.propertyType)
        {
            case SerializedPropertyType.String:
                DrawStringDropdown(position, property, label, list, displayOptions);
                break;

            case SerializedPropertyType.Integer:
                DrawIntDropdown(position, property, label, list, displayOptions);
                break;

            case SerializedPropertyType.ObjectReference:
                DrawObjectDropdown(position, property, label, list, displayOptions);
                break;

            default:
                EditorGUI.LabelField(position, label.text,
                    $"Type '{property.propertyType}' is not supported");
                break;
        }
    }

    private void DrawStringDropdown(
        Rect position,
        SerializedProperty property,
        GUIContent label,
        IList list,
        List<string> displayOptions)
    {
        int currentIndex = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if ((list[i] != null) && list[i].ToString() == property.stringValue)
            {
                currentIndex = i;
                break;
            }
        }

        int newIndex = EditorGUI.Popup(position, label.text, currentIndex, displayOptions.ToArray());
        if (newIndex >= 0 && newIndex < list.Count)
        {
            object selected = list[newIndex];
            property.stringValue = selected != null ? selected.ToString() : "";
        }
    }

    private void DrawIntDropdown(
        Rect position,
        SerializedProperty property,
        GUIContent label,
        IList list,
        List<string> displayOptions)
    {
        int currentValue = property.intValue;
        int currentIndex = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] is int intVal && intVal == currentValue)
            {
                currentIndex = i;
                break;
            }
        }

        int newIndex = EditorGUI.Popup(position, label.text, currentIndex, displayOptions.ToArray());
        if (newIndex >= 0 && newIndex < list.Count)
        {
            if (list[newIndex] is int intVal)
            {
                property.intValue = intVal;
            }
        }
    }

    private void DrawObjectDropdown(
        Rect position,
        SerializedProperty property,
        GUIContent label,
        IList list,
        List<string> displayOptions)
    {
        UnityEngine.Object currentObj = property.objectReferenceValue;
        int currentIndex = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if ((object)list[i] == (object)currentObj)
            {
                currentIndex = i;
                break;
            }
        }

        int newIndex = EditorGUI.Popup(position, label.text, currentIndex, displayOptions.ToArray());
        if (newIndex >= 0 && newIndex < list.Count)
        {
            if (list[newIndex] is UnityEngine.Object objVal)
            {
                property.objectReferenceValue = objVal;
            }
            else
            {
                property.objectReferenceValue = null;
            }
        }
    }
}
#endif
