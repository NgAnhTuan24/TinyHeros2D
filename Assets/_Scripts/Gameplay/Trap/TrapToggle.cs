using UnityEngine;
using System.Collections;

public class TrapToggle : MonoBehaviour
{
    [SerializeField] private float showTime = 2f;
    [SerializeField] private float hideTime = 2f;

    private SpriteRenderer sr;
    private Collider2D col;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        StartCoroutine(ToggleTrap());
    }

    IEnumerator ToggleTrap()
    {
        while (true)
        {
            // Show
            SetActive(true);
            yield return new WaitForSeconds(showTime);

            // Hide
            SetActive(false);
            yield return new WaitForSeconds(hideTime);
        }
    }

    void SetActive(bool state)
    {
        sr.enabled = state;
        col.enabled = state;
    }
}