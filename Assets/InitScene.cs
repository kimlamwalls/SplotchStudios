using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour
{

    GameObject musicObject;
    FMODUnity.StudioEventEmitter emitter;
    private FMODUnity.StudioEventEmitter menuMusic ;

    
    // Start is called before the first frame update
    void Start()
    {
        musicObject = GameObject.Find("LoadMenuMusic");
        emitter = musicObject.GetComponent<FMODUnity.StudioEventEmitter>();
        emitter.StopInstance();
        //Destroy(musicObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
