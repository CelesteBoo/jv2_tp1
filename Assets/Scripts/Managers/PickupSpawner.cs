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
        // Sometimes spawns multiple collectibles.
        // Collectibles slowly spawn lower and lower.
        // Sometimes it spawns both armor and health. Must fix this.

        var index = Random.Range(0, 4);
        //GameObject pickup;
        position.y += heightABoveAlien;
        switch (index)
        {
            case 1:
                pickupAmmoObjectPool.Set(position);
                /*pickup = pickupAmmoObjectPool.Get();
                if (pickup != null)
                    pickup.transform.SetPositionAndRotation(position, Quaternion.identity);*/
                return;
            case 2:
                pickupArmorObjectPool.Set(position);
                /*pickup = pickupArmorObjectPool.Get();
                if (pickup != null)
                    pickup.transform.SetPositionAndRotation(position, Quaternion.identity);*/
                return;
            case 3:
                pickupHealthObjectPool.Set(position);
                /*pickup = pickupHealthObjectPool.Get();
                if (pickup != null)
                    pickup.transform.SetPositionAndRotation(position, Quaternion.identity);*/
                return;
        }
    }
}
