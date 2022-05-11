using System;
using System.Collections;
using System.Threading;
using Cinemachine.Utility;
using UnityEngine;

namespace Altars
{
    public class Altar : MonoBehaviour
    {
        // [SerializeField] private GameObject AltarObject;
        [SerializeField] private GameObject Player;
        [SerializeField] private GameObject[] AltarLights;
        [SerializeField] private int TimeBetweenCandleIgnition = 1;
        [SerializeField] private GameObject progressBar;
        [SerializeField] private ParticleSystem particles;
        
        AdventureLog log;
        private bool displayed;
        private float startTime;
        private bool altarRestored;
        private void Start()
        {
            // turn off all the lights around the altar
            foreach (var l in AltarLights)
            {
                l.GetComponentInChildren<Candle>().LightsOff();
            }
            particles.Stop();

            progressBar.transform.localScale = new Vector3(0, 0.05f);
        }

        void Update()
        {
            if(altarRestored) return;
            
            // calculate distance to player
            var distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);
            // Debug.Log("Distance: " + distance);
            if (distance > 2) return;
            
            // start timer when the key is pressed down
            if (Input.GetKeyDown(KeyCode.R))
            {
                progressBar.transform.localScale = new Vector3(0, 0.05f);
                startTime = Time.time;
                particles.Play();
            }

            if (Input.GetKey(KeyCode.R) && !altarRestored)
            {
                Debug.Log("Restoring altar...");
                progressBar.transform.localScale += new Vector3(0.5f * Time.deltaTime, 0);
                var holdTime = Time.time - startTime;
                if(!displayed)
                {
                    displayed = true;
                    log = GameObject.Find("AdventureLogView").GetComponent<AdventureLog>();
                    log.AddEventMessage("As you touch the altar, the skin on your hand crawls into mysterious shapes.");
                }
                if (holdTime > 3)
                {
                    altarRestored = true;
                    particles.Stop();
                    StartCoroutine(Ignite());
                    log = GameObject.Find("AdventureLogView").GetComponent<AdventureLog>();
                    log.AddEventMessage("A familiar symbol appears on the altar.");
                    log.AddEventMessage("You feel something coursing through you, replenishing your energy");
                    // when altar is restored, reset player health to max and reset sanity to max
                    PlayerMovement.GetInstance().FullHeal();
                    PlayerMovement.GetInstance().FullSanity();
                }
            }
            else
            {
                particles.Stop();
                progressBar.transform.localScale = new Vector3(0, 0.05f);
            }

            // if player is close, prompt them to press and hold the 'R' key to restore
            // while restoring particle effects should surround the player growing in intensity the closer they are?
            // once altar is restored, ignite altar lights to show that it has been completed
        }

        
        /// <summary>
        /// Ignites the candles one by one with a 1 second pause between them
        /// </summary>
        /// <returns></returns>
        IEnumerator Ignite()
        {
            // TODO: fade the progress bar out? looks nicer
            Destroy(progressBar);
            
            foreach (var l in AltarLights)
            {
                l.GetComponentInChildren<Candle>().LightsOn();
                yield return new WaitForSeconds(TimeBetweenCandleIgnition);
            }

            PlayerMovement.AltarsRestored++;
            if (PlayerMovement.AllAltarsRestoredAndHasKey())
            {
                log.AddEventMessage("The grinding sound of a gate opening can be heard echoing through the dungeon");
            }
            yield return null;
        }
        
        
    }
}