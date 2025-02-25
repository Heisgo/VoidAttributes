# VoidAttributes

This project demonstrates the use of custom attributes to enhance the Unity Inspector experience. With a variety of attributes, you can control property visibility, enforce requirements, group related fields, and even execute methods directly from the Inspector—all without modifying your underlying logic.

## Features

**[ShowOnly]:**
Displays a property as read-only in the Inspector.

**[DisplayIf]:**
Conditionally shows a property based on the value of another variable.

**[Required]:**
Ensures that reference fields (like GameObjects) are assigned, preventing runtime errors.

**[LabelOverride]:**
Overrides the default label of a property to a custom name, making your Inspector more intuitive.

**[FoldoutGroup]:**
Organizes related properties into a collapsible section, keeping the Inspector tidy.

**[HorizontalLine]:**
Inserts a customizable horizontal line to visually separate sections in the Inspector.

**[InfoBox]:**
Displays an informational message directly in the Inspector to guide or warn users.

**[TooltipExtended]:**
Provides advanced tooltips with multi-line support for clearer, more detailed explanations. It also has a more beaultiful look.

**[ConditionalField]:**
Shows or hides a property based on the value of another property (e.g., displays a string only when an integer equals 1).

**[MinMaxSlider]:**
Creates a slider in the Inspector that lets you choose a value within a specified range.

**[TagSelector]:**
Displays a dropdown for selecting tags, simplifying the process of tagging GameObjects.

**[Button]:**
Adds a clickable button in the Inspector to execute methods directly, useful for testing or debugging.

## Code Example Structure

### The Player Class
This example class demonstrates how to apply the custom attributes to enhance the Inspector:

```csharp
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [ShowOnly] public int value = 5;

    public bool valueOfTest;
    [DisplayIf("valueOfTest")] public int value1 = 1;

    [Required]
    public GameObject PlayerPrefab;

    [LabelOverride("Jairo")]
    public int maxScore;

    [FoldoutGroup("Stats")]
    public int health;
    [FoldoutGroup("Stats")]
    public int mana;
    [FoldoutGroup("Stats")]
    public int damage;
    [FoldoutGroup("Stats")]
    public int damage2;

    [HorizontalLine(Color = "white", Thickness = 1, Margin = 7)]
    [InfoBox("Try not to change this var value.", InfoType.Info)]
    public float randomVar;

    [TooltipExtended("This is an Advanced Tooltip!\nSupports multiple lines\nBetter look!")]
    public int mode;

    [ConditionalField("mode", 1)]
    public string textForMode1;

    [MinMaxSlider(0, 70)]
    public int dropChance;

    [TagSelector]
    public string objectTag;

    [Button]
    public void Test()
    {
        Debug.Log("Function Called!");
    }
}

```
# Contributions
Contributions are welcome! If you have suggestions or improvements, feel free to open issues or submit pull requests.





There's a secret attribute exclusive to the unitypackage... ImprovedHeader... 
