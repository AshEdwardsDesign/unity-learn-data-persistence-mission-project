using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnDelay = 3;
    public GameObject ObstaclePrefab;
    [SerializeField]
    private float xSpawnMin = -2;
    [SerializeField]
    private float xSpawnMax = 2;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", 1, spawnDelay);
    }

    private void SpawnObstacle()
    {
        float xPosition = Random.Range(xSpawnMin, xSpawnMax);
        Vector3 spawnPosition = new Vector3(xPosition, transform.position.y, transform.position.z);
        Instantiate(ObstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
