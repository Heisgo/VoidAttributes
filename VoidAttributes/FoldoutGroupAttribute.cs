using UnityEngine;

public class FoldoutGroupAttribute : PropertyAttribute
{
    public string groupName;

    public FoldoutGroupAttribute(string groupName)
    {
        this.groupName = groupName;
    }
}
