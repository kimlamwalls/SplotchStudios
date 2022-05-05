using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenFade : MonoBehaviour

{
    public float timeToWait = 4;

    private void Start()
    {
        StartCoroutine(LoadMenuAfterSeconds(timeToWait));
        
    }

    private void Update()
    {

    }

    IEnumerator LoadMenuAfterSeconds(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene("StartMenu");
    }
}

