
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float firingRate = 0.2f;
    [SerializeField] Transform shootingPoint;
    
    [Header("AI")]
    [SerializeField] float AIFiringRate = 2f;
    [SerializeField] float firingRateVariance = 1f;
    [SerializeField] float minimumFiringRate = 2f; 
    [SerializeField] bool useAI;
    
    [HideInInspector]public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start() {
        if(useAI)
        {
            isFiring = true;
        }
    }

    private void Update() {
        Fire();
    }

    private void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            if(projectilePrefab != null)
            {
                firingCoroutine = StartCoroutine(FireContinuously());
            }
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position,
                                                Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifeTime);
            if(useAI)
            {
                firingRate = GetRandomFiringRate();
            }
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(firingRate);
        }
        
    }

    public float GetRandomFiringRate()
    {
        float randomFiringRate = Random.Range(AIFiringRate - firingRateVariance,
                                        AIFiringRate + firingRateVariance);
        return Mathf.Clamp(randomFiringRate, minimumFiringRate, float.MaxValue);
    }
}
