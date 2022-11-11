using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject player;
    public PlayerMovement3D playerScript;
    private GameObject MainCanvas;
    public InventoryManager Inventory;
    public QueueManagment Queue;
    public HealthBarManager playerHealth;
    public PlayerManager playerManager;
    public GameObject[] enemies;
    public bool paused;
    public List<GameObject> HitByLightning;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject ControlsMenu;
    public GameObject EnemyHealthBars;

    private void Awake()
    {
        instance = this;
        instance.player = GameObject.FindWithTag("Player");
        instance.playerScript = player.GetComponent<PlayerMovement3D>();
        instance.MainCanvas = GameObject.Find("Main Canvas");
        instance.EnemyHealthBars = GameObject.Find("EnemyHealthBars");
        instance.Inventory = GameObject.Find("Inventory").GetComponent<InventoryManager>();
        instance.Queue = GameObject.Find("Queue").GetComponent<QueueManagment>();
        instance.playerHealth = GameObject.Find("PlayerHealthBar").GetComponent<HealthBarManager>();
        instance.playerManager = player.GetComponent<PlayerManager>();
        paused = false;
    }

    public void Refind()
    {
        instance.player = GameObject.FindWithTag("Player");
        instance.playerScript = player.GetComponent<PlayerMovement3D>();
        instance.MainCanvas = GameObject.Find("Main Canvas");
        instance.EnemyHealthBars = GameObject.Find("EnemyHealthBars");
        instance.Inventory = GameObject.Find("Inventory").GetComponent<InventoryManager>();
        instance.Queue = GameObject.Find("Queue").GetComponent<QueueManagment>();
        instance.playerHealth = GameObject.Find("PlayerHealthBar").GetComponent<HealthBarManager>();
        instance.playerManager = player.GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu!=null)
        {
            if (paused == false)
            {
                PauseGame();
                pauseMenu.gameObject.SetActive(true);
                

            }
            else
            {
                PauseGame();
                
                pauseMenu.gameObject.SetActive(false);
                optionsMenu.gameObject.SetActive(false);
                ControlsMenu.gameObject.SetActive(false);
            }
        }
    }

    public void ResetHitByLightning()
    {
        for (int i = 0; i < HitByLightning.Count; i++)
        {
            HitByLightning[i] = null;
        }
    }

    //public void ResetEnemyPosition()
    //{
    //    enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //    if (enemies != null)
    //    {
    //        for (int i = 0; i < enemies.Length; i++)
    //        {
    //            enemies[i].GetComponent<BaseEnemyController>().ResetPosition();
    //        }
    //    }
    //}

    public void PauseGame()
    {
        paused = !paused;
    }

   
}
