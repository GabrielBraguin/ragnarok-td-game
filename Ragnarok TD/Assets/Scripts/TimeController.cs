using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimeController : MonoBehaviour
{
    [SerializeField] GameObject timeButton;

    public void Start()
    {
        EventSystem.current.SetSelectedGameObject(null); //clear any previous selection (best practice)
        EventSystem.current.SetSelectedGameObject(timeButton); //selection the button you dropped in the inspector
    }

    public void SetSpeed(float speed)
    {
        Time.timeScale = speed;
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }

}
