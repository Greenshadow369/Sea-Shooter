using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIShop : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    [SerializeField] List<GameObject> slotList;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;
    CardShop cardShop;
    UpgradeCardSO card;
    CardManager cardManager;
    ScoreKeeper scoreKeeper;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
        cardShop = FindObjectOfType<CardShop>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start() {
        cardShop.onCardSelect += ShowCardInformation;
        cardShop.onCardBuy += BuyCard;
        pointText.text = "Points left: " + scoreKeeper.GetCurrentPoint().ToString();
    }

    private void Update()
    {
        
    }

    private void BuyCard(int ID)
    {
        scoreKeeper.DecreasePoint(1);
        pointText.text = "Points left: " + scoreKeeper.GetCurrentPoint().ToString();
        
        cardManager.BuyCard(ID);

        cardShop.DisableActiveToggle();
        Debug.Log("available cards: " + cardManager.availableCardIDs.Count);
        Debug.Log("own cards: " + cardManager.ownCards.Count);
    }
    

    private void ShowCardInformation(int ID)
    {
        if(ID != -1)
        {
            card = cardManager.FindCard(ID);
            titleText.text = card.cardTitle;
            descriptionText.text = card.cardDescription;
        }
        else
        {
            titleText.text = "";
            descriptionText.text = "";
        }
    }
}
