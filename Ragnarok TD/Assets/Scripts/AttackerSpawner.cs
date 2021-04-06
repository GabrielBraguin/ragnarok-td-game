using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] Attacker [] attackerPreFab;
    bool spawn = true;

    public IEnumerator StartWave()
    {
        while (spawn)
        {
            SpawnAttacker();
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
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
