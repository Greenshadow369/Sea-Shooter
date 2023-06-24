using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] float maxScore = 100f;
    [SerializeField] float scoreIncreasePerLV = 50f;
    float currentScore = 0f;
    float leftOverScore = 0f;
    float startingMaxScore;
    int currentLevel = 1;
    int point = 2;
    static ScoreKeeper instance;

    private void Awake() {
        ManageSingleton();
    }

    private void Start() {
        startingMaxScore = maxScore;
    }

    private void Update() {
        LevelUp();
    }

    private void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public float GetScore()
    {
        return currentScore;
    }

    public float GetMaxScore()
    {
        return maxScore;
    }

    public void IncreaseScore(float points)
    {
        currentScore += points;
        Mathf.Clamp(currentScore, 0, float.MaxValue);
;    }

    public float SetScore(float score)
    {
        currentScore = score;
        return Mathf.Clamp(currentScore, 0, float.MaxValue);
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public void ResetPlayerLevel()
    {
        currentLevel = 1;
        maxScore = startingMaxScore;
    }

    public void ResetPoint()
    {
        point = 1;
    }

    private void LevelUp()
    {
        if(currentScore >= maxScore)
        {
            leftOverScore = currentScore - maxScore;
            ResetScore();
            currentScore = leftOverScore; 
            currentLevel++;
            maxScore += scoreIncreasePerLV;
            point++;
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void DecreasePoint(int cost)
    {
        point = point - cost;
    }

    public int GetCurrentPoint()
    {
        return point;
    }
    
}
