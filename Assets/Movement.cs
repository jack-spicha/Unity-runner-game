using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float sidewaysSpeed = 8f;
    public float maxX = 5f;

    private void Update()
    {
        // Move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Get sideways input
        float horizontalInput = 0f;

        if (Keyboard.current.aKey.isPressed)
        {
            horizontalInput = -1f;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            horizontalInput = 1f;
        }

        // Move left/right
        transform.Translate(Vector3.right * horizontalInput * sidewaysSpeed * Time.deltaTime);

        // Stop player going beyond the edges
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -maxX, maxX);
        transform.position = position;
    }
}