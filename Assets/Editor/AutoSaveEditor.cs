using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class AutoSaveOnChange
{
    static AutoSaveOnChange()
    {
        EditorSceneManager.sceneDirtied += OnSceneDirtied;
    }

    private static void OnSceneDirtied(UnityEngine.SceneManagement.Scene scene)
    {
        EditorSceneManager.SaveScene(scene);
        AssetDatabase.SaveAssets();
        // Debug.Log($"[AutoSave] Scene autosaved: {scene.name}");
    }
}
