using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    float attackersSpawned = 0;
    bool spawn = true;


    public IEnumerator StartWave()
    {
        while (spawn)
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.NumberOfEnemies; enemyCount++)
        {
            if(FindObjectOfType<GameController>().levelTimerFinished == false) 
            {
                var newEnemy = Instantiate(waveConfig.EnemyPrefab, transform.position, transform.rotation);
                newEnemy.transform.parent = transform;
                attackersSpawned += 1;
            }
            yield return new WaitForSeconds(Random.Range(waveConfig.MinSpawnDelay, waveConfig.MaxSpawnDelay));
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }
}
