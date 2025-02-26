using System;
using UnityEngine;

/// <summary>
/// Indicates that the field should be drawn as a dropdown
/// The ValuesName's the name of a field/property/method in the same class
/// that returns a list of values for the dropdown
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class DropdownAttribute : PropertyAttribute
{
    public string ValuesName { get; private set; }

    public DropdownAttribute(string valuesName)
    {
        ValuesName = valuesName;
    }
}
