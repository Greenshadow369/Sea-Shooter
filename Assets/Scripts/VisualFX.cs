using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFX : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem dieEffect;

    public void PlayHitEffect(Transform position)
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, position.position, Quaternion.identity);
            //instance.transform.SetParent(position); (effect will follow character but disappear when it die)
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public void PlayDieEffect(Transform position)
    {
        if(dieEffect != null)
        {
            ParticleSystem instance = Instantiate(dieEffect, position.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}
