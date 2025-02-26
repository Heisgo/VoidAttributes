using UnityEngine;
public class HeaderImprovedAttribute : PropertyAttribute
{
    public string headerText;

    // Color strings in HEX format ("#FF0000")
    public string textColorHex;
    public string backgroundColorHex;

    public bool customColors;

    /// <summary>
    /// Default constructor (no custom colors) - uses default styling
    /// </summary>
    /// <param name="headerText">Header text to be displayed.</param>
    public HeaderImprovedAttribute(string headerText)
    {
        this.headerText = headerText;
        this.customColors = false;
    }

    /// <summary>
    /// Constructor with custom text and background colors in HEX format ("#FFFFFF").
    /// </summary>
    /// <param name="headerText">Header text to be displayed.</param>
    /// <param name="textColorHex">Text color in HEX (e.g., "#FFFFFF").</param>
    /// <param name="backgroundColorHex">Background color in HEX (e.g., "#333333").</param>
    public HeaderImprovedAttribute(string headerText, string textColorHex, string backgroundColorHex)
    {
        this.headerText = headerText;
        this.textColorHex = textColorHex;
        this.backgroundColorHex = backgroundColorHex;
        this.customColors = true;
    }
}