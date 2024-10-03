using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AlienSpawnTimer : MonoBehaviour
{
    [SerializeField, Tooltip("In seconds."), Min(0)] private float delay = 1.5f;
    [SerializeField] private GameObject[] portals;

    private Awaitable routine;

    private void OnEnable()
    {
        routine = SpawningRoutine();
    }

    private void OnDisable()
    {
        routine.Cancel();
    }

    private async Awaitable SpawningRoutine()
    {
        while (isActiveAndEnabled)
        {
            var index = Random.Range(0, portals.Length);
            portals[index].GetComponent<AlienSpawner>().SpawnAlien();

            await Awaitable.WaitForSecondsAsync(delay);
        }
    }
}
