#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoBehaviour))]
public class Initialiser : Editor
{
    [MenuItem("GameObject/Custom Properties/MainMenu/Initialise", false, -1)]
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
        FindAnyObjectByType<MainMenuManager>().Initialize_Editor();
    }
}
#endif
