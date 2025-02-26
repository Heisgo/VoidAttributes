using UnityEngine;

public class BoxGroupAttribute : PropertyAttribute
{
    public string groupName;

    public BoxGroupAttribute(string groupName)
    {
        this.groupName = groupName;
    }
}
