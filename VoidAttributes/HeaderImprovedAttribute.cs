using UnityEngine;
public class HeaderImprovedAttribute : PropertyAttribute
{
    public string headerText;

    // Color strings in HEX format ("#FF0000")
    public string textColorHex;
    public string backgroundColorHex;

    public bool customColors;

    public int fontSize;

    /// <summary>
    /// Default constructor (no custom colors) - uses default styling
    /// </summary>
    /// <param name="headerText">Header text to be displayed.</param>
    public HeaderImprovedAttribute(string headerText)
    {
        this.headerText = headerText;
        this.customColors = false;
        this.fontSize = 18;
    }

    /// <summary>
    /// Default constructor (no custom colors) - uses default styling - allows the user to change the fontSize
    /// </summary>
    public HeaderImprovedAttribute(string headerText, int fontSize)
    {
        this.headerText = headerText;
        this.fontSize = fontSize;
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
        this.fontSize = 18;
    }

    /// <summary>
    /// Constructor with custom text and background colors in HEX format ("#FFFFFF").
    /// </summary>
    /// <param name="headerText">Header text to be displayed.</param>
    /// <param name="fontSize">Font Size</param>
    /// <param name="textColorHex">Text color in HEX (e.g., "#FFFFFF").</param>
    /// <param name="backgroundColorHex">Background color in HEX (e.g., "#333333").</param>
    public HeaderImprovedAttribute(string headerText, int fontSize, string textColorHex, string backgroundColorHex)
    {
        this.headerText = headerText;
        this.fontSize = fontSize;
        this.textColorHex = textColorHex;
        this.backgroundColorHex = backgroundColorHex;
        this.customColors = true;
    }
}