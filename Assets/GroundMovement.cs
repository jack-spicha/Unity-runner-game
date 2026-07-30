using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public Transform player;
    public Transform[] groundPieces;

    public float groundLength = 50f;

    private void Update()
    {
        foreach (Transform ground in groundPieces)
        {
            float groundEnd = ground.position.z + groundLength / 2f;

            if (player.position.z > groundEnd)
            {
                ground.position += Vector3.forward * groundLength * groundPieces.Length;
            }
        }
    }
}