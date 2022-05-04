using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EventTrigger : MonoBehaviour
{

    public FMODUnity.StudioEventEmitter PaperSound;
    private int flipSoundSwitch = 0;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private bool triggerFired;

    private void Awake()
    {
        playerInRange = false;
        triggerFired = false;
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && !triggerFired)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            PaperSound.Play();
            flipSound();
            PaperSound.SetParameter("Paper Sound", flipSoundSwitch);
            triggerFired = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void flipSound()
    {
        if (flipSoundSwitch == 0)
        {
            flipSoundSwitch = 1;
        };
        if (flipSoundSwitch == 1)
        {
            flipSoundSwitch = 0;
        }
    }
}