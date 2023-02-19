using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Upgrade Card", fileName = "New Upgrade Card")]
public class UpgradeCardSO : ScriptableObject
{
    public string cardTitle;
    public Sprite sprite;
    public int cardID;
    public float modifier;
    [HideInInspector] public bool isUsed = true;
    [HideInInspector] public string cardDescription;

}
