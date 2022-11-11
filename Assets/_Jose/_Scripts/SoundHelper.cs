using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHelper : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.GetInt("GameLoaded") == 1)
            return;
        PlayerPrefs.SetFloat("MusicVolume", 100);
        PlayerPrefs.SetFloat("SFXVolume", 100);
        PlayerPrefs.SetInt("GameLoaded", 1);
    }
}
