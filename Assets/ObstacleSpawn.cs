using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public Transform player;
    public GameObject obstaclePrefab;
    public Transform obstacleParent;
    public GameManager gameManager;

    public float maxX = 5f;
    public float spawnDistance = 80f;

    public float minGap = 10f;
    public float maxGap = 20f;

    public float obstacleY = 1f;
    public float deleteDistanceBehindPlayer = 10f;

    private float nextSpawnZ = 30f;

    private void Update()
    {
        // Do nothing unless the game is running
        if (!gameManager.gameRunning)
        {
            return;
        }

        // Spawn obstacles ahead
        while (nextSpawnZ < player.position.z + spawnDistance)
        {
            SpawnObstacle();
            nextSpawnZ += Random.Range(minGap, maxGap);
        }

        // Delete obstacles the player has passed
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.transform.position.z < player.position.z - deleteDistanceBehindPlayer)
            {
                Destroy(obstacle);
            }
        }
    }

    private void SpawnObstacle()
    {
        float randomX = Random.Range(-maxX, maxX);

        Vector3 spawnPosition = new Vector3(
            randomX,
            obstacleY,
            nextSpawnZ
        );

        Instantiate(
            obstaclePrefab,
            spawnPosition,
            Quaternion.identity,
            obstacleParent
        );
    }
}