using UnityEngine;

public class CourseManager : MonoBehaviour
{
    public Transform player;

    public ObstacleSpawn obstacleSpawn;
    public GateManager gateManager;

    public GameManager gameManager;

    public float spawnDistance = 100f;

    public int minObstaclesBeforeGate = 3;
    public int maxObstaclesBeforeGate = 6;

    public float minObstacleGap = 10f;
    public float maxObstacleGap = 20f;

    public float gapBeforeGate = 15f;
    public float gapAfterGate = 20f;

    private float nextSpawnZ = 30f;
    private int obstaclesUntilGate;

    private void Start()
    {
        ChooseNextObstacleCount();
    }

    private void Update()
    {
        if (!gameManager.gameRunning)
        {
            return;
        }

        while (nextSpawnZ < player.position.z + spawnDistance)
        {
            if (obstaclesUntilGate > 0)
            {
                SpawnObstacle();
            }
            else
            {
                SpawnGateRow();
            }
        }
    }

    private void SpawnObstacle()
    {
        obstacleSpawn.SpawnObstacleAt(nextSpawnZ);

        nextSpawnZ += Random.Range(
            minObstacleGap,
            maxObstacleGap
        );

        obstaclesUntilGate--;
    }

    private void SpawnGateRow()
    {
        nextSpawnZ += gapBeforeGate;

        gateManager.SpawnGateRowAt(nextSpawnZ);

        nextSpawnZ += gapAfterGate;

        ChooseNextObstacleCount();
    }

    private void ChooseNextObstacleCount()
    {
        obstaclesUntilGate = Random.Range(
            minObstaclesBeforeGate,
            maxObstaclesBeforeGate + 1
        );
    }
}