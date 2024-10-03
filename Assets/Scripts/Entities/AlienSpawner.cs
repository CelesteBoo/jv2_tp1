using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Runtime.CompilerServices;

public class AlienSpawner : MonoBehaviour
{
    private ObjectPool alienPrefab;
    private void Awake()
    {
        alienPrefab = Finder.AlienObjectPool;
    }

    public void SpawnAlien()
    {
        if (isActiveAndEnabled)
        {
            var alien = alienPrefab.Get();
            if (alien != null)
            {
                alien.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            }
        }
    }
}
