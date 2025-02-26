using System;
using UnityEngine;

/// <summary>
/// Attribute to select a layer from the inspector.
/// Can be applied to an int or a string field.
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class LayerAttribute : PropertyAttribute
{
}
