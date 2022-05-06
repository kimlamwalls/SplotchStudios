using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public string text;
    AdventureLog log;

    protected override void OnCollect()
    {
        log = GameObject.Find("AdventureLogView").GetComponent<AdventureLog>();
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            log.AddEventMessage(text);
        }
    }
}
