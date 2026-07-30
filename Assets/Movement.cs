using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float sidewaysSpeed = 8f;
    public float maxX = 5f;

    public GameManager gameManager;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        if (!gameManager.gameRunning)
        {
            return;
        }

        // Forward movement
        transform.Translate(
            Vector3.forward * speed * Time.deltaTime
        );

        // Read all movement input through the Input System
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        float horizontalInput = moveInput.x;

        // Sideways movement
        transform.Translate(
            Vector3.right *
            horizontalInput *
            sidewaysSpeed *
            Time.deltaTime
        );

        // Keep player on platform
        Vector3 position = transform.position;

        position.x = Mathf.Clamp(
            position.x,
            -maxX,
            maxX
        );

        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.Die();
        }
    }
}