using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public bool started;
	public bool gameOver;
	public bool shouldStart;
	public bool hasHealth;
	public float difficulter;
	public int score;
	public int stars;
	public float groundHeight;
	public float jumpHeight;
	public int health;
	public int highScore;
	public int giftTime;
	public bool hasGift;
	public bool hasSound;
	public bool mustStart;
	public bool paused;
	int touched;
	UiManager uiManager;
	Movement playerMovement;
	public GameObject PlatformItem;
	public int viewportIndex;
	void Awake() {
		instance = this;
	}
	// Use this for initialization
	void Start () {
		uiManager = UiManager.instance;
		playerMovement = GameObject.Find ("Player").GetComponent<Movement>();
		PlatformItem = GameObject.Find (PlayerPrefs.GetString ("activePlatform"));
		score = 0;
		stars = PlayerPrefs.GetInt ("stars");
		highScore = PlayerPrefs.GetInt ("highScore");
		giftTime = PlayerPrefs.GetInt("giftTime");
		hasSound = PlayerPrefs.GetInt("hasSound") == 1;
		viewportIndex = PlayerPrefs.GetInt ("viewportIndex");
		started = false;
		gameOver = false;
		shouldStart = true;
		groundHeight = 0;
		jumpHeight = 0;
		health = 230;
		hasGift = false;
		setPlayerColors ();
		AudioManager.instance.mute(!hasSound);
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetMouseButtonDown(0) || touched == 2) && !started && !mustStart)
		//if ((touched == 2) && !started && !mustStart)
			Invoke ("enablePlayer", .2f);
		
		if (started && shouldStart) {
			startGame ();
			uiManager.startGame ();
			shouldStart = false;
		}

		if (gameOver) {
			endGame ();
			uiManager.endGame ();
		}

		handleTouch();
		handleHealth ();
		handleGift ();
	}

	void startGame() {
	}

	void endGame() {
		PlayerPrefs.SetInt ("stars", stars);
		if (score > highScore) {
			highScore = score;
			PlayerPrefs.SetInt ("highScore", highScore);
		}
		PlayerPrefs.SetInt ("giftTime", giftTime);
		PlayerPrefs.SetInt("hasSound", hasSound ? 1 : 0);

	}

	void handleHealth() {
		if (hasHealth && health > 0) {
			health--;
		} else {
			health = 20 * 23;
			hasHealth = false;
		}
	}

	void handleGift() {
		if (giftTime > 0)
			giftTime--;
		else if (giftTime == 0)
			hasGift = true;
	}

	void handleTouch() {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began)
				touched = 1;
			if (touch.phase == TouchPhase.Ended && touched == 1)
				touched = 2;

			if (touched == 2) {
				if (touch.position.x < Screen.width / 2f)
					playerMovement.shouldChangeDirection = true;
				else
					playerMovement.shouldJump = true;
			}
		}
	}

	public void getGift() {
		if (!started) {
			disablePlayer ();
			int gift = Random.Range (1, 10) * 10;
			stars += gift;
			hasGift = false;
			//giftTime = 60 * 60 * 24 * 23;
			giftTime = 60 * 10 * 23;
			GameObject giftGained = GameObject.Find ("giftGained").gameObject;
			giftGained.GetComponent<Text> ().text = "+" + gift;
			giftGained.GetComponent<Animator> ().Play ("scoreGain");
		}
	}

	public void enablePlayer () {
		GameObject.Find ("Player").GetComponent<Movement> ().enabled = true;
		mustStart = true;
	}


	public void disablePlayer() {
		touched = 0;
		CancelInvoke ("enablePlayer");
	}

	public void setPlayerColors() {
		GameObject CarItem = GameObject.Find (PlayerPrefs.GetString ("activeCar"));
		Transform playerModel = GameObject.Find ("Player").transform.GetChild (0).transform;
		for (int i = 1; i < 10; i++) {
			playerModel.FindChild ("body (" + i + ")").GetComponent<Renderer> ().material.SetColor ("_Color", CarItem.GetComponent<CarButton> ().bodyColor); 
		}
		playerModel.FindChild ("wheel1").GetComponent<Renderer> ().material.SetColor("_Color", CarItem.GetComponent<CarButton> ().wheelColor); 
		playerModel.FindChild ("wheel2").GetComponent<Renderer> ().material.SetColor("_Color", CarItem.GetComponent<CarButton> ().wheelColor);
		setPlatformColors ();
	}

	public void setPlatformColor(GameObject obj) {
		if(obj != null)
			obj.gameObject.GetComponent<Renderer> ().material.SetColor("_Color", PlatformItem.GetComponent<CarButton> ().platformColor); 

	}

	public void setPlatformColors() {
		PlatformItem = GameObject.Find (PlayerPrefs.GetString ("activePlatform"));
		Transform platforms = GameObject.Find ("Platforms").transform;
		Transform platforms2 = GameObject.Find ("platforms2").transform;
		Transform platforms3 = GameObject.Find ("platforms3").transform;
		Transform platforms4 = GameObject.Find ("platforms4").transform;
		int x = 0;
		int.TryParse (PlatformItem.name.Substring (14, 1), out x);
		GameObject.Find ("bgImage").GetComponent<Image> ().sprite = ShopManager.instance.images [x];
		for (int i = 0; i < 5; i++) {
			setPlatformColor (GameObject.Find ("Platform (" + i + ")"));
		}

		for (int i = 0; i < platforms.childCount; i++) {
			GameObject p = platforms.GetChild (i).gameObject;
			if(p.name.Contains("Platform"))
				setPlatformColor(p);
		}

		for (int i = 0; i < platforms2.childCount; i++){
			GameObject p = platforms2.GetChild(i).gameObject;
			setPlatformColor(p);
		}
		for (int i = 0; i < platforms3.childCount; i++){
			GameObject p = platforms3.GetChild(i).gameObject;
			setPlatformColor(p);
		}
		for (int i = 0; i < platforms4.childCount; i++){
			GameObject p = platforms4.GetChild(i).gameObject;
			setPlatformColor(p);
		}

	}




}
