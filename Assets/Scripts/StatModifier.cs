using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier : MonoBehaviour
{
    public static StatModifier instance;
    private float playerSpeedModifier;

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
}
