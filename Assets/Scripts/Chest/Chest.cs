using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public string text;
    [SerializeField] private bool containsKey = false;
    AdventureLog log;

    protected override void OnCollect()
    {
        log = GameObject.Find("AdventureLogView").GetComponent<AdventureLog>();
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            log.AddEventMessage(text);
            
            if (!containsKey) return;
            
            PlayerMovement.HasKey = true;
            if (PlayerMovement.AllAltarsRestoredAndHasKey())
            {
                log.AddEventMessage("The grinding sound of a gate opening can be heard echoing through the dungeon");
            }
            
            
        }
    }
}
