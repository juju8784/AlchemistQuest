using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Manages the music
public class MusicManager : MonoBehaviour
{
    public GameObject musicVolumeSlider;
    public GameObject backgroundMusic;
    public GameObject sfxVolumeSlider;
    public GameObject sfxExample;


    private void Start()
    {
        backgroundMusic.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
        //sfxExample.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
        GameObject[] sfx = GameObject.FindGameObjectsWithTag("SFX");
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
        }
        musicVolumeSlider.GetComponent<Slider>().value = backgroundMusic.GetComponent<AudioSource>().volume;
        sfxVolumeSlider.GetComponent<Slider>().value = sfxExample.GetComponent<AudioSource>().volume;
    }
    void Update()
    {
        backgroundMusic.GetComponent<AudioSource>().volume = musicVolumeSlider.GetComponent<Slider>().value;
        sfxExample.GetComponent<AudioSource>().volume = sfxVolumeSlider.GetComponent<Slider>().value;
    }

    public void SaveOptions()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.GetComponent<Slider>().value);
        PlayerPrefs.Save();
        GameObject[] sfx = GameObject.FindGameObjectsWithTag("SFX");
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXVolume");
        }
    }
}