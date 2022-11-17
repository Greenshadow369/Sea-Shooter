using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("LV")]
    [SerializeField] Slider LVSlider;
    [SerializeField] TextMeshProUGUI LVText;
    ScoreKeeper scoreKeeper;

    private void Awake() 
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start() 
    {
        healthSlider.maxValue = playerHealth.GetHealth();
        LVSlider.maxValue = scoreKeeper.GetMaxScore();
    }

    private void Update()
    {
        healthSlider.value = playerHealth.GetHealth();
        LVSlider.value = scoreKeeper.GetScore();
        LVText.text = scoreKeeper.GetCurrentLevel().ToString();
        LVSlider.maxValue = scoreKeeper.GetMaxScore();
    }
}
