using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;
    [SerializeField] Slider difficultySlider;
    [SerializeField] GameObject[] difficultyLevel;
    float defaultMusicVolume = 0.5f, defaultSFXVolume = 0.5f;
    int defaultDifficulty = 2;
    public TextMeshProUGUI textToChange;
    TMPro.TextMeshProUGUI easyHeader, normalHeader, hardHeader;

    void Start()
    {
        musicVolumeSlider.value = PlayerPrefsController.GetMusicVolume();
        SFXVolumeSlider.value = PlayerPrefsController.GetSFXVolume();
        difficultySlider.value = PlayerPrefsController.GetDifficulty();
        easyHeader = difficultyLevel[0].GetComponent<TextMeshProUGUI>();
        normalHeader = difficultyLevel[1].GetComponent<TextMeshProUGUI>();
        hardHeader = difficultyLevel[2].GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {        
        var musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer)
        {
            musicPlayer.SetVolume(musicVolumeSlider.value);
        }
        else
        {
            Debug.LogWarning("No music player found.");
        }

        switch (difficultySlider.value)
        {
            case 1:
                easyHeader.color = new Color32(39, 255, 0, 255);
                normalHeader.color = new Color32(241, 244, 0, 128);
                hardHeader.color = new Color32(255, 51, 0, 128);
                break;
            case 2:
                easyHeader.color = new Color32(39, 255, 0, 128);
                normalHeader.color = new Color32(241, 244, 0, 255);
                hardHeader.color = new Color32(255, 51, 0, 128);
                break;
            case 3:
                easyHeader.color = new Color32(39, 255, 0, 128);
                normalHeader.color = new Color32(241, 244, 0, 128);
                hardHeader.color = new Color32(255, 51, 0, 255);
                break;
            default:
                break;
        }
    }

    public void SaveAndExit()
    {
        int difficultySliderValue = (int) difficultySlider.value;
        PlayerPrefsController.SetMusicVolume(musicVolumeSlider.value);
        PlayerPrefsController.SetSFXVolume(SFXVolumeSlider.value);
        PlayerPrefsController.SetDifficulty(difficultySliderValue);
        FindObjectOfType<GameController>().LoadMainMenu();
    }

    public void SetDefaults()
    {
        musicVolumeSlider.value = defaultMusicVolume;
        SFXVolumeSlider.value = defaultSFXVolume;
        difficultySlider.value = defaultDifficulty;
    }
}
