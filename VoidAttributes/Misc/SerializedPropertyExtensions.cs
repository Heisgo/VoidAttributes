using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class SerializedPropertyExtensions
{
    public static T GetAttribute<T>(this SerializedProperty property) where T : Attribute
    {
        if (property == null)
            return null;

        Type parentType = property.serializedObject.targetObject.GetType();
        FieldInfo field = parentType.GetField(property.name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        return field?.GetCustomAttribute<T>();
    }
}