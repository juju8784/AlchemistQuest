using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public float health = 100;
    public int lives;
    public GameObject dieEffect;
    [SerializeField] GameObject spawn = null;
    [SerializeField] GameObject livesText = null;
    public bool dead = false;
    private Animator anim;
    private float animSpeed;
    private PlayerMovement3D pc;

    public void Start()
    {
        GameManager.instance.playerHealth.maxHealth = health;
        GameManager.instance.playerHealth.SetHealth(health);
        if (!GameManager.instance.playerManager)
            GameManager.instance.playerManager = this;
        dead = false;
        anim = GetComponent<Animator>();
        animSpeed = anim.speed; 
        anim.SetBool("Idle", true);
        anim.SetBool("Walking", false);
        pc = GetComponent<PlayerMovement3D>();
    }

    public void TakeDamage(float damage)
    {

        GameManager.instance.playerHealth.SetHealth(GameManager.instance.playerHealth.GetHealth() - damage);
        if (GameManager.instance.playerHealth.GetHealth() <= 0 && dead == false) 
        {
            StartCoroutine(Die());
        }
    }
    private void Update()
    {
        if (GameManager.instance.paused && GameManager.instance.playerManager.dead == false)
        {
            anim.speed = 0;
            anim.SetBool("Dead", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Walking", false);
        }
        else
            anim.speed = animSpeed;
    }

    public IEnumerator Die()
    {
        lives--;
        livesText.GetComponent<TextMeshProUGUI>().SetText(lives.ToString());
        dead = true;
        anim.SetBool("Dead", true);
        anim.SetBool("Idle", false); 
        anim.SetBool("Walking", false);

        pc.ResetMoveSpeed();
        yield return new WaitForSeconds(4);
        if (lives >= 0)
        {
            // when map finished, change this to just reload the scene
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.instance.playerHealth.SetHealth(health);
            GameManager.instance.player.transform.position = spawn.transform.position;
            //GameManager.instance.ResetEnemyPosition();
            anim.SetBool("Dead", false);
            anim.SetBool("Idle", true);
            anim.SetBool("Walking", false);
            dead = false;
        }
        else
        {
            SceneManager.LoadScene("Lose");
        }
    }
}
