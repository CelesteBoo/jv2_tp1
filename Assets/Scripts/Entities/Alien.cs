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

    private NavMeshAgent navAgent;

    private ObjectPool alienObjectPool;
    private PickupSpawner pickupSpawner;

    public Vector3 Velocity => navAgent.velocity;

    private void Awake()
    {
        alienObjectPool = Finder.AlienObjectPool;
        pickupSpawner = Finder.PickupSpawner;

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.enabled = false;
    }

    private void Update()
    {
        // Alien needs animation.

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
        if (player is not null)
        {
            alienObjectPool.Release(gameObject);

            // Aliens need to maybe drop a pickup when they die.
            var index = Random.Range(0, 100);
            if (index < pickupSpawnChance)
                pickupSpawner.SpawnRandomPickup(transform.position);
        }
    }
}
