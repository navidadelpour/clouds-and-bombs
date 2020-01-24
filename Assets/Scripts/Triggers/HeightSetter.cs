using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			GameManager.instance.jumpHeight += 1f;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			Invoke ("addJumpHeight", .3f);
			GameManager.instance.groundHeight += 2f;
		}
	}

	void addJumpHeight() {
		GameManager.instance.jumpHeight += 1f;

	}
}
