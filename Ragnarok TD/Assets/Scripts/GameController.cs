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
    int numberOfAttackers = 0;
    bool levelTimerFinished = false;
    [SerializeField] AudioClip[] gameEndSFX;
    float gameEndSFXvolume;
    public bool gameOver = false;
    TimeController timeController;

    [Header("Scene Loading settings")]
    [SerializeField] Button startGameButton = null;
    [SerializeField] Button optionsButton = null;
    [SerializeField] Button mainMenuButton = null;
    [SerializeField] Button tryAgainButton = null;
    [SerializeField] Button quitGameButton = null;
    int currentSceneIndex;

    private void Awake()
    {
        if (winLabel) { winLabel.SetActive(false); }
        if (loseLabel) { loseLabel.SetActive(false); }
        if (slider) { slider.SetActive(false); }

    }

    private void Start()
    {
        CheckButtons();        
        gameEndSFXvolume = PlayerPrefsController.GetSFXVolume();        
        if (!slider) { return; }
        timeController = FindObjectOfType<TimeController>();
        timeController.StartGame();
        StartCoroutine(WaitStartGame()); // begin game
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            QuitGame();
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
        if (numberOfAttackers <= 0 && levelTimerFinished)
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
        //timeController.StopGame();
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

    //Scene Loading settings procedures

    private void CheckButtons()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(SplashScreenLoad());
        }
        if (startGameButton)
        {
            startGameButton.onClick.AddListener(() => { SceneManager.LoadScene("04. Level 1"); });
        }
        if (optionsButton)
        {
            optionsButton.onClick.AddListener(() => { SceneManager.LoadScene("03. Options Screen"); });
        }
        if (mainMenuButton)
        {
            mainMenuButton.onClick.AddListener(() => LoadMainMenu());
        }
        if (tryAgainButton)
        {
            tryAgainButton.onClick.AddListener(() => { SceneManager.LoadScene(currentSceneIndex); }); //retry same scene
        }
        if (quitGameButton)
        {
            quitGameButton.onClick.AddListener(() => QuitGame());
        }
    }
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
        SceneManager.LoadScene("02. Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
