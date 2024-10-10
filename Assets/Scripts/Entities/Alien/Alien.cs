using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Alien : MonoBehaviour
{
    // Target must be from the scene and not prefabs, it doesn't track the target otherwise.
    [Header("Alien")]
    [SerializeField] private GameObject target;
    [SerializeField] private float floorLevel = -72;

    [Header("Pickup")]
    [SerializeField] private float pickupSpawnChance = 5;

    [Header("AlienDeathSound")]
    [SerializeField] private AudioClip deathClip;

    private AudioSource audioSource;

    private NavMeshAgent navAgent;

    private ObjectPool alienObjectPool;
    private PickupSpawner pickupSpawner;

    public Vector3 Velocity => navAgent.velocity;

    private Awaitable routine;
    private ParticleSystem particles;
    private GameObject body;

    private void Awake()
    {
        alienObjectPool = Finder.AlienObjectPool;
        pickupSpawner = Finder.PickupSpawner;

        audioSource = GetComponent<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();
        navAgent = GetComponent<NavMeshAgent>();
        body = GetComponentInChildren<Animator>().gameObject;
    }

    private void OnEnable()
    {
        navAgent.enabled = false;
        particles.Stop();
    }

    private void Update()
    {
        if (navAgent.enabled == true)
        {
            navAgent.destination = target.transform.position;
            return;
        }

        // Alien shouldn't reactivate navAgent with an hardcoded value.
        if (transform.position.y <= floorLevel)
            navAgent.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Bullets should also kill the aliens.
        var player = collision.gameObject.GetComponent<Player>();
        if (body.activeSelf && player != null)
        {
            var index = Random.Range(0, 100);
            if (index < pickupSpawnChance)
                pickupSpawner.SpawnRandomPickup(transform.position);

            routine = KillRoutine();
        }
    }

    private async Awaitable KillRoutine()
    {
        while (isActiveAndEnabled)
        {
            body.SetActive(false);
            particles.Play();
            audioSource.PlayOneShot(deathClip);

            await Awaitable.WaitForSecondsAsync(particles.main.duration);
            body.SetActive(true);
            
            alienObjectPool.Release(gameObject);
        }
    }
}
