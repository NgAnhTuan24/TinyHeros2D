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
        Time.timeScale = 1f;
        Destroy(GameManager.instance.gameObject);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene(0);
    }
}
