using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyHolder", fileName = "New Enemy Holder")]
public class EnemyHolderSO : ScriptableObject
{
    [SerializeField] List<WaveConfigSO> waves;
    [SerializeField] float timeBetweenWaves = 0f;
    public bool isWavesRandomize;
    WaveConfigSO currentWave;
    List<WaveConfigSO> alpha;
    //isLooping should only be used for endless
    bool isLooping = false;

    private void Awake() {
        alpha = new List<WaveConfigSO>();
    }

    public float GetTimeBetweenWaves()
    {
        return timeBetweenWaves;
    }

    public bool IsLooping()
    {
        return isLooping;
    }

    public List<WaveConfigSO> GetWaveList()
    {
        
        CopyList(waves);
        if(isWavesRandomize)
        {
            
            RandomizeWave(alpha);
        }
        return alpha;
    }

    public int GetWaveCount()
    {
        return waves.Count;
    }

    public WaveConfigSO GetWave(int index)
    {
        return waves[index];
    }

    public void RandomizeWave(List<WaveConfigSO> beta)
    {
        for (int i = 0; i < beta.Count; i++)
        {
            WaveConfigSO temp = beta[i];
            int randomIndex = Random.Range(i, alpha.Count);
            beta[i] = beta[randomIndex];
            beta[randomIndex] = temp;
            alpha = beta;
        }
    }
    
    public void CopyList(List<WaveConfigSO> list)
    {
        for(int i= 0; i < waves.Count; i++)
        {
            alpha.Add(waves[i]);
        }
    }
}
