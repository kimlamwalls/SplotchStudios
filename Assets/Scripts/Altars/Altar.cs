using System;
using System.Collections;
using System.Threading;
using Cinemachine.Utility;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
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
            Debug.Log("Distance: " + distance);
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
                if (holdTime > 3)
                {
                    altarRestored = true;
                    particles.Stop();
                    StartCoroutine(Ignite());
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
            yield return null;
        }
        
        
    }
}