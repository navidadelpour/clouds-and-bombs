using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public GameObject main;
	public GameObject drum;
	public static AudioManager instance;
	AudioMixer audioMixer;
	float cutoffRate;
	float rateRate;
	float framesPast;
	Movement playerMovement;

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		main = GameObject.Find ("music_main");
		audioMixer = main.GetComponent<AudioSource> ().outputAudioMixerGroup.audioMixer;
		cutoffRate = 200;
		rateRate = 1f;
		playerMovement = GameObject.Find ("Player").GetComponent<Movement> ();
	}
	
	// Update is called once per frame
	void Update () {
		gameOver ();
		jump ();



	}

	public void mute(bool b) {
		if (!audioMixer)
			audioMixer = GameObject.Find ("music_main").GetComponent<AudioSource> ().outputAudioMixerGroup.audioMixer;
		if(b)
			audioMixer.SetFloat ("volume", -80f);
		else
			audioMixer.SetFloat ("volume", 0f);

	}

	void jump() {
		if(!GameManager.instance.gameOver)
			if (playerMovement.jumping) {
				float x;
				audioMixer.GetFloat ("rate", out x);
				if (x < 40) {
					audioMixer.SetFloat ("rate", rateRate * framesPast);
					if (framesPast >= 40) {
						framesPast = 0;
					} else
						framesPast++;
				}
			} else if (playerMovement.falling) {
				float x;
				audioMixer.GetFloat ("rate", out x);
				if (x < 40) {
					audioMixer.SetFloat ("rate", rateRate * framesPast);
					if (framesPast >= 40) {
						framesPast = 0;
					} else
						framesPast++;
				}
			} else {
				audioMixer.SetFloat ("rate", 0);
				framesPast = 0;
			}
	}


	void gameOver() {
		if (GameManager.instance.gameOver) {
			float x;
			audioMixer.GetFloat ("cutoff", out x);
			audioMixer.SetFloat ("rate",0);

			if (x < 21900) {
				audioMixer.SetFloat ("cutoff", cutoffRate * framesPast);
				if (framesPast >= 110) {
					framesPast = 0;
				} else
					framesPast++;
			}
		} else {
			audioMixer.SetFloat("cutoff", 0f);
		}
	}

	public void health () {
		GameObject.Find ("healthClip").GetComponent<AudioSource> ().Play ();
	}

	public void bomb() {
		GameObject.Find ("bombClip").GetComponent<AudioSource> ().Play ();
	}

	public void star() {
		AudioSource coinClip = GameObject.Find ("coinClip").GetComponent<AudioSource> ();
		coinClip.pitch = Random.Range (0.95f, 1.05f);
		coinClip.Play ();
	}
}
