using UnityEngine;

public enum InfoType { Info, Warning, Error }

[System.AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class InfoBoxAttribute : PropertyAttribute
{
    public string Message { get; }
    public InfoType InfoType { get; }
    public string Color { get; set; }
    public string Icon { get; set; }

    public InfoBoxAttribute(string message, InfoType infoType = InfoType.Info)
    {
        Message = message;
        InfoType = infoType;
    }
}