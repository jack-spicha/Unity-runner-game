using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float zOffset = -5f;

    private float fixedX;
    private float fixedY;

    private void Start()
    {
        fixedX = transform.position.x;
        fixedY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        transform.position = new Vector3(
            fixedX,
            fixedY,
            target.position.z + zOffset
        );
    }
}