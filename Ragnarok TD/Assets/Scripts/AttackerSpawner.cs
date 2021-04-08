using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [Range(0f,10f)][SerializeField] float minSpawnDelay = 1f;
    [Range(0.1f,20f)][SerializeField] float maxSpawnDelay = 5f;
    float startingMinSpawnDelay, startingMaxSpawnDelay;
    [SerializeField] float difficultyFactor = 0.2f;
    [SerializeField] Attacker [] attackerPreFab;
    float attackersSpawned = 0;
    bool spawn = true;

    private void Start()
    {
        startingMinSpawnDelay = minSpawnDelay;
        startingMaxSpawnDelay = maxSpawnDelay;
    }

    public IEnumerator StartWave()
    {
        while (spawn)
        {
            SpawnAttacker();
            attackersSpawned += 1;
            IncrementDifficultyTimer();
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

    private void IncrementDifficultyTimer()
    {
        if (minSpawnDelay >= 1f)
        {
            minSpawnDelay = startingMinSpawnDelay - (attackersSpawned * difficultyFactor);
        }
        if (maxSpawnDelay >= 2f)
        {
            maxSpawnDelay = startingMaxSpawnDelay - (attackersSpawned * (difficultyFactor));
        }        
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnAttacker()
    {
        Attacker newAttacker = Instantiate
            (attackerPreFab[UnityEngine.Random.Range(0, attackerPreFab.Length)], transform.position, transform.rotation) 
            as Attacker;
        newAttacker.transform.parent = transform;
    }

}
