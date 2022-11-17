using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyHolderSO> enemyHolders;
    [SerializeField] float timeBetweenHolders = 0f;
    List<WaveConfigSO> waves;
    WaveConfigSO currentWave;
    EnemyHolderSO currentHolder;
    void Start()
    {   
        
        StartCoroutine(StartEnemyHolders());
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            waves = currentHolder.GetWaveList();
            for(int i = 0; i < currentHolder.GetWaveCount(); i++)
            {
                WaveConfigSO wave = null;
                wave = waves[i];
                currentWave = wave;
                for(int j = 0; j < currentWave.GetEnemyCount(); j++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(j),
                                currentWave.GetStartingWayPoint().position,
                                Quaternion.Euler(0, 0, 180),
                                transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(currentHolder.GetTimeBetweenWaves());
            }
        }
        while(currentHolder.IsLooping());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator StartEnemyHolders()
    {
            foreach(EnemyHolderSO holder in enemyHolders)
            {
                currentHolder = holder;
                yield return StartCoroutine(SpawnEnemyWaves());
                yield return new WaitForSeconds(timeBetweenHolders);
            }
        
    }

    public EnemyHolderSO GetCurrentHolder()
    {
        return currentHolder;
    }

    public int GetHolderCount()
    {
        return enemyHolders.Count;
    }

    
}
