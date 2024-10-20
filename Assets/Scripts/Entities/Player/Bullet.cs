using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 5000f;

    private ObjectPool bulletPool;
    private new Rigidbody rigidbody;
    private Vector3 direction;

    void Awake()
    {
        bulletPool = Finder.BulletPool;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var alien = collision.gameObject.GetComponent<Alien>();
        if (alien != null)
        {
            bulletPool.Release(gameObject);
            return;
        }

        var alienSpawner = collision.gameObject.GetComponent<AlienSpawner>();
        if (alienSpawner != null)
        {
            alienSpawner.getDamage(damage);
            bulletPool.Release(gameObject);
            return;
        }

        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
            return;

        if (collision != null)
            bulletPool.Release(gameObject);
    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = direction * speed * Time.deltaTime; 
    }

    public void fireBullet(Vector3 playerPos, Vector3 playerDir)
    {
        playerPos.y += 4;
        transform.SetPositionAndRotation(playerPos, transform.rotation);

        direction = playerDir;
    }
}
