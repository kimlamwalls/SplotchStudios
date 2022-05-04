using System;
using System.Collections;
using System.Threading;
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
        
        private float startTime;
        private bool altarRestored;
        private void Start()
        {
            // turn off all the lights around the altar
            foreach (var l in AltarLights)
            {
                l.GetComponentInChildren<Candle>().LightsOff();
            }

            progressBar.transform.localScale = new Vector3(0, 0.05f);
        }

        void Update()
        {
            if(altarRestored) return;
            
            // calculate distance to player
            var distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);
            if (distance > 3) return;
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                progressBar.transform.localScale = new Vector3(0, 0.05f);
                startTime = Time.time;
            }

            if (Input.GetKey(KeyCode.R) && !altarRestored)
            {
                progressBar.transform.localScale += new Vector3(1f * Time.deltaTime, 0);
                var holdTime = Time.time - startTime;
                if (holdTime > 3)
                {
                    altarRestored = true;
                    StartCoroutine(Ignite());
                }
                
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
            yield return null;
        }
        
        
    }
}