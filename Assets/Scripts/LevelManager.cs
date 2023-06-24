using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
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
        var op = SceneManager.LoadSceneAsync("Game");
        op.completed += (x) => {
            cardManager.UseCards();
        };

        audioPlayer.PlayGameClip();
        
    }

    public void LoadMainMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
        audioPlayer.PlayMainMenuClip();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay, 
            audioPlayer.PlayGameOverClip));
    }

    public void LoadShop()
    {
        StartCoroutine(WaitAndLoad("Shop", sceneLoadDelay, 
            audioPlayer.PlayShopClip));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay, Action changeMusicClip)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
        changeMusicClip();
    }
}
