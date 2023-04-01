using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LVText;
    ScoreKeeper scoreKeeper;
    CardManager cardManager;
    StatModifier statModifier;

    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        cardManager = FindObjectOfType<CardManager>();
        statModifier = FindObjectOfType<StatModifier>();
    }

    private void Start() {
        LVText.text = scoreKeeper.GetCurrentLevel().ToString();

        scoreKeeper.ResetScore();
        scoreKeeper.ResetPlayerLevel();
        scoreKeeper.ResetPoint();

        cardManager.ReturnAllOwnCards();

        statModifier.ResetPlayerSpeedModifier();
        statModifier.ResetPlayerMaxHealthModifier();
    }
}
