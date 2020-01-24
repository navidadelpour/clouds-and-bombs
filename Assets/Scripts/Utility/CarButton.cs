using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarButton : MonoBehaviour{

	int price;
	bool active;
	bool bought;
	public Color bodyColor;
	public Color wheelColor;
	public Color platformColor;
	public string type;

	void Start() {
		if (type == "car") {
			bodyColor = transform.FindChild ("model").transform.FindChild ("bodyMain").gameObject.GetComponent<Image> ().color;
			wheelColor = transform.FindChild ("model").transform.FindChild ("wheel1").gameObject.GetComponent<Image> ().color;
			active = PlayerPrefs.GetString ("activeCar") == name;
		} else if (type == "platform") {
			platformColor = transform.FindChild ("model").transform.FindChild ("platformBody").gameObject.GetComponent<Image> ().color;
			active = PlayerPrefs.GetString ("activePlatform") == name;
		}
		int.TryParse(transform.FindChild ("price").gameObject.GetComponent<Text> ().text, out price);

		if (active) {
			GameManager.instance.setPlayerColors ();
		}
	}


	void Update() {
		bought = PlayerPrefs.GetString (name + ".bought") == "True";
		if (type == "car") {
			active = PlayerPrefs.GetString ("activeCar") == name;
		} else if (type == "platform") {
			active = PlayerPrefs.GetString ("activePlatform") == name;
		}

		transform.FindChild ("price").gameObject.SetActive (!bought);
		transform.FindChild ("overlay").gameObject.SetActive(!bought);
		transform.FindChild ("tick").gameObject.SetActive (active);
	}

	public void handle() {
		if (active)
			return;
		if (bought) {
			if (type == "car") {
				PlayerPrefs.SetString ("activeCar", name);
			} else if (type == "platform") {
				PlayerPrefs.SetString ("activePlatform", name);
			}
		} else if (GameManager.instance.stars >= price) {
			GameManager.instance.stars -= price;
			PlayerPrefs.SetString (name + ".bought", "True");
			if (type == "car") {
				PlayerPrefs.SetString ("activeCar", name);
			} else if (type == "platform") {
				PlayerPrefs.SetString ("activePlatform", name);
				GameManager.instance.setPlatformColors ();
			}
			PlayerPrefs.SetInt("stars", GameManager.instance.stars);
		}
		GameManager.instance.setPlayerColors ();
	}

}
