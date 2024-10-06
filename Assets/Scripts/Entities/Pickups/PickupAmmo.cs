using UnityEngine;

public class PickupAmmo : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float livingTime = 15.0f;

    private float timeSinceSpawned = 0;
    private ObjectPool pickupAmmoObjectPool;

    private void Awake()
    {
        pickupAmmoObjectPool = Finder.PickupAmmoObjectPool;
    }

    void Update()
    {
        timeSinceSpawned += Time.deltaTime;
        if (timeSinceSpawned > livingTime)
        {
            pickupAmmoObjectPool.Release(gameObject);
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

            pickupAmmoObjectPool.Release(gameObject);
        }
    }
}
