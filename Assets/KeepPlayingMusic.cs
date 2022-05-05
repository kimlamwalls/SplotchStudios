using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KeepPlayingMusic : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("StartMenu");
    }
    



}
