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

    public void ExitGame()
    {
        Time.timeScale = 1f;

        if (GameManager.instance != null) Destroy(GameManager.instance.gameObject);

        if (PlayerController.instance != null) Destroy(PlayerController.instance.gameObject);

        if (UIManager.instance != null) Destroy(UIManager.instance.gameObject);

        SceneManager.LoadScene(0);
    }
}
