using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlash;
    [SerializeField] private float restoreDefaulMatTime = .1f;

    private Material defaulMat;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaulMat = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlash;
        yield return new WaitForSeconds(restoreDefaulMatTime);
        spriteRenderer.material = defaulMat;
    }
}
