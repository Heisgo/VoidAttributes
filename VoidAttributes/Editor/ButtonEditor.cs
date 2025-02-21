using UnityEngine;
using UnityEditor;
using System.Reflection;

#if UNITY_EDITOR
[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var type = target.GetType();
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var method in methods)
        {
            if (method.GetCustomAttribute<ButtonAttribute>() != null)
            {
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(target, null);
                }
            }
        }
    }
}
#endif
