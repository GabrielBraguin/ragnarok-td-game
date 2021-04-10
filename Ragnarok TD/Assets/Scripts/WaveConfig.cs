using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [Range(0f, 10f)] [SerializeField] float timeBetweenSpawns = 0.5f;
    [Range(0f, 2f)] [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 10;

    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; set => timeBetweenSpawns = value; }
    public float SpawnRandomFactor { get => spawnRandomFactor; set => spawnRandomFactor = value; }
    public int NumberOfEnemies { get => numberOfEnemies; set => numberOfEnemies = value; }
    public float MinSpawnDelay { get => (timeBetweenSpawns - spawnRandomFactor); }
    public float MaxSpawnDelay { get => (timeBetweenSpawns + spawnRandomFactor); }
}
