using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MUSIC_VOLUME_KEY = "music volume";
    const string SFX_VOLUME_KEY = "sfx volume";
    const string DIFFICULTY_KEY = "difficulty";

    const float MIN_VOLUME = 0f, MAX_VALUE = 1f;
    const int MIN_DIFFICULTY = 1, MAX_DIFFICULTY = 3;

    public static void SetMusicVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VALUE)
        {
            Debug.Log("Music volume set to " + volume);
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Music volume is out of range");
        }        
    }

    public static float GetMusicVolume()
    {

        return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
    }

    public static void SetSFXVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VALUE)
        {            
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
            Debug.Log("SFX volume set to " + volume);
        }
        else
        {
            Debug.LogError("SFX volume is out of range");
        }
    }

    public static float GetSFXVolume()
    {

        return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
    }

    public static void SetDifficulty(int difficulty)
    {
        if (difficulty >= MIN_DIFFICULTY && difficulty <= MAX_DIFFICULTY)
        {
            Debug.Log("Difficulty set to " + difficulty);
            PlayerPrefs.SetInt(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Difficulty is out of range");
        }
    }

    public static int GetDifficulty()
    {

        return PlayerPrefs.GetInt(DIFFICULTY_KEY);
    }
}
