using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [SerializeField] private int health = 20;
    [SerializeField] private int heightLoweThanPortal = 4;


    private ObjectPool alienPrefab;

    private void Awake()
    {
        alienPrefab = Finder.AlienObjectPool;
    }

    public void SpawnAlien()
    {
        if (isActiveAndEnabled)
        {
            var position = transform.position;
            position.y -= heightLoweThanPortal;
            alienPrefab.Set(position);
        }
    }

    public void getDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            gameObject.SetActive(false);
    }
}
