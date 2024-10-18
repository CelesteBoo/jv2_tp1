using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private float heightABoveAlien = 5;

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
        // Collectibles slowly spawn lower and lower.

        var index = Random.Range(0, 3);
        position.y += heightABoveAlien;
        switch (index)
        {
            case 0:
                pickupAmmoObjectPool.Set(position);
                return;
            case 1:
                pickupArmorObjectPool.Set(position);
                return;
            case 2:
                pickupHealthObjectPool.Set(position);
                return;
        }
    }
}
