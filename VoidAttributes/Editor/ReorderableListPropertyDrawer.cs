using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using System.Collections;
using System.Reflection;
using System;
using Object = UnityEngine.Object;

public class ReorderableListPropertyDrawer : BaseCustomPropertyDrawer
{
    public static readonly ReorderableListPropertyDrawer Instance = new();

    private Dictionary<string, ReorderableList> listCache = new();
    private GUIStyle headerStyle;

    private GUIStyle GetHeaderStyle()
    {
        headerStyle ??= new GUIStyle(EditorStyles.boldLabel)
            {
                richText = true
            };
        return headerStyle;
    }

    private string GetUniqueKey(SerializedProperty property) => property.serializedObject.targetObject.GetInstanceID() + "." + property.name;

    protected override float GetPropertyHeight_Internal(SerializedProperty property)
    {
        if (property.isArray)
        {
            string key = GetUniqueKey(property);
            if (!listCache.TryGetValue(key, out ReorderableList list))
            {
                return 0;
            }
            return list.GetHeight();
        }
        return EditorGUI.GetPropertyHeight(property, true);
    }

    protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
    {
        if (property.isArray)
        {
            string key = GetUniqueKey(property);

            if (!listCache.ContainsKey(key))
            {
                ReorderableList newList = new(property.serializedObject, property, true, true, true, true);

                newList.drawHeaderCallback = (Rect headerRect) =>
                {
                    EditorGUI.LabelField(headerRect, string.Format("{0}: {1}", label.text, property.arraySize), GetHeaderStyle());
                    ProcessDragDrop(headerRect, newList);
                };

                newList.drawElementCallback = (Rect elementRect, int index, bool isActive, bool isFocused) =>
                {
                    SerializedProperty element = property.GetArrayElementAtIndex(index);
                    elementRect.y += 1.0f;
                    elementRect.x += 10.0f;
                    elementRect.width -= 10.0f;
                    EditorGUI.PropertyField(new Rect(elementRect.x, elementRect.y, elementRect.width, EditorGUIUtility.singleLineHeight), element, true);
                };

                newList.elementHeightCallback = (int index) =>
                {
                    return EditorGUI.GetPropertyHeight(property.GetArrayElementAtIndex(index)) + 4.0f;
                };

                listCache[key] = newList;
            }

            ReorderableList cachedList = listCache[key];

            if (rect == default)
                cachedList.DoLayoutList();
            else
                cachedList.DoList(rect);
        }
        else
        {
            string errorMsg = "ReorderableListAttribute can only be applied to arrays or lists";
            VoidEditorGUI.ShowHelpBox(errorMsg, MessageType.Warning, property.serializedObject.targetObject);
            EditorGUILayout.PropertyField(property, true);
        }
    }

    public void ClearCache() => listCache.Clear();

    private Object GetCompatibleObject(Object obj, ReorderableList list)
    {
        System.Type listType = SerializedPropertyHelper.GetPropertyType(list.serializedProperty);
        System.Type elementType = ReflectionHelper.GetListElementType(listType);

        if (elementType == null)
            return null;

        System.Type objType = obj.GetType();

        if (elementType.IsAssignableFrom(objType))
            return obj;

        if (objType == typeof(GameObject))
        {
            if (typeof(Transform).IsAssignableFrom(elementType))
            {
                Transform trans = ((GameObject)obj).transform;
                if (elementType == typeof(RectTransform))
                    return trans as RectTransform;
                return trans;
            }
            else if (typeof(MonoBehaviour).IsAssignableFrom(elementType))
            {
                return ((GameObject)obj).GetComponent(elementType);
            }
        }
        return null;
    }

    private void ProcessDragDrop(Rect dropArea, ReorderableList list)
    {
        Event evt = Event.current;
        bool eventUsed = false;

        switch (evt.type)
        {
            case EventType.DragExited:
                if (GUI.enabled)
                    HandleUtility.Repaint();
                break;

            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (dropArea.Contains(evt.mousePosition) && GUI.enabled)
                {
                    bool acceptedDrag = false;
                    Object[] objects = DragAndDrop.objectReferences;
                    foreach (Object obj in objects)
                    {
                        Object compatible = GetCompatibleObject(obj, list);
                        if (compatible != null)
                        {
                            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                            if (evt.type == EventType.DragPerform)
                            {
                                list.serializedProperty.arraySize++;
                                int newIndex = list.serializedProperty.arraySize - 1;
                                list.serializedProperty.GetArrayElementAtIndex(newIndex).objectReferenceValue = compatible;
                                acceptedDrag = true;
                            }
                        }
                    }
                    if (acceptedDrag)
                    {
                        GUI.changed = true;
                        DragAndDrop.AcceptDrag();
                        eventUsed = true;
                    }
                }
                break;
        }

        if (eventUsed)
            evt.Use();
    }
}
    public static class ReflectionHelper
    {
        public static Type GetListElementType(Type listType)
        {
            if (listType.IsArray)
                return listType.GetElementType();
            else if (listType.IsGenericType && listType.GetGenericTypeDefinition() == typeof(List<>))
                return listType.GetGenericArguments()[0];
            return null;
        }
    }

    public static class SerializedPropertyHelper
    {
        public static Type GetPropertyType(SerializedProperty property)
        {
            object target = GetTargetObject(property);
            return target?.GetType();
        }

        private static object GetTargetObject(SerializedProperty property)
        {
            if (property == null)
                return null;

            string path = property.propertyPath.Replace(".Array.data[", "[");
            object obj = property.serializedObject.targetObject;
            string[] elements = path.Split('.');

            foreach (string element in elements)
            {
                if (element.Contains("["))
                {
                    string elementName = element.Substring(0, element.IndexOf("["));
                    int index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue(obj, elementName, index);
                }
                else
                    obj = GetValue(obj, element);
            }
            return obj;
        }

        private static object GetValue(object source, string name)
        {
            if (source == null)
                return null;

            Type type = source.GetType();
            FieldInfo field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
                return field.GetValue(source);

            PropertyInfo prop = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            return prop?.GetValue(source, null);
        }

        private static object GetValue(object source, string name, int index)
        {
            if (GetValue(source, name) is not IEnumerable enumerable)
                return null;

            IEnumerator enumerator = enumerable.GetEnumerator();
            for (int i = 0; i <= index; i++)
            {
                if (!enumerator.MoveNext())
                    return null;
            }
            return enumerator.Current;
        }
    }

    public class BaseCustomPropertyDrawer : PropertyDrawer
    {
        protected virtual float GetPropertyHeight_Internal(SerializedProperty property) => EditorGUI.GetPropertyHeight(property, true);

        protected virtual void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label) => EditorGUI.PropertyField(rect, property, label, true);
    }

    public static class VoidEditorGUI
    {
        public static void ShowHelpBox(string message, MessageType messageType, UnityEngine.Object context) => EditorGUILayout.HelpBox(message, messageType);
    }
