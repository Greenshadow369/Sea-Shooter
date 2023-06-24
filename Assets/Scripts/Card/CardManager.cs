using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [HideInInspector] public List<UpgradeCardSO> cardList;
    [HideInInspector] public List<int> availableCardIDs;
    [HideInInspector] public List<int> ownCards;
    [SerializeField] Player player;
    LevelManager levelManager;
    CardShop cardShop;
    
    static CardManager instance;
    CardBuilder cardBuilder;
    private List<int> usedIDList;


    private void Awake()
    {
        ManageSingleton();
        cardBuilder = FindObjectOfType<CardBuilder>();
        cardList = new List<UpgradeCardSO>();
        availableCardIDs = new List<int>();
        ownCards = new List<int>();
        usedIDList = new List<int>();
        cardShop = FindObjectOfType<CardShop>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        
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

    // public int TakeRandomCardID()
    // {
    //     if(availableCardIDs.Count != 0)
    //     {
    //         //range must be from 0, and increment by 1
    //         int randomIndex = Random.Range(0, availableCardIDs.Count);
    //         Debug.Log("random index " + randomIndex);
    //         Debug.Log("random ID " + availableCardIDs[randomIndex]);
    //         Debug.Log("available cards size: " + availableCardIDs.Count);
    //         int randomCardID = availableCardIDs[randomIndex];
        
    //         availableCardIDs.Remove(randomCardID);
    //         Debug.Log("Cards left:" + availableCardIDs.Count);
    //         return randomCardID;
    //     }
    //     else
    //     {
    //         Debug.Log("There are no card left");
    //         return -1;
    //     }
    // }

    public List<int> GetRandomAvailableCardIDList()
    {
        List<int> randomList = new List<int>();
        
        if(availableCardIDs.Count > 0)
        {
            foreach(int cardID in availableCardIDs)
            {
                randomList.Add(cardID);
            }

            Shuffle(randomList);

            return randomList;
        }
        return null;
    }

    public UpgradeCardSO FindCard(int ID)
    {
        UpgradeCardSO foundCard = cardList.Find(
            delegate(UpgradeCardSO card)
            {
                return card.cardID == ID;
            }
            );
            if (foundCard != null)
            {
                return foundCard;
            }
            else
            {
                Debug.Log("Not found card ID: " + ID);
                return null;
            }
        
    }

    public Sprite GetCardSprite(int ID)
    {
        
        return FindCard(ID).sprite;
        
    }

    public void FillAvailableCardID()
    {
        for(int i = 0; i < cardList.Count; i++)
        {
            if(ownCards.Contains(cardList[i].cardID) 
                || availableCardIDs.Contains(cardList[i].cardID))
            {
                //This card is already added or owned
                continue;
            }

            var prequesiteIDlist = new List<int>();
            foreach(UpgradeCardSO card in cardList[i].prequesiteCardList)
            {
                //Create a list of prequesite ID
                prequesiteIDlist.Add(card.cardID);
            }

            bool isNotAvailable = false;
            foreach(int ID in prequesiteIDlist)
            {
                if(!ownCards.Contains(ID))
                {
                    //one of the requirement is not met
                    isNotAvailable = true;
                }
            }

            if(isNotAvailable)
            {
                continue;
            }

            availableCardIDs.Add(cardList[i].cardID);
        }
    }

    public void UseCards()
    {
        foreach(int ID in ownCards)
        {
            ApplyCardEffect(ID);
        }
    }

    private void ApplyCardEffect(int ID)
    {
        Player player = FindObjectOfType<Player>();
        UnitState unitState = player.GetComponent<UnitState>();
        
        UpgradeCardSO card = FindCard(ID);
        
        Debug.Log("applied effect");
        if(card.IsCardType(UpgradeCardSO.CardType.Evolve) && card.IsUpgraded())
        {
            //The card is a weaker version of an own card
            return;
        }

        switch(ID)
        {
            case 1:
                if(!usedIDList.Contains(ID))
                {
                    usedIDList.Add(ID);
                    StatModifier.instance.IncreasePlayerSpeedModifier(card.modifier);
                }
                break;

            case 2:
                if(!usedIDList.Contains(ID))
                {
                    usedIDList.Add(ID);
                    StatModifier.instance.IncreasePlayerProjectileDamageModifier(card.modifier);
                }
                break;

            case 3:
                if(!usedIDList.Contains(ID))
                {
                    usedIDList.Add(ID);
                    StatModifier.instance.IncreasePlayerMaxHealthModifier(card.modifier);
                }
                break;

            case 4:
                if(!usedIDList.Contains(ID))
                {
                    usedIDList.Add(ID);
                    StatModifier.instance.IncreasePlayerSpeedModifier(card.modifier);
                }
                break;

            case 5:
                //Apply at every stage
                usedIDList.Add(ID);
                unitState.SetTwoSkewed();
                break;

            case 6:
                //Apply at every stage
                usedIDList.Add(ID);
                unitState.SetThreeSkewed();
                break;

            default:
                Debug.Log("There are no card with this ID " + "ID: " + ID);
                break;
        }
    }

    public void ReturnAllOwnCards()
    {
        foreach(int cardID in ownCards)
        {
            availableCardIDs.Add(cardID);
        }
        ownCards.Clear();
        usedIDList.Clear();
        Debug.Log("return all own cards");
    }

    public void ReturnOwnCard(int cardID)
    {
        ownCards.Remove(cardID);
        availableCardIDs.Add(cardID);
        usedIDList.Remove(cardID);

        foreach(UpgradeCardSO card in cardList)
        {
            card.ResetCard();
        }
    }
    
    public void BuyCard(int cardID)
    {
        availableCardIDs.Remove(cardID);
        ownCards.Add(cardID);

        UpgradeCardSO boughtCard = FindCard(cardID);
        Debug.Log(cardID + " " + boughtCard.IsUpgraded());
        foreach(UpgradeCardSO card in boughtCard.prequesiteCardList)
        {  
            card.UpgradeCard();
        }
    }

    public void Shuffle<T>(IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}