using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    [SerializeField] Player player;
    ScoreKeeper scoreKeeper;
    AudioPlayer audioPlayer;
    public delegate void OnGameLoad();
    public event OnGameLoad onGameLoad;
    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start() {
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
        scoreKeeper.ResetScore();
        scoreKeeper.ResetLevel();
        Debug.Log("game load");
        SceneManager.LoadScene("Game");
        
        PrepareGame();
        audioPlayer.PlayGameClip();
        
    }

    public void LoadMainMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
        audioPlayer.PlayMainMenuClip();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
        audioPlayer.PlayGameOverClip();
    }

    public void LoadShop()
    {
        StartCoroutine(WaitAndLoad("Shop", sceneLoadDelay));
        audioPlayer.PlayShopClip();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    private void PrepareGame()
    {
        if(onGameLoad != null)
        {
            onGameLoad();
        }
    }
}
