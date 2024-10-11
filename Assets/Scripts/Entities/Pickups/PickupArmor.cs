using UnityEngine;

public class PickupArmor : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float livingTime = 15.0f;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip spawnClip;
    [SerializeField] private AudioClip pickupClip;

    private AudioSource audioSource;

    private float timeSinceSpawned = 0;
    private bool isPickedUp;

    private ObjectPool pickupArmorObjectPool;

    private Awaitable routine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        pickupArmorObjectPool = Finder.PickupArmorObjectPool;
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
            pickupArmorObjectPool.Release(gameObject);
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
            pickupArmorObjectPool.Release(gameObject);
        }
    }
}
