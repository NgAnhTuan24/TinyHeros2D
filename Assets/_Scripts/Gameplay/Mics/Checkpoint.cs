using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;

        if (other.GetComponent<PlayerController>())
        {
            activated = true;
            GameManager.instance.checkpointManager.SetCheckpoint(transform.position);
        }
    }
}