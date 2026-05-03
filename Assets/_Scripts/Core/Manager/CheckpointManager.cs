using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public string checkpointScene;
    public Vector3 checkpointPosition;

    public void SetCheckpoint(Vector3 pos)
    {
        checkpointScene = SceneManager.GetActiveScene().name;
        checkpointPosition = pos;

        Debug.Log("Saved checkpoint at: " + pos + " | Scene: " + checkpointScene);
    }

    public void Respawn()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == checkpointScene)
        {
            RespawnInSameScene();
        }
        else
        {
            StartCoroutine(RespawnDifferentScene());
        }

        PlayerController.instance.movement.UnlockPlayer();
        UIManager.instance.SetUIActive(true);
    }

    void RespawnInSameScene()
    {
        PlayerController.instance.transform.position = checkpointPosition;

        PlayerController.instance.health.ResetPlayer();

        GameManager.instance.cameraController.SetPlayerCameraFollow();

        Debug.Log("Respawn cùng scene");
    }

    IEnumerator RespawnDifferentScene()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(checkpointScene);

        while (!load.isDone)
        {
            yield return null;
        }

        PlayerController.instance.transform.position = checkpointPosition;

        PlayerController.instance.health.ResetPlayer();

        GameManager.instance.cameraController.SetPlayerCameraFollow();

        Debug.Log("Respawn khác scene");
    }
}