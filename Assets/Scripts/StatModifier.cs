using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier : MonoBehaviour
{
    public static StatModifier instance;
    private float playerSpeedModifier;
    private float playerMaxHealthModifier;
    private float playerProjectileDamageModifier;

    private void Awake() {
        ManageSingleton();
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

    public void IncreasePlayerSpeedModifier(float modifier)
    {
        playerSpeedModifier += modifier;
    }

    public float GetPlayerSpeedModifier()
    {
        return playerSpeedModifier / 100 + 1;
    }

    public void ResetPlayerSpeedModifier()
    {
        playerSpeedModifier = 0;
    }

    public void IncreasePlayerMaxHealthModifier(float modifier)
    {
        playerMaxHealthModifier += modifier;
    }

    public float GetPlayerMaxHealthModifier()
    {
        return playerMaxHealthModifier / 100 + 1;
    }

    public void ResetPlayerMaxHealthModifier()
    {
        playerMaxHealthModifier = 0;
    }

    public void IncreasePlayerProjectileDamageModifier(float modifier)
    {
        playerMaxHealthModifier += modifier;
    }

    public float GetPlayerProjectileDamageModifier()
    {
        return playerMaxHealthModifier / 100 + 1;
    }

    public void ResetPlayerProjectileDamageModifier()
    {
        playerMaxHealthModifier = 0;
    }
}
