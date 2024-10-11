using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float livingTime = 15.0f;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip spawnClip;
    [SerializeField] private AudioClip pickupClip;

    [SerializeField] private float amountToPickup;

    private AudioSource audioSource;

    private float timeSinceSpawned = 0;
    private bool isPickedUp;

    private ObjectPool pickupObjectPool;

    private Awaitable routine;

    /*private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        pickupObjectPool = Finder.GetPickup(gameObject.tag);
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
            pickupObjectPool.Release(gameObject);
            timeSinceSpawned = 0;
        }

        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null && !isPickedUp)
        {
            // Put the bonus here
            if (gameObject.tag == "Ammo")
            {
                // Replace with bonus method
                Debug.Log("You collected " + amountToPickup + " ammo.");
            } else if (gameObject.tag == "Armor")
            {
                // Replace with bonus method
                Debug.Log("You collected " + amountToPickup + " missiles.");
            } else if (gameObject.tag == "Health")
            {
                // Replace with bonus method
                Debug.Log("You collected " + amountToPickup + " health.");
            }

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
            pickupObjectPool.Release(gameObject);
        }
    }*/
}
