using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 30f;
    [SerializeField] private float rotationSpeed = 0.1f;

    [Header("Inputs")]
    [SerializeField] private InputActionReference moveAction;

    private CharacterController controller;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main?.transform;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 moveVector = Vector3.zero;

        //Copié de l'exercice cinemachine
        // Get directions.
        var relativeTransform = cameraTransform ?? transform;
        var forward = relativeTransform.forward;
        var right = relativeTransform.right;

        // Remove Y component from vectors.
        forward.y = 0;
        right.y = 0;

        // Calculate movement direction.
        var moveInput = moveAction.action.ReadValue<Vector2>();

        //Apply gravity.
        if (!controller.isGrounded)
        {
            moveVector += Physics.gravity;
        }

        if (moveInput != Vector2.zero)
        {
            var moveDirection = forward * moveInput.y + right * moveInput.x;
            var lookRotation = Quaternion.LookRotation(moveDirection);

            moveVector += moveDirection * speed;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed);
        }

        // Apply movement.
        controller.Move(moveVector * Time.deltaTime);

    }
}
