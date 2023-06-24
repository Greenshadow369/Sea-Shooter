using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Upgrade Card", fileName = "New Upgrade Card")]
public class UpgradeCardSO : ScriptableObject
{
    /*For card that has upgraded but still applied effect, use "Additional"
    otherwise use "Evolve". Example: shoot 2 -> shoot 3 use "Evolve"
    */
    public enum CardType
    {
        Additional,
        Evolve,
    }
    
    public string cardTitle;
    public Sprite sprite;
    public int cardID;
    public List<UpgradeCardSO> prequesiteCardList;
    public float modifier;
    private bool isUpgraded = false;
    [SerializeField] private CardType cardType;
    [HideInInspector] public string cardDescription;

    public void UpgradeCard()
    {
        isUpgraded = true;
    }

    public void ResetCard()
    {
        isUpgraded = false;
    }

    public bool IsUpgraded()
    {
        return isUpgraded;
    }

    public bool IsCardType(CardType cardType)
    {
        return this.cardType == cardType;
    }
}
