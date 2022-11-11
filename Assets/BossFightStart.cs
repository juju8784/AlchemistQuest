using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossFightStart : MonoBehaviour
{
    GameObject[] BossHealthBars;
    [SerializeField] AudioSource bossFightSong = null;
    [SerializeField] GameObject musicVolumeSlider = null;
    [SerializeField] GameObject regularMusic = null;

    private void Start()
    {
        BossHealthBars = GameObject.FindGameObjectsWithTag("BossBar");
        foreach (var item in BossHealthBars)
        {
            item.SetActive(false);
        }
        bossFightSong.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
    private void Update()
    {
        bossFightSong.volume = musicVolumeSlider.GetComponent<Slider>().value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var item in BossHealthBars)
            {
                item.SetActive(true);
            }
            bossFightSong.Play();
            regularMusic.GetComponent<AudioSource>().Stop();
            GetComponent<Collider>().enabled = false;
        }
    }
}
