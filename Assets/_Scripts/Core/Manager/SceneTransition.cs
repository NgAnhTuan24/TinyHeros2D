using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator transitionAnim;

    public string SceneTransitionName { get; private set; }

    public void SetTransitionName(string sceneName, string transitionName)
    {
        StartCoroutine(TransitionRoutine(sceneName, transitionName));
    }

    private IEnumerator TransitionRoutine(string sceneName, string transitionName)
    {
        PlayerController.instance.movement.LockPlayer();

        UIManager.instance.SetUIActive(false);

        transitionAnim.SetTrigger("end");

        yield return new WaitForSeconds(1.3f);

        SceneTransitionName = transitionName;

        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator PlayTransition()
    {
        PlayerController.instance.movement.LockPlayer();

        UIManager.instance.SetUIActive(false);

        transitionAnim.SetTrigger("end");

        yield return new WaitForSeconds(1.3f);
    }
}
