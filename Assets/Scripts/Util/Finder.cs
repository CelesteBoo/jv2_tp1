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
}
