using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public Transform player;
    public GameObject obstaclePrefab;
    public Transform obstacleParent;

    public float maxX = 5f;
    public float obstacleY = 1f;
    public float deleteDistanceBehindPlayer = 10f;

    public GameManager gameManager;

    private void Update()
    {
        if (!gameManager.gameRunning)
        {
            return;
        }

        GameObject[] obstacles =
            GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            if (
                obstacle.transform.position.z <
                player.position.z - deleteDistanceBehindPlayer
            )
            {
                Destroy(obstacle);
            }
        }
    }

    public void SpawnObstacleAt(float zPosition)
    {
        float randomX = Random.Range(-maxX, maxX);

        Vector3 spawnPosition = new Vector3(
            randomX,
            obstacleY,
            zPosition
        );

        Instantiate(
            obstaclePrefab,
            spawnPosition,
            Quaternion.identity,
            obstacleParent
        );
    }
}