using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    [SerializeField] private RockFall rock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            rock.TriggerFall();
        }
    }
}