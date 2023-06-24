using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage = 10;

    public float GetDamage()
    {
        return damage;
    }

    public void IncreaseProjectileDamageMultiflier(float modifier)
    {
        damage = damage * modifier;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
