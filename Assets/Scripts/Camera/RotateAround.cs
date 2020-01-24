using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

	GameObject player;
	float radius;
	float rotateSpeed;
	float position;
	CameraViewportHandler handler;
	float height;
	float heightToSet;
	float lastheight;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		rotateSpeed = 10f;
		position = 0;
		radius = (player.transform.position - transform.position).magnitude;
		handler = GetComponent<CameraViewportHandler> ();
		if (handler.activeState == States.ROTATE_AROUND) {
			transform.position += Vector3.up * 20;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (handler.activeState == States.ROTATE_AROUND) {
			height = GameManager.instance.groundHeight;
			if (height > lastheight) {
				lastheight = height;
				heightToSet = 2;
			} else {
				heightToSet = 0;
			}
			transform.position = new Vector3 (
				player.transform.position.x + radius * Mathf.Sin (Mathf.Deg2Rad * position), 
				transform.position.y + heightToSet ,
				player.transform.position.z - radius * Mathf.Cos (Mathf.Deg2Rad * position)
			);
			transform.LookAt (player.transform.position + Vector3.up * 2, Vector3.up);
			//transform.localRotation = Quaternion.Euler(90, transform.localRotation.eulerAngles.y, 0);
			position += Time.deltaTime * rotateSpeed;

		}
	}

}
