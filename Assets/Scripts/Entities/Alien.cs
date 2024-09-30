using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Alien : MonoBehaviour
{
    // Target must be from the scene and not prefabs, it doesn't track the target otherwise.
    [SerializeField] private GameObject target;

    private NavMeshAgent navAgent;

    public Vector3 Velocity => navAgent.velocity;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.enabled = false;
    }

    private void Update()
    {
        // Alien needs animation.

        // Alien should always look directly at the target no matter where it is.

        if (navAgent.enabled == true)
        {
            navAgent.destination = target.transform.position;
            return;
        }

        // Alien shouldn't reactivate navAgent with an hardcoded value.
        if (transform.position.y <= -72)
            navAgent.enabled = true;
    }
}
