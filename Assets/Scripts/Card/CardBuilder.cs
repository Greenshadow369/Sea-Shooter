using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*To make a new card: 
1.make a UpgradeCardSO with new ID and title and put in rawCardList
2.make new case and description
3.set effect according to card to other scripts
*/

public class CardBuilder : MonoBehaviour
{
    [SerializeField] List<UpgradeCardSO> rawCardList;
    CardManager cardManager;
    static CardBuilder instance;

    private void Awake() {
        ManageSingleton();
        cardManager = FindObjectOfType<CardManager>();
        FillCardList();
    }

    void Start()
    {
        
    }

    private UpgradeCardSO MakeCard(UpgradeCardSO card)
    {
        switch(card.cardID)
        {
            case 1:
                card.cardDescription = "Swim " + card.modifier + "% " + "faster";
                break;

            case 2:
                card.cardDescription = "Projectile deal " + card.modifier + "% " + "more damge";
                break;

            case 3:
                card.cardDescription = "Maximum health increase by " + card.modifier + "% " + "more";
                break;

            case 4:
                card.cardDescription = "Swim an additonal " + card.modifier + "% " + "faster";
                break;

            case 5:
                card.cardDescription = "Shoot 2 projectiles";
                break;

            case 6:
                card.cardDescription = "Shoot 3 projectiles";
                break;

            default:
                Debug.Log("There are no card with this ID " + "ID: " + card.cardID);
                break;
        }
        return card;
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

    private void FillCardList()
    {
        for(int i = 0; i < rawCardList.Count; i++)
        {
            UpgradeCardSO card = rawCardList[i];
            cardManager.cardList.Add(MakeCard(card));
        }
    }

    public List<UpgradeCardSO> GetCardList()
    {
        return cardManager.cardList;
    }
}
