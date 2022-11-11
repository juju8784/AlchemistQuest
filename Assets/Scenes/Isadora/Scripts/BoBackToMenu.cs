using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoBackToMenu : MonoBehaviour
{
    
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
