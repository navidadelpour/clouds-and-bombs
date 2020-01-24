using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplode : MonoBehaviour {

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
			AudioManager.instance.bomb ();
			explode ();
			if (gameManager.hasHealth != true) {
				gameManager.gameOver = true;
				GameObject player = GameObject.Find ("Player");
				for (int i = 0; i < player.transform.GetChild (0).childCount; i++) {
					player.transform.GetChild (0).GetChild (i).gameObject.AddComponent<BoxCollider> ();
					player.transform.GetChild (0).GetChild (i).gameObject.GetComponent<Rigidbody> ().isKinematic = false;
				}
			}
		}
	}

	public void explode() {

		transform.position += Vector3.up * transform.localScale.y;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).gameObject.AddComponent<BoxCollider> ();
			transform.GetChild (i).GetComponent<Rigidbody> ().isKinematic = false;
			transform.GetChild (i).GetComponent<Rigidbody> ().useGravity = false;

		}
	}


}
