using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void  StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    // navigate from credits to start menu
    public void BackToStartMenu()
    {

    }
}
