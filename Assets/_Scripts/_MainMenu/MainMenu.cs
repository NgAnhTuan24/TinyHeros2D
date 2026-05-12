using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "SaveLoad";

    [SerializeField] private AudioClip audioGame;

    private void Start()
    {
        AudioManager.instance.PlayMusic(audioGame);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}