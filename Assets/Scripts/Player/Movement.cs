using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	GameManager gameManager;
	public float speed;
	public float jumpSpeed;
	public float jumpHeight;
	char direction;
	GameObject Camera;
	Rigidbody rigidBody;
	public bool jumping;
	public bool falling;
	float height;
	public bool shouldChangeDirection;
	public bool shouldJump;
	public bool canJump;
	public bool canDir;
	float jumpThreshold;
	public float ts;
	float tsRate;
	public float timeScaleShow;

	// Use this for initialization
	void Start () {
		gameManager = GameManager.instance;
		speed = 9f;
		jumpSpeed = 3.5f;
		jumpHeight = 2f;
		jumpThreshold = .3f;
		height = transform.position.y;
		rigidBody = GetComponent<Rigidbody> ();
		Camera = GameObject.Find ("Main Camera");
		canJump = false;
		canDir = true;
		tsRate = .05f;
		ts = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.gameOver) {
			rigidBody.useGravity = true;
			fall ();
			if(Camera)
				Camera.GetComponent<CameraFollow> ().enabled = false;
			return;
		}


		if (!canJump) {
			Invoke ("canJumpTrue", .2f);
			shouldJump = false;
		}
		
		if ((shouldChangeDirection || Input.GetMouseButtonDown(0) || gameManager.mustStart) && !jumping && !falling && !gameManager.paused) {
		//if ((shouldChangeDirection) && canDir && !jumping && !falling) {
			shouldChangeDirection = false;
			gameManager.started = true;
			if(!canJump)
				canDir = false;
			changeDirection ();
			gameManager.mustStart = false;
		}

	}

	void FixedUpdate() {
		move ();
		//timeScaleShow = Time.timeScale;
		if ((shouldJump || Input.GetMouseButtonDown(1)) && !falling && !jumping) {
		//if ((shouldJump) && canJump && !falling && !jumping ) {
			shouldJump = false;
			jumping = true;
		}
		if (transform.position.y > jumpHeight + gameManager.jumpHeight) {
			falling = true;
			jumping = false;
		}
		if ((transform.position.y <= height + gameManager.groundHeight + jumpThreshold && transform.position.y >= height + gameManager.groundHeight - jumpThreshold) && falling && !gameManager.gameOver) {
			stop ();
			falling = false;
			transform.position = new Vector3 (transform.position.x, height + gameManager.groundHeight, transform.position.z);
		}

		if (jumping && !gameManager.gameOver)
			jump ();
		if (falling)
			fall ();


	}

	void jump() {
		rigidBody.velocity = Vector3.Lerp(
			rigidBody.velocity + Vector3.up * jumpSpeed,
			new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z),
			5 * Time.deltaTime);
		/*if (Time.timeScale > 0) {
			if(ts <= 1)
				Time.timeScale = 1 - ts;
			ts += tsRate;
		}*/
	}

	void fall() {
		rigidBody.velocity = Vector3.Lerp(
			rigidBody.velocity,
			rigidBody.velocity + Vector3.down * jumpSpeed,
			40 * Time.deltaTime);
		/*if (Time.timeScale < 1) {
			if(ts >= 0)
				Time.timeScale = ts;
			ts += tsRate;
		}*/
	}

	void stop() {
		ts = 0;
		//Time.timeScale = 1;
		rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
	}

	void move() {
		if (speed < 12f)
			speed += gameManager.difficulter / 10000;
		Vector3 velocity = Vector3.zero;
		Quaternion rotation = Quaternion.identity;
		float velocityY = rigidBody.velocity.y;

		if (direction == 'x') {
			velocity = new Vector3 (speed, velocityY, 0);
			rotation = Quaternion.Euler (0, 90, 90);
		} else if (direction == 'z') {
			velocity = new Vector3 (0, velocityY, speed);
			rotation = Quaternion.Euler (0, 0, 90);
		} else
			return;


		if (transform.rotation != rotation)
			transform.rotation = Quaternion.Lerp (transform.rotation, rotation, speed * 1.5f * Time.deltaTime);
		rigidBody.velocity = velocity;

	}

	void canJumpTrue() {
		canJump = true;
		shouldJump = false;
		Invoke ("canDirTrue", .1f);
	}
	void canDirTrue() {
		canDir = true;
	}


	void changeDirection() {
		Vector3 offset = Vector3.zero;
		if (direction == 'x') {
			direction = 'z';
			offset = new Vector3 (0, 0, transform.localScale.y / 2);
		} else {
			direction = 'x';
			offset = new Vector3 (transform.localScale.y / 2, 0, 0);
		}

	}

}
