using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	GameManager gameManager;
	GameObject player;
	float rate = .5f;
	public Vector3 offset;
	Vector3 newOffset;
	bool shouldSwipe;
	public Transform initial;
	CameraViewportHandler handler;
	public bool needInitial;
	public bool shouldStart;

	// Use this for initialization
	void Start () {
		shouldSwipe = true;
		gameManager = GameManager.instance;
		player = GameObject.Find ("Player");
		offset = player.transform.position - transform.position;
		initial = transform;
		handler = GetComponent<CameraViewportHandler> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (handler.activeState == States.DEFUALT) {
			if (needInitial) {
				transform.position = initial.position;
				transform.eulerAngles = new Vector3(30, 45, 0);
				needInitial = false;
			}

			if (gameManager.started && shouldSwipe) {
				offset = offset + Vector3.down * 5 + Vector3.right * 2;
				shouldSwipe = false;
			}

			if (player.GetComponent<Rigidbody> ().velocity.x > 0)
				newOffset = Vector3.right * 4;
			else if (player.GetComponent<Rigidbody> ().velocity.z > 0)
				newOffset = Vector3.forward * 4;

			if (rate < 3)
				Invoke ("increaseRate", .1f);

			transform.position = Vector3.Lerp (transform.position, player.transform.position - offset + newOffset, rate * Time.deltaTime);
		}

	}

	void increaseRate() {
		rate += .01f;
	}
}
