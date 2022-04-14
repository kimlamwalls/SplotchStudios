using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int goldCoins = 5;
    AdventureLog log;

    protected override void OnCollect()
    {
        log = GameObject.Find("AdventureLogView").GetComponent<AdventureLog>();
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            log.AddEventMessage("You have gained " + goldCoins + " gold coins!");
        }
    }
}
