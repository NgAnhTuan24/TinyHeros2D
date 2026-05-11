using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneMenuGame : EditorWindow
{
    [MenuItem("Scenes/Open Scene Selector")]
    public static void ShowWindow()
    {
        GetWindow<SceneMenuGame>("Scene Selector");
    }

    private void OnGUI()
    {
        GUILayout.Label("=== MAIN ===", EditorStyles.boldLabel);
        DrawScene("MainMenu");
        DrawScene("SaveLoad");
        DrawScene("SelectCharacter");

        GUILayout.Space(10);
        GUILayout.Label("=== MAP 1 ===", EditorStyles.boldLabel);
        DrawScenesByPrefix("Map1_");

        GUILayout.Space(10);
        GUILayout.Label("=== MAP 2 ===", EditorStyles.boldLabel);
        DrawScenesByPrefix("Map2_");

        GUILayout.Space(10);
        GUILayout.Label("=== MAP 3 ===", EditorStyles.boldLabel);
        DrawScenesByPrefix("Map3_");

        GUILayout.Space(10);
        GUILayout.Label("=== MAP 4 ===", EditorStyles.boldLabel);
        DrawScenesByPrefix("Map4");

        GUILayout.Space(10);
        GUILayout.Label("=== SYSTEM ===", EditorStyles.boldLabel);
        DrawScene("SafeZone");
        DrawScene("Home");
    }

    void DrawScene(string sceneName)
    {
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (!scene.path.Contains(sceneName)) continue;

            if (GUILayout.Button(sceneName))
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(scene.path);
                }
            }
        }
    }

    void DrawScenesByPrefix(string prefix)
    {
        foreach (var scene in EditorBuildSettings.scenes)
        {
            string name = System.IO.Path.GetFileNameWithoutExtension(scene.path);

            if (!name.StartsWith(prefix)) continue;

            if (GUILayout.Button(name))
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(scene.path);
                }
            }
        }
    }
}
