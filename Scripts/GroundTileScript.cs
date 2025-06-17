using UnityEngine;

public class GroundTileScript : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject tallObstaclePrefab;
    [SerializeField] float tallObstacleChance = 0.2f;
    [SerializeField] GameObject coinPrefab;
    GroundSpawner groundSpawner;

    [SerializeField] GameObject[] treePrefabs;
    [SerializeField] int numberOfTrees = 30;

    private void Start()
    {
        groundSpawner = FindFirstObjectByType<GroundSpawner>();
        SpawnTrees();
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    private void Update()
    {

    }



    public void SpawnObstacle()
    {
        GameObject obstacleToSpawn = obstaclePrefab;
        float chance = Random.Range(0f, 1f);
        if (chance <= tallObstacleChance)
        {
            obstacleToSpawn = tallObstaclePrefab;
        }

        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);
    }




    public void SpawnCoins()
    {
        int coinsToSpawn = 10;
        float[] laneX = { -3f, 0f, 3f };

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float x = laneX[Random.Range(0, laneX.Length)];
            float z = Random.Range(2f, 28f); 
            Vector3 spawnPos = transform.position + new Vector3(x, 1f, z);

            Collider[] colliders = Physics.OverlapSphere(spawnPos, 1f);
            bool tooCloseToObstacle = false;

            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Obstacle") || col.CompareTag("Tree"))
                {
                    tooCloseToObstacle = true;
                    break;
                }
            }

            if (!tooCloseToObstacle)
            {
                Instantiate(coinPrefab, spawnPos, Quaternion.identity, transform);
            }
            else
            {
                i--; 
            }
        }
    }


    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }

    public void SpawnTrees()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            
            int index = Random.Range(0, treePrefabs.Length);
            GameObject treePrefab = treePrefabs[index];

            float xPos = Random.value > 0.5f ? 9f : -9f;

            float zPos = Random.Range(0.5f, 20f);

            Vector3 spawnPos = transform.position + new Vector3(xPos, 0, zPos);
            Instantiate(treePrefab, spawnPos, Quaternion.identity, transform);
        }
    }


}