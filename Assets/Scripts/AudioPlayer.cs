using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;
    
    
    [Header("Hitting")]
    [SerializeField] AudioClip hittingClip;
    [SerializeField] [Range(0f, 1f)] float hittingVolume = 1f;

    [Header("Defeated")]
    [SerializeField] AudioClip defeatedClip;
    [SerializeField] [Range(0f, 1f)] float defeatedVolume = 1f;

    [Header("Background")]
    [SerializeField] AudioClip mainMenuClip;
    [SerializeField] [Range(0f, 1f)] float mainMenuVolume = 1f;
    
    [SerializeField] AudioClip gameClip;
    [SerializeField] [Range(0f, 1f)] float gameVolume = 1f;

    [SerializeField] AudioClip shopClip;
    [SerializeField] [Range(0f, 1f)] float shopVolume = 1f;

    [SerializeField] AudioClip gameOverClip;
    [SerializeField] [Range(0f, 1f)] float gameOverVolume = 1f;

    static AudioPlayer instance;
    AudioSource audioSource;

    private void Awake() {
        ManageSingleton();
        audioSource = FindObjectOfType<AudioSource>();
    }

    private void Start()
    {
        PlayMainMenuClip();
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

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayHittingClip()
    {
        PlayClip(hittingClip, hittingVolume);
    }

    public void PlayDefeatedClip()
    {
        PlayClip(defeatedClip, defeatedVolume);
    }

    public void PlayMainMenuClip()
    {
        PlayBackgroundClip(mainMenuClip, mainMenuVolume);
    }

    public void PlayGameClip()
    {
        PlayBackgroundClip(gameClip, gameVolume);
    }

    public void PlayShopClip()
    {
        PlayBackgroundClip(shopClip, shopVolume);
    }

    public void PlayGameOverClip()
    {
        PlayBackgroundClip(gameOverClip, gameOverVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

    private void PlayBackgroundClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = null;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}
