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

        // Slows down afterwards for some reason. Must find how to fix it.
        transform.Rotate(0, transform.rotation.y + rotationSpeed, 0);
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
