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
    }

    private void Update()
    {
        // Target should always look directly at the target no matter where it is.
        navAgent.destination = target.transform.position;
    }
}
