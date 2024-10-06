using UnityEngine;

public class PickupArmor : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float livingTime = 15.0f;

    private float timeSinceSpawned = 0;
    private ObjectPool pickupArmorObjectPool;

    private void Awake()
    {
        pickupArmorObjectPool = Finder.PickupArmorObjectPool;
    }

    void Update()
    {
        timeSinceSpawned += Time.deltaTime;
        if (timeSinceSpawned > livingTime)
        {
            pickupArmorObjectPool.Release(gameObject);
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

            pickupArmorObjectPool.Release(gameObject);
        }
    }
}
