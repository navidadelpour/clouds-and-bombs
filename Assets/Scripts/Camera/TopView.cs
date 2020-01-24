using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopView : MonoBehaviour {

	public bool inited;
	CameraViewportHandler handler;
	float rate = .5f;
	public Vector3 offset;
	public Vector3 newOffset;
	GameObject player;
	public int x;
	public int z;

	// Use this for initialization
	void Start () {
		handler = GetComponent<CameraViewportHandler> ();
		player = GameObject.Find ("Player");

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (handler.activeState == States.FROM_TOP) {
			if (!inited) {
				transform.position = new Vector3 (-5, 10, -5);
				transform.eulerAngles = new Vector3 (60, 45, 0);
				inited = true;
				offset = player.transform.position - transform.position;
			}
			if (player.GetComponent<Rigidbody> ().velocity.x > 0)
				newOffset = Vector3.right * 25 + Vector3.back * 0;
			else if (player.GetComponent<Rigidbody> ().velocity.z > 0)
				newOffset = Vector3.left * 0 + Vector3.forward * 25;
		
			transform.position = Vector3.Lerp (transform.position, player.transform.position - offset + newOffset, rate * Time.deltaTime); 
		}

	}
}
