using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 100;
    [SerializeField] float scorePoint = 1f;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    VisualFX visualFX;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    
    private void Awake() {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start() {
        visualFX = FindObjectOfType<VisualFX>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if(damageDealer != null)
        {
            damageDealer.Hit();
            visualFX.PlayHitEffect(transform);
            audioPlayer.PlayHittingClip();
            ShakeCamera();
            TakeDamage(damageDealer.GetDamage());
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        
        if(health <= 0)
        {
            Die();
            
        }
    }

    private void Die()
    {
        visualFX.PlayDieEffect(transform);
        audioPlayer.PlayDefeatedClip();
        Destroy(gameObject);
        if(!isPlayer)
        {
            scoreKeeper.IncreaseScore(scorePoint);
        }
        else
        {
            levelManager.LoadGameOver();
        }
    }

    private void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public float GetHealth()
    {
        return health;
    }
}
