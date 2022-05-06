using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	public GameObject portal;
	private GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player") 
		{
			player.transform.position = new Vector2 (portal.transform.position.x, portal.transform.position.y);
		}
	}
}