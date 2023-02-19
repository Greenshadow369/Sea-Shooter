using System.Collections;
using System.Collections.Generic;
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


    private void Awake()
    {
        ManageSingleton();
        cardBuilder = FindObjectOfType<CardBuilder>();
        cardList = new List<UpgradeCardSO>();
        availableCardIDs = new List<int>();
        ownCards = new List<int>();
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

    public int TakeRandomCardID()
    {
        if(availableCardIDs.Count != 0)
        {
            //range must be from 0, and increment by 1
            int randomIndex = Random.Range(0, availableCardIDs.Count);
            Debug.Log("random index " + randomIndex);
            Debug.Log("random ID " + availableCardIDs[randomIndex]);
            Debug.Log("available cards size: " + availableCardIDs.Count);
            int randomCardID = availableCardIDs[randomIndex];
        
            availableCardIDs.Remove(randomCardID);
            Debug.Log("Cards left:" + availableCardIDs.Count);
            return randomCardID;
        }
        else
        {
            Debug.Log("There are no card left");
            return -1;
        }
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
        Debug.Log("applied effect");
        switch(ID)
        {
            case 1:
                StatModifier.instance.IncreasePlayerSpeedModifier(50);
                break;

            case 2:
                
                break;

            case 3:
                
                break;

            case 4:
                StatModifier.instance.IncreasePlayerSpeedModifier(100);
                break;

            default:
                Debug.Log("There are no card with this ID " + "ID: " + ID);
                break;
        }
    }
}
