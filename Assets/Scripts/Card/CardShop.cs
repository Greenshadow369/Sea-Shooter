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
        GetCardInformation(GetActiveToggleID());
    }

    private void PrepareCards()
    {
        // List<int> randomList = new List<int>();
        // for(int i = 0; i < cardBox.Count; i++)
        // {
        //     randomList.Add(cardManager.TakeRandomCardID());
        // }
        
        // foreach(int randomID in randomList)
        // {
        //     cardIDBox.Add(randomID);
        // }

        List<int> randomList = new List<int>();
        randomList = cardManager.GetRandomAvailableCardIDList();
        //randomList = (randomList == null) ? cardManager.GetRandomAvailableCardIDList() : randomList;

        //Get count of randomList
        int randomListCount;
        if(randomList == null)
        {
            randomListCount = 0;
        }
        else
        {
            randomListCount = randomList.Count;
        }

        //Put each random ID in card box
        for(int i = 0; i < cardBox.Count; i++)
        {
            if(i < randomListCount)
            {
                //Index is lower than list size
                cardIDBox.Add(randomList[i]);
            }
            else
            {
                //There are no cards left to add
                cardIDBox.Add(-1);
            }
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
                cardBox[i].GetComponent<Image>().enabled = false;
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
                int cardID = GetActiveToggleID();
                //Card ID is valid
                if(cardID != -1)
                {
                    onCardBuy(cardID);
                }
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
                toggles[i].isOn = false;
            }
        }
    }

    private void GetCardInformation(int ID)
    {
        if(onCardSelect != null)
        {
            ShowSelectedVisual();
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

    public void ShowSelectedVisual()
    {
        for(int i = 0; i < cardBox.Count; i++)
        {
            if(toggles[i].isOn)
            {
                cardBox[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                cardBox[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}
