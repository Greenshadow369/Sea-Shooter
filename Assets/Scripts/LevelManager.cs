using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    [SerializeField] float sceneTransitionDelay = 2f;
    [SerializeField] Animator sceneTransition;
    AudioPlayer audioPlayer;
    CardManager cardManager;
    ScoreKeeper scoreKeeper;

    private void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cardManager = FindObjectOfType<CardManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        /*if(SceneManager.GetActiveScene().name == "Game")
        {
            if(FindObjectOfType<Player>() == null)
            {
                player.SpawnPlayer();
            }
        }*/
    }

    public void LoadGame()
    {
        /*This solution is found online, which make UseCards() 
        run after scene fully loaded (with objects)*/
        // var op = SceneManager.LoadSceneAsync("Game");
        // op.completed += (x) => {
        //    cardManager.UseCards();
        // };

        StartCoroutine(WaitAndLoad("Game", false, sceneLoadDelay, 
            audioPlayer.PlayGameClip));

        
    }

    public void LoadMainMenu()
    {
        
        StartCoroutine(WaitAndLoad("MainMenu", false, sceneLoadDelay, 
            audioPlayer.PlayMainMenuClip));
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", true, sceneLoadDelay, 
            audioPlayer.PlayGameOverClip));
    }

    public void LoadShop()
    {
        StartCoroutine(WaitAndLoad("Shop", true, sceneLoadDelay, 
            audioPlayer.PlayShopClip));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, bool isTransitionDelayed, float delay, Action changeMusicClip)
    {
        if(isTransitionDelayed)
        {
            yield return new WaitForSeconds(sceneTransitionDelay);
        }

        sceneTransition.SetTrigger("Ending");

        yield return new WaitForSeconds(delay);
        var op = SceneManager.LoadSceneAsync(sceneName);
        op.completed += (x) => {
            if(sceneName == "Game")
            {
                cardManager.UseCards();
            }
        };
        
        changeMusicClip();
    }
}
