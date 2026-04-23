using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public static MapSpawner instance;

    [SerializeField] private Transform spawnPoint;

    private GameObject currentPortal;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // nếu quên kéo thì tự tìm
        if (spawnPoint == null)
        {
            spawnPoint = GameObject.Find("PortalSpawnPoint")?.transform;
        }
    }

    public void Spawn(GameObject portalPrefab)
    {
        if (spawnPoint == null)
        {
            Debug.LogError("Không tìm thấy spawn point!");
            return;
        }

        if (currentPortal != null)
        {
            Destroy(currentPortal);
        }

        currentPortal = Instantiate(portalPrefab, spawnPoint.position, Quaternion.identity);
    }
}