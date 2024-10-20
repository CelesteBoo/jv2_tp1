using UnityEngine;
using UnityEngine.Audio;

public class Bullet : MonoBehaviour
{
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
        if (collision != null) {
            Debug.Log("boing");
            bulletPool.Release(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = direction * speed * Time.deltaTime; 
    }

    public void fireBullet(Vector3 playerPos, Vector3 playerDir)
    {
        playerPos.y += 10;
        var bullet = bulletPool.Get();
        if (bullet != null)
        {
            bullet.transform.SetPositionAndRotation(playerPos, transform.rotation);
        }
        direction = playerDir;
    }
}
