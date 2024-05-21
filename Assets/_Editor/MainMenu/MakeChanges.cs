#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour))]
public class MakeChanges : Editor
{
    [MenuItem("GameObject/Custom Button/Test", false, -1)]
    static void CustomButton()
    {
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            Debug.Log("No object selected.");
        }

        YourFunction(selectedObject);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static void YourFunction(GameObject selectedObject)
    {
        selectedObject.GetComponent<MainMenuManager>().Initialize_Editor();
    }
}
#endif
