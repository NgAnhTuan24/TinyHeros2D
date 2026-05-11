using System.Collections;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private EndingCredits endingCredits;

    [SerializeField] private float endingDuration = 20f;

    private bool playing;

    private void Start()
    {
        CheckEnding();
    }

    void CheckEnding()
    {
        if (playing)
            return;

        if (GameManager.instance.endingUnlocked &&
            !GameManager.instance.endingPlayed)
        {
            StartCoroutine(PlayEnding());
        }
    }

    IEnumerator PlayEnding()
    {
        playing = true;

        GameManager.instance.isPlayingEnding = true;

        endingCredits.gameObject.SetActive(true);

        UIManager.instance.SetUIActive(false);

        PlayerController.instance.movement.LockPlayer();

        yield return new WaitForSeconds(endingDuration);

        endingCredits.gameObject.SetActive(false);

        UIManager.instance.SetUIActive(true);

        PlayerController.instance.movement.UnlockPlayer();

        GameManager.instance.endingPlayed = true;

        GameManager.instance.isPlayingEnding = false;

        GameManager.instance.SaveGame();

        Debug.Log("Ending completed");
    }
}