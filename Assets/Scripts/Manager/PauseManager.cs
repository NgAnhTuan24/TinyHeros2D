using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void RepeatGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
        Destroy(GameManager.instance.gameObject);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        SceneManager.LoadSceneAsync(0);
        Destroy(GameManager.instance.gameObject);
        Time.timeScale = 1f;
    }
}
