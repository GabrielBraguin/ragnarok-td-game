using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Game settings")]
    [SerializeField] GameObject slider;
    [Tooltip("Level timer in seconds")]
    [SerializeField] public int gameTimer = 10;
    [Tooltip("Delay before wave begin spawning")]
    [SerializeField] public int spawnDelay = 3;
    [SerializeField] public int playerHealth = 5;
    [SerializeField] public GameObject [] playerHeart;
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] GameObject optionsLabel;
    [SerializeField] int numberOfAttackers = 0;
    public bool levelTimerFinished = false;
    [SerializeField] AudioClip[] gameEndSFX;
    float gameEndSFXvolume;
    public bool gameOver = false;
    TimeController timeController;
    int currentSceneIndex;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;

    private void Awake()
    {
        if (winLabel) { winLabel.SetActive(false); }
        if (loseLabel) { loseLabel.SetActive(false); }
        if (slider) { slider.SetActive(false); }

    }

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(SplashScreenLoad());
        }
        if (!slider) { return; }
        timeController = FindObjectOfType<TimeController>();
        timeController.StartGame();
        StartCoroutine(WaitStartGame()); // begin game
    }

    private void Update()
    {
        gameEndSFXvolume = PlayerPrefsController.GetSFXVolume();
        if (Input.GetKey("escape"))
        {
            if (slider)
            {
                timeController.StopGame();
                optionsLabel.SetActive(true);
            }
            else
            { 
                QuitGame();
            }
        }
        if (playerHealth <= 0)
        {
            if (gameOver) { return; }
            StartCoroutine(HandleLoseCondition());            
        }
    }

    //Game settings procedures

    IEnumerator WaitStartGame()
    {
        yield return new WaitForSeconds(spawnDelay);
        slider.SetActive(true);
        FindObjectOfType<GameTimer>().gameStarted = true;
        FindObjectOfType<GameTimer>().BeginSlider();
        AttackerSpawner[] spawnerArray = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawnerArray)
        {
            StartCoroutine(spawner.StartWave());
        }
    }

    public void AttackerSpawned()
    {
        numberOfAttackers++;
    }

    public void AttackerKilled()
    {
        numberOfAttackers--;
        if (numberOfAttackers <= 0 && levelTimerFinished && playerHealth > 0)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        gameOver = true;
        winLabel.SetActive(true);
        slider.SetActive(false);
        GetComponent<AudioSource>().Stop();
        AudioSource.PlayClipAtPoint(gameEndSFX[1], Camera.main.transform.position, gameEndSFXvolume);
        yield return new WaitForSeconds(3);
        StartCoroutine(LoadNextScene());
    }

    IEnumerator HandleLoseCondition()
    {
        gameOver = true;        
        loseLabel.SetActive(true);
        GetComponent<AudioSource>().Stop();
        AudioSource.PlayClipAtPoint(gameEndSFX[0], Camera.main.transform.position, gameEndSFXvolume);
        yield return new WaitForSeconds(1);
        timeController.StopGame();
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawnerArray = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }

    public void ResumeGame()
    {
        timeController.StartGame();
        optionsLabel.SetActive(false);
        timeController.SetNormalSpeedButton();
    }

    //Scene Loading settings procedures

    IEnumerator SplashScreenLoad()
    {
        yield return new WaitForSeconds(4);
        LoadMainMenu();
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadMainMenu()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        SceneManager.LoadScene("02. Main Menu");
    }

    public void LoadOptionsScreen()
    {
        SceneManager.LoadScene("03. Options Screen");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("04. Level 1");
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
