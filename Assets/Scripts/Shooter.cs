
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
    [SerializeField] float projectileSpacing;
    [SerializeField] float projectileAngle;
    
    [Header("AI")]
    [SerializeField] float AIFiringRate = 2f;
    [SerializeField] float firingRateVariance = 1f;
    [SerializeField] float minimumFiringRate = 2f; 
    [SerializeField] bool useAI;
    
    [HideInInspector]public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    UnitState unitState;

    private void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        unitState = GetComponent<UnitState>();
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
            GameObject instance;
            Rigidbody2D rb;
            Vector3 distance = new Vector3(0.25f, 0, 0);

            switch(unitState.GetCurrentShootingState())
            {
                case UnitState.ShootingState.OneStraight:
                    instance = Instantiate(projectilePrefab, transform.position,
                                                                        Quaternion.identity);
                    
                    //For player damage modifying
                    if(!useAI)
                    {
                        DamageDealer damageDealer = instance.GetComponent<DamageDealer>();
                        damageDealer.IncreaseProjectileDamageMultiflier(StatModifier.instance.GetPlayerProjectileDamageModifier());
                    }

                    rb = instance.GetComponent<Rigidbody2D>();

                    rb.velocity = transform.up * projectileSpeed;

                    Destroy(instance, projectileLifeTime);
                    break;

                case UnitState.ShootingState.TwoStraight:
                    for(int i = 0; i < 2; i++)
                    {
                        instance = Instantiate(projectilePrefab, 
                            transform.position - distance + (2 * i) * distance, Quaternion.identity);

                        //For player damage modifying
                        if(!useAI)
                        {
                            DamageDealer damageDealer = instance.GetComponent<DamageDealer>();
                            damageDealer.IncreaseProjectileDamageMultiflier(StatModifier.instance.GetPlayerProjectileDamageModifier());
                        }
                            
                        rb = instance.GetComponent<Rigidbody2D>();

                        rb.velocity = transform.up * projectileSpeed;

                        Destroy(instance, projectileLifeTime);
                    }
                    break;
                    
                case UnitState.ShootingState.ThreeStraight:
                    for(int i = 0; i < 3; i++)
                    {
                        instance = Instantiate(projectilePrefab, 
                            transform.position - distance + i * distance, Quaternion.identity);

                        //For player damage modifying
                        if(!useAI)
                        {
                            DamageDealer damageDealer = instance.GetComponent<DamageDealer>();
                            damageDealer.IncreaseProjectileDamageMultiflier(StatModifier.instance.GetPlayerProjectileDamageModifier());
                        }

                        rb = instance.GetComponent<Rigidbody2D>();

                        rb.velocity = transform.up * projectileSpeed;

                        Destroy(instance, projectileLifeTime);
                    }
                    break;

                case UnitState.ShootingState.TwoSkewed:
                    for(int i = 0; i < 2; i++)
                    {
                        instance = Instantiate(projectilePrefab, transform.position,
                                                                            Quaternion.identity);

                        //For player damage modifying
                        if(!useAI)
                        {
                            DamageDealer damageDealer = instance.GetComponent<DamageDealer>();
                            damageDealer.IncreaseProjectileDamageMultiflier(StatModifier.instance.GetPlayerProjectileDamageModifier());
                        }

                        rb = instance.GetComponent<Rigidbody2D>();
                        if(i == 0)
                        {
                            Vector3 angle = new Vector3(0.25f, 1, 0);
                            rb.velocity = angle * projectileSpeed;
                        }
                        else
                        {
                            Vector3 angle = new Vector3(-0.25f, 1, 0);
                            rb.velocity = angle * projectileSpeed;
                        }

                        Destroy(instance, projectileLifeTime);
                    }
                    break;

                case UnitState.ShootingState.ThreeSkewed:
                    
                    for(int i = 0; i < 3; i++)
                    {
                        Vector3 angle3 = new Vector3(-0.25f + i * 0.25f, 1, 0);

                        instance = Instantiate(projectilePrefab, transform.position,
                                                                        Quaternion.identity);

                        //For player damage modifying
                        if(!useAI)
                        {
                            DamageDealer damageDealer = instance.GetComponent<DamageDealer>();
                            damageDealer.IncreaseProjectileDamageMultiflier(StatModifier.instance.GetPlayerProjectileDamageModifier());
                        }

                        rb = instance.GetComponent<Rigidbody2D>();

                        rb.velocity = angle3 * projectileSpeed;

                        Destroy(instance, projectileLifeTime);
                    }
                    break;

                default:
                    break;

                
            }
               
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
