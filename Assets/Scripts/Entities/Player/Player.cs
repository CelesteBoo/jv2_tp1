using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 30f;
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float jumpHeight = 125f;

    [Header("Inputs")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private InputActionReference altFireAction;

    [Header("Stats")]
    [SerializeField] private int maxNbMissiles;
    [SerializeField] private float maxRefireTimer = 0.1f;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float maxInvTimer = 0.5f;

    [Header("SFX")]
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioClip altFireClip;

    private AudioSource audioSource;

    private CharacterController controller;
    private Transform cameraTransform;

    private Vector3 maxJumpVector;
    private Vector3 jumpVector = Vector3.zero;

    private float refireTimer;
    private int nbMissiles;
    private int health;
    private float invTimer;
    private ObjectPool bulletPool;

    private void Awake()
    {
        cameraTransform = Camera.main?.transform;
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        maxJumpVector = transform.up * jumpHeight;
        refireTimer = maxRefireTimer;
        nbMissiles = 0;
        health = maxHealth;
        UpdateHealth();
        invTimer = 0;
        bulletPool = Finder.BulletPool;
    }

    private void FixedUpdate()
    {
        Vector3 moveVector = Vector3.zero;

        // Copié en grande partie de l'exercice cinemachine
        // Obtenir les directions
        var relativeTransform = cameraTransform ?? transform;
        var forward = relativeTransform.forward;
        var right = relativeTransform.right;

        // Retirer le y des vecteurs
        forward.y = 0;
        right.y = 0;

        // Calculer la direction de mouvement
        var moveInput = moveAction.action.ReadValue<Vector2>();

        // Appliquer la gravité
        if (!controller.isGrounded)
        {
            moveVector += Physics.gravity;
            jumpVector += Physics.gravity;
        }

        // Appliquer le saut
        if (jumpAction.action.IsPressed() && controller.isGrounded)
        {
            jumpVector = maxJumpVector;
        }

        // Projectiles
        refireTimer -= Time.deltaTime;

        if(fireAction.action.IsPressed() && refireTimer < 0)
        {
            var bullet = bulletPool.Get();
            if (bullet != null)
            {
                bullet.GetComponent<Bullet>().fireBullet(transform.position, transform.forward);
            }

            if (fireClip != null)
                audioSource.PlayOneShot(fireClip);
            refireTimer = maxRefireTimer;
        }

        if (altFireAction.action.IsPressed() && refireTimer < 0 && nbMissiles > 0)
        {
            refireTimer = maxRefireTimer;
            if (altFireClip != null)
                audioSource.PlayOneShot(altFireClip);
        }

        invTimer += Time.deltaTime;

        if (moveInput != Vector2.zero)
        {
            var moveDirection = forward * moveInput.y + right * moveInput.x;
            var lookRotation = Quaternion.LookRotation(moveDirection);

            moveVector += moveDirection * speed;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed);
        }

        // Appliquer le mouvement
        controller.Move((moveVector + jumpVector) * Time.deltaTime);
    }

    public void HurtPlayer(int damage)
    {
        if (invTimer > maxInvTimer)
        {
            health -= damage;
            invTimer = 0;

            if (health <= 0)
                KillPlayer();

            if (hurtClip != null)
                audioSource.PlayOneShot(hurtClip);

            UpdateHealth();
        }
    }

    public void HealPlayer(int healingPoints)
    {
        health += healingPoints;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        Finder.InterfaceManager.UpdateHealth(health);
    }

    private void KillPlayer()
    {
        if (deathClip != null)
            audioSource.PlayOneShot(deathClip);

        Finder.EventChannels.PublishGameLost();

        gameObject.SetActive(false);
    }
}
