using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("loadScene", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void loadScene() {
		SceneManager.LoadScene ("GameScene");
	}
}
