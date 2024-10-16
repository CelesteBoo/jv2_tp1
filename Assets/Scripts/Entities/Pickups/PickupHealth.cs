using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float livingTime = 15.0f;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip spawnClip;
    [SerializeField] private AudioClip pickupClip;

    private AudioSource audioSource;

    private float timeSinceSpawned = 0;
    private bool isPickedUp;

    private ObjectPool pickupHealthObjectPool;

    private Awaitable routine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        pickupHealthObjectPool = Finder.PickupHealthObjectPool;
    }

    private void OnEnable()
    {
        isPickedUp = false;
        audioSource.PlayOneShot(spawnClip);
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
        if (player != null && !isPickedUp)
        {
            // Must make the player get the bonus.
            routine = PickupRoutine();
        }
    }

    private async Awaitable PickupRoutine()
    {
        while (isActiveAndEnabled)
        {
            isPickedUp = true;
            audioSource.PlayOneShot(pickupClip);
            await Awaitable.WaitForSecondsAsync(pickupClip.length);
            pickupHealthObjectPool.Release(gameObject);
        }
    }
}
