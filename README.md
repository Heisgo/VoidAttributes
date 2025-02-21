# VoidAttributes

This project demonstrates the use of custom attributes to enhance the Unity Inspector experience. By using attributes such as **[ShowOnly]**, **[DisplayIf]**, **[Required]**, **[LabelOverride]**, **[FoldoutGroup]**, **[ConditionalField]**, and **[Button]**, this example illustrates how to control property visibility, editing, and grouping, as well as how to facilitate direct testing from the Inspector.

## Features

- **[ShowOnly]**: Displays the `value` property as read-only.
- **[DisplayIf]**: Shows the `value` property only when a boolean is true.
- **[Required]**: Ensures that a GameObject field is assigned.
- **[LabelOverride]**: Changes the displayed label of a variable to a name of your choice.
- **[FoldoutGroup]**: Groups the properties of the `Stats` class into a collapsible panel.
- **[ConditionalField]**: Displays a variable property only when another variable equals a specified value.  
  (Example: `[ConditionalField("value", 1)]` will show the field when `value` equals 1)
- **[Button]**: Allows you to execute a method directly from the Inspector.

## Code Example Structure

### The `Stats` Class

A simple class to store basic player attributes such as health and mana.

```csharp
using UnityEngine;

[System.Serializable]
public class Stats
{
    public int health;
    public int mana;
}
```

The Player Class
This class, inheriting from SingletonMonoBehaviour<Player>, uses various custom attributes to demonstrate advanced Inspector functionalities in Unity.

Read-only Display: The value property uses [ShowOnly].
Conditional Display: The value1 property is shown only if valueOfTest is true ([DisplayIf]).
Mandatory Assignment: The PlayerPrefab is enforced using [Required].
Custom Label: The maxScore field is labeled "Jairo" via [LabelOverride].
Grouped Attributes: The stats field is organized under a foldout named "Stats" ([FoldoutGroup]).
Field Condition: The textForMode1 field is conditionally shown when mode equals 1 ([ConditionalField]).
Inspector Button: The Test method is callable directly from the Inspector with [Button].

```csharp
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [ShowOnly]
    public int value = 5;

    public bool valueOfTest;
    [DisplayIf("valueOfTest")]
    public int value1 = 1;

    [Required]
    public GameObject PlayerPrefab;

    [LabelOverride("Jairo")]
    public int maxScore;

    [FoldoutGroup("Stats")]
    public Stats stats;

    public int mode;

    [ConditionalField("mode", 1)]
    public string textForMode1;

    [Button]
    public void Test()
    {
        Debug.Log("Function Called!");
    }
}
```
# Contributions
Contributions are welcome! If you have suggestions or improvements, feel free to open issues or submit pull requests.
