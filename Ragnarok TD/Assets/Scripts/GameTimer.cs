using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    bool triggeredLevelFinished = false;
    public bool gameStarted = false;
    float levelTime;
    float sliderBeginTime;

    private void Start()
    {
        levelTime = FindObjectOfType<GameController>().gameTimer;
    }

    public void BeginSlider()
    {
        sliderBeginTime = Time.time;
    }

    void Update()
    {
        if (!gameStarted) { return; }
        if (triggeredLevelFinished) { return; }
        GetComponent<Slider>().value = (Time.time - sliderBeginTime)  / levelTime;


        bool timerFinished = (Time.time - sliderBeginTime >= levelTime);
        if (timerFinished)
        {
            FindObjectOfType<GameController>().LevelTimerFinished();
            triggeredLevelFinished = true;
        }
    }

}
