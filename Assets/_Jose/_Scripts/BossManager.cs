using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public List<TwinHeadBossController> heads;

    // Update is called once per frame
    void Update()
    {
        foreach (TwinHeadBossController head in heads.ToArray())
        {
            if (head.enemyHealth <= 0)
                heads.Remove(head);
        }

        if (heads.Count <= 0)
        {
            SceneManager.LoadScene("Win");
            //SceneManager.LoadScene("Level2_Snow");
        }
    }
}
