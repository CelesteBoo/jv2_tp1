using UnityEngine;

public class Finder : MonoBehaviour
{
    private static ObjectPool alienObjectPool;

    public static ObjectPool AlienObjectPool
    {
        get
        {
            if (alienObjectPool == null)
            {
                alienObjectPool = GameObject.Find("AlienObjectPool").GetComponent<ObjectPool>();
            }
            return alienObjectPool;
        }
    }

    private static ObjectPool pickupAmmoObjectPool;

    public static ObjectPool PickupAmmoObjectPool
    {
        get
        {
            if (pickupAmmoObjectPool == null)
            {
                pickupAmmoObjectPool = GameObject.Find("PickupAmmoObjectPool").GetComponent<ObjectPool>();
            }
            return pickupAmmoObjectPool;
        }
    }

    private static ObjectPool pickupArmorObjectPool;

    public static ObjectPool PickupArmorObjectPool
    {
        get
        {
            if (pickupArmorObjectPool == null)
            {
                pickupArmorObjectPool = GameObject.Find("PickupArmorObjectPool").GetComponent<ObjectPool>();
            }
            return pickupArmorObjectPool;
        }
    }

    private static ObjectPool pickupHealthObjectPool;

    public static ObjectPool PickupHealthObjectPool
    {
        get
        {
            if (pickupHealthObjectPool == null)
            {
                pickupHealthObjectPool = GameObject.Find("PickupHealthObjectPool").GetComponent<ObjectPool>();
            }
            return pickupHealthObjectPool;
        }
    }


    private static EventChannels eventChannels;

    public static EventChannels EventChannels
    {
        get
        {
            if (eventChannels == null)
            {
                eventChannels = GameObject.FindWithTag("GameController")?
                                        .GetComponent<EventChannels>();
            }
            return eventChannels;
        }
    }

    private static PickupSpawner pickupSpawner;

    public static PickupSpawner PickupSpawner
    {
        get
        {
            if (pickupSpawner == null)
            {
                pickupSpawner = GameObject.Find("PickupSpawner").GetComponent<PickupSpawner>();
            }
            return pickupSpawner;
        }
    }
}
