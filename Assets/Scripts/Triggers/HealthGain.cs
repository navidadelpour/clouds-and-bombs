using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGain : MonoBehaviour {

	GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			AudioManager.instance.health ();
			Destroy (this.gameObject);
			if(gameManager.hasHealth)
				gameManager.health = 20 * 23;
			gameManager.hasHealth = true;
		}
	}
}
