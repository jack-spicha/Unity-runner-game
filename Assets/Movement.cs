using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float sidewaysSpeed = 8f;
    public float maxX = 5f;

    public GameManager gameManager;

    private void Update()
    {
        if (!gameManager.gameRunning)
        {
            return;
        }

        // Forward movement
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Sideways movement
        float horizontalInput = 0f;

        if (Keyboard.current.aKey.isPressed)
        {
            horizontalInput = -1f;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            horizontalInput = 1f;
        }

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