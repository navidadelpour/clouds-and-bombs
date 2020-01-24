using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {

	public static UiManager instance;
	public Sprite soundOffSprite;
	public Sprite soundOnSprite;
	public Sprite pauseSprite;
	public Sprite playSprite;
	

	RawImage title;
	Text score;
	Text stars;
	Text highScore;
	Text gameOverText;
	Text gameOverHighScore;
	Button button;
	Button questionMarkButton;
	Button giftButton;
	Button soundButton;
	Button shopButton;
	Button pauseButton;
	Button cameraToggleButton;
	Slider healthSlider;
	GameObject transitionPanel;
	GameObject infoPanel;
	GameObject shopPanel;
	GameManager gameManager;
	GameObject healthIcon;
	GameObject startPanel;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		gameManager = GameManager.instance;
		title = GameObject.Find ("title").GetComponent<RawImage> ();
		score = GameObject.Find ("score").GetComponent<Text> ();
		stars = GameObject.Find ("stars").GetComponent<Text> ();
		highScore = GameObject.Find ("highScore").GetComponent<Text> ();
		gameOverText = GameObject.Find ("gameOverText").GetComponent<Text> ();
		gameOverHighScore = GameObject.Find ("gameOverHighScore").GetComponent<Text> ();
		button = GameObject.Find ("resetButton").GetComponent<Button> ();
		healthSlider = GameObject.Find ("healthSlider").GetComponent<Slider> ();
		transitionPanel = GameObject.Find ("transitionPanel").gameObject;
		infoPanel = GameObject.Find ("infoPanel").gameObject;
		shopPanel = GameObject.Find ("shopPanel").gameObject;
		questionMarkButton = GameObject.Find ("questionMarkButton").GetComponent<Button>();
		giftButton = GameObject.Find ("giftButton").GetComponent<Button>();
		soundButton = GameObject.Find ("soundButton").GetComponent<Button>();
		shopButton = GameObject.Find ("shopButton").GetComponent<Button>();
		pauseButton = GameObject.Find ("pauseButton").GetComponent<Button>();
		cameraToggleButton = GameObject.Find ("cameraToggleButton").GetComponent<Button>();
		healthIcon = GameObject.Find ("healthIcon");
		highScore.text = "BEST: " + gameManager.highScore;
		startPanel = GameObject.Find ("startPanel");
		if(!gameManager.hasSound)
			soundButton.GetComponent<Image> ().sprite = soundOffSprite;

		if (PlayerPrefs.GetInt ("shownTutorial") != 1) {
			infoPanelIn ();
			PlayerPrefs.SetInt ("shownTutorial", 1);
		}

	}
	
	// Update is called once per frame
	void Update () {
		score.text = gameManager.score.ToString();
		stars.text = gameManager.stars.ToString();
		healthSlider.gameObject.SetActive (gameManager.hasHealth && !gameManager.gameOver);
		healthSlider.gameObject.GetComponent<Slider> ().value = gameManager.health;
		giftButton.gameObject.SetActive(gameManager.hasGift);
		giftButton.gameObject.GetComponent<Animator> ().SetBool ("wiggling", gameManager.hasGift);
		healthIcon.gameObject.SetActive (gameManager.hasHealth && !gameManager.gameOver);

	}

	public void startGame() {
		questionMarkButton.gameObject.GetComponent<Animator> ().SetTrigger ("out");
		giftButton.gameObject.GetComponent<Animator> ().SetTrigger ("out");
		shopButton.gameObject.GetComponent<Animator> ().SetTrigger ("out");
		soundButton.gameObject.GetComponent<Animator> ().SetTrigger ("out");
		cameraToggleButton.gameObject.GetComponent<Animator> ().SetTrigger ("out");

		title.gameObject.GetComponent<Animator> ().SetTrigger ("started");
		pauseButton.gameObject.GetComponent<Animator> ().SetTrigger ("started");
		score.gameObject.GetComponent<Animator> ().SetTrigger ("started");
		highScore.gameObject.GetComponent<Animator> ().SetTrigger ("started");
	}

	public void endGame() {
		transitionPanel.GetComponent<Animator> ().SetTrigger ("in");
		gameOverText.GetComponent<Animator> ().SetTrigger ("in");
		gameOverHighScore.text = "BEST: " + gameManager.highScore;
		gameOverHighScore.GetComponent<Animator> ().SetTrigger ("in");
		button.GetComponent<Animator> ().SetTrigger ("in");
		score.color = new Color (255, 255, 255, 1f);
	}

	public void reloadGame() {
		SceneManager.LoadScene ("GameScene");
	}

	public void infoPanelIn() {
		if (!gameManager.started) {
			gameManager.disablePlayer ();
			Time.timeScale = 0;
			infoPanel.transform.Translate (new Vector3 (0, -Screen.height, 0)); 
		}
	}

	public void infoPanelOut() {
		if (!gameManager.started) {
			gameManager.disablePlayer ();
			infoPanel.transform.Translate (new Vector3 (0, Screen.height, 0)); 
			Time.timeScale = 1;
		}
	}

	public void soundToggle() {
		if (!gameManager.started) {
			gameManager.disablePlayer ();
			Time.timeScale = 0;
			if (gameManager.hasSound) {
				soundButton.GetComponent<Image> ().sprite = soundOffSprite;
				gameManager.hasSound = false;
				PlayerPrefs.SetInt("hasSound", 0);

				AudioManager.instance.mute (true);
			} else {
				soundButton.GetComponent<Image> ().sprite = soundOnSprite;
				gameManager.hasSound = true;
				PlayerPrefs.SetInt("hasSound", 1);
				AudioManager.instance.mute (false);
			}
			Time.timeScale = 1;
		}
	}

	public void shopPanelIn() {
		if (!gameManager.started) {
			gameManager.disablePlayer ();
			startPanel.gameObject.SetActive(false);
			Time.timeScale = 0;
			shopPanel.transform.Translate (new Vector3 (0, Screen.height, 0)); 
		}
	}

	public void shopPanelOut() {
		if (!gameManager.started) {
			gameManager.disablePlayer ();
			startPanel.gameObject.SetActive (true);
			shopPanel.transform.Translate (new Vector3 (0, -Screen.height, 0)); 
			Time.timeScale = 1;
		}
	}
		
	public void pause() {
		if (!gameManager.paused) {
			Time.timeScale = 0;
			gameManager.paused = true;
			pauseButton.GetComponent<Image> ().sprite = playSprite;
			pauseButton.GetComponent<Image> ().color = new Color(255, 255, 255, .75f);
			
		} else {
			Time.timeScale = 1;
			gameManager.paused = false;
			pauseButton.GetComponent<Image> ().color = new Color(255, 255, 255, .75f);
			pauseButton.GetComponent<Image> ().sprite = pauseSprite;
		}
	}}
