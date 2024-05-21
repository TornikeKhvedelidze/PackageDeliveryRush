#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class HierarchyColorChanger : MonoBehaviour
{
    static HierarchyColorChanger()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (go != null && go.name.StartsWith("@"))
        {
            EditorGUI.DrawRect(selectionRect, Color.white);

            string text = go.name.Substring(1);

            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.normal.textColor = Color.black;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontStyle = FontStyle.Bold;

            Rect labelRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.height);
            EditorGUI.LabelField(labelRect, text, style);
        }
    }
}

#endif