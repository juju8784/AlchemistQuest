using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkInWin : MonoBehaviour
{
    public float timer = 100;

    public void Update()
    {
        if(GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false) {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                GetComponent<SphereCollider>().isTrigger = true;
            }
        }
    }

   public void OnTriggerEnter(Collider other)
   {
        if(other.CompareTag("Player"))
            SceneManager.LoadScene("Win");
   }
}
