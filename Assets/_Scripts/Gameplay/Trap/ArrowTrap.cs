using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;

    public void Shooter()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        Arrow arrowScript = arrow.GetComponent<Arrow>();

        if (arrowScript != null)
        {
            float dir = transform.localScale.x > 0 ? 1f : -1f;
            arrowScript.SetDirection(dir);
        }
    }
}
