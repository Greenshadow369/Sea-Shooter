using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardShop : MonoBehaviour
{
    [SerializeField] List<GameObject> cardBox;
    List<int> cardIDBox;
    UpgradeCardSO card;
    CardManager cardManager;
    ScoreKeeper scoreKeeper;
    Image image;
    ToggleGroup toggleGroup;
    List<Toggle> toggles;

    
    public delegate void OnCardSelect(int cardID);
    public event OnCardSelect onCardSelect;
    public delegate void OnCardBuy(int cardID);
    public event OnCardBuy onCardBuy;

    private void Awake() {
        cardManager = FindObjectOfType<CardManager>();
        toggleGroup = GetComponent<ToggleGroup>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        image = GetComponent<Image>();
        cardIDBox = new List<int>();
        toggles = new List<Toggle>();
        
    }

    private void Start()
    {
        for(int i = 0; i < cardBox.Count; i++)
        {
            toggles.Add(cardBox[i].GetComponent<Toggle>());
        }
        cardManager.FillAvailableCardID();
        PrepareCards();
        ShowCard();
        
    }

    private void Update()
    {
        if(toggleGroup.AnyTogglesOn())
        {
            GetCardInformation(GetActiveToggleID());
        }
        
    }

    private void PrepareCards()
    {
        for(int i = 0; i < cardBox.Count; i++)
        {
            cardIDBox.Add(cardManager.TakeRandomCardID());
        }
        
    }


    private void ShowCard()
    {
        for(int i = 0; i < cardBox.Count; i++)
        {
            Debug.Log("Card ID in box: " + cardIDBox[i]);
        }
        for(int i = 0; i < cardBox.Count; i++)
        {
            if(cardIDBox[i] == -1)
            {
                cardBox[i].GetComponent<Image>().sprite = null;
            }
            else
            {
                cardBox[i].GetComponent<Image>().sprite = cardManager.GetCardSprite(cardIDBox[i]);
            }
            
        }

    }

    public void BuyButtonClicked()
    {
        if(onCardBuy != null)
        {
            if(canBuyCard())
            {
                onCardBuy(GetActiveToggleID());
            }
        }
    }

    public int GetCardIDInSlot(int index)
    {
        return cardIDBox[index];
    }

    public void DisableActiveToggle()
    {
        for(int i = 0; i < cardBox.Count; i++)
        {
            if(toggles[i].isOn)
            {
                    toggles[i].interactable = false;
            }
        }
    }

    private void GetCardInformation(int ID)
    {
        if(onCardSelect != null)
        {
            onCardSelect(ID);
        }
    }

    private int GetActiveToggleID()
    {
        for(int i = 0; i < cardBox.Count; i++)
        {
            if(toggles[i].isOn)
            {
                    return cardIDBox[i];
            }
        }
        return -1;
    }

    private bool canBuyCard()
    {
        if(scoreKeeper.GetCurrentPoint() > 0)
        {
            return true;
        }
        return false;
    }
}
