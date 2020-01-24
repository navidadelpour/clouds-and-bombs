using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	int triggering;
	bool exitedFromGap;
	// Use this for initialization
	void Start () {
		triggering = 1;
		InvokeRepeating ("incrementTriggering", .1f, .1f);
	}
	
	void OnTriggerStay(Collider other) {
		triggering++;
	}

	void incrementTriggering() {
		if (triggering > 0)
			triggering = 0;
		else
			GameManager.instance.gameOver = true;
	}
}