using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<Transform> enemies;
    [SerializeField] private int shitToSpawn = 10;

    public bool Spawning;
    [SerializeField] private int minEnemiesPerSpawn, maxEnemiesPerSpawn;
    [SerializeField] private float minSpawnInterval, maxSpawnInterval;

    private void OnEnable()
    {
        GolfGoal.OnGolfStart += StopSpawning;
        GolfGoal.OnGolfEnd += StartSpawning;
        GolfGoal.OnGolfEnd += SpawnShitTonOfStuff;
    }

    private void OnDisable()
    {
        GolfGoal.OnGolfStart -= StopSpawning;
        GolfGoal.OnGolfEnd -= StartSpawning;
        GolfGoal.OnGolfEnd -= SpawnShitTonOfStuff;
    }

    void StartSpawning()
    {
        Spawning = true;
    }
    void StopSpawning()
    {
        Spawning = false;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void SpawnShitTonOfStuff()
    {
        for (int i = 0; i < shitToSpawn; i++)
        {
            var enemy = Instantiate(enemies[Random.Range(0, enemies.Count)]);
            enemy.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        }
    }
    
    private IEnumerator SpawnEnemies()
    {
        while(true)
        {
            if(Spawning)
            {
                int numberOfEnemies = Random.Range(minEnemiesPerSpawn, maxEnemiesPerSpawn);
                for (int i = 0; i < numberOfEnemies; i++)
                {
                    var enemy = Instantiate(enemies[Random.Range(0, enemies.Count)]);
                    enemy.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                }
            }
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }
}
