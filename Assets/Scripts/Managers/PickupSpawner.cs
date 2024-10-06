using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupSpawner : MonoBehaviour
{
    private ObjectPool pickupAmmoObjectPool;
    private ObjectPool pickupArmorObjectPool;
    private ObjectPool pickupHealthObjectPool;

    private void Awake()
    {
        pickupAmmoObjectPool = Finder.PickupAmmoObjectPool;
        pickupArmorObjectPool = Finder.PickupArmorObjectPool;
        pickupHealthObjectPool = Finder.PickupHealthObjectPool;
    }

    public void SpawnRandomPickup(Vector3 position)
    {
        // Sometimes it spawns both armor and health. Must fix this.
        var index = Random.Range(0, 4);
        GameObject pickup;
        position.y += 5;
        switch (index)
        {
            case 1:
                pickup = pickupAmmoObjectPool.Get();
                if (pickup != null)
                    pickup.transform.SetPositionAndRotation(position, Quaternion.identity);
                return;
            case 2:
                pickup = pickupArmorObjectPool.Get();
                if (pickup != null)
                    pickup.transform.SetPositionAndRotation(position, Quaternion.identity);
                return;
            case 3:
                pickup = pickupHealthObjectPool.Get();
                if (pickup != null)
                    pickup.transform.SetPositionAndRotation(position, Quaternion.identity);
                return;
        }
    }
}
