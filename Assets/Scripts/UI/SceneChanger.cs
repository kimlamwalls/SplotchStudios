using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour

{
    public Animator transition;
    public float transitionTime = 1.5f;
    public void StartGame()
    {
        LoadLevelWithFade("Main");
    }

    public void SplashScreenRestart()
    {
        LoadLevelWithFade("LoadMainMenu");
    }

    public void CreditsScene()
    {
        LoadLevelWithFade("Credits");
    }

    // navigate from credits to start menu
    public void BackToStartMenu()
    {
        LoadLevelWithFade("StartMenu");
    }

    public void DeathScene()
    {
        LoadLevelWithFade("Death");
    }

    public void HowToPlay()
    {
        LoadLevelWithFade("Controls");
    }

    public void LoadLevelWithFade(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }

    IEnumerator LoadLevel(string levelName)
    {
        //play animation
        transition.SetTrigger("Start");
        
        //WaitUntil
        yield return new WaitForSeconds(transitionTime);
        
        //load scene 
        SceneManager.LoadScene(levelName);
    }
}
