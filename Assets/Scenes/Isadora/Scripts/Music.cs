using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public GameObject backgroundMusic;
    private void Start()
    {
        backgroundMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
        //sfxExample.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
        GameObject[] sfx = GameObject.FindGameObjectsWithTag("SFX");
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

}
