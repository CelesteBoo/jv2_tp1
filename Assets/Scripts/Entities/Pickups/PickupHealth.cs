using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float livingTime = 15.0f;

    private float timeSinceSpawned = 0;
    private ObjectPool pickupHealthObjectPool;

    private void Awake()
    {
        pickupHealthObjectPool = Finder.PickupHealthObjectPool;
    }

    void Update()
    {
        timeSinceSpawned += Time.deltaTime;
        if (timeSinceSpawned > livingTime)
        {
            pickupHealthObjectPool.Release(gameObject);
            timeSinceSpawned = 0;
        }

        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player is not null)
        {
            // Must make the player get the bonus.

            pickupHealthObjectPool.Release(gameObject);
        }
    }
}
