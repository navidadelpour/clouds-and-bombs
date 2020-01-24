using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	public Sprite[] images;
	public static ShopManager instance;
	Text stars;

	void Awake() {
		instance = this;
	}

	void Start() {
		//PlayerPrefs.DeleteAll ();
		if(!PlayerPrefs.HasKey("setting"))
			PlayerPrefs.SetInt("setting", 0);
		setPlayerPrefs ();
	}

	// Update is called once per frame
	void Update () {
		GameObject.Find ("starsNum").GetComponent<Text> ().text = GameManager.instance.stars.ToString();
	}

	void setPlayerPrefs() {
		if(PlayerPrefs.GetInt("setting") != 1) {
			PlayerPrefs.SetString ("activeCar", "CarItem (0)");
			PlayerPrefs.SetString ("CarItem (0).bought", "True");
			PlayerPrefs.SetString ("CarItem (1).bought", "False");
			PlayerPrefs.SetString ("CarItem (2).bought", "False");
			PlayerPrefs.SetString ("CarItem (3).bought", "False");
			PlayerPrefs.SetString ("CarItem (4).bought", "False");
			PlayerPrefs.SetString ("CarItem (5).bought", "False");

			PlayerPrefs.SetString ("activePlatform", "PlatformItem (0)");
			PlayerPrefs.SetString ("PlatformItem (0).bought", "True");
			PlayerPrefs.SetString ("PlatformItem (1).bought", "False");
			PlayerPrefs.SetString ("PlatformItem (2).bought", "False");
			PlayerPrefs.SetString ("PlatformItem (3).bought", "False");
			PlayerPrefs.SetString ("PlatformItem (4).bought", "False");
			PlayerPrefs.SetString ("PlatformItem (5).bought", "False");
			PlayerPrefs.SetInt("viewportIndex", 0);

			PlayerPrefs.SetInt ("stars", 0);
			PlayerPrefs.SetInt ("hasSound", 1);
			PlayerPrefs.SetInt ("highScore", 0);
			PlayerPrefs.SetInt("setting", 1);
			PlayerPrefs.SetInt ("shownTutorial", 0);
		}
	}

		
	public void handleClick(GameObject self) {
		GameManager.instance.disablePlayer ();
		self.GetComponent<CarButton> ().handle ();
	}


}
