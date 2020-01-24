using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewportHandler : MonoBehaviour {

	public States[] gameStates;
	public States activeState;
	int index;

	// Use this for initialization
	void Start () {
		gameStates = new States[]{ States.DEFUALT, States.ROTATE_AROUND, States.FROM_TOP };
		index = GameManager.instance.viewportIndex;
		activeState = gameStates[index];
		if (index >= gameStates.Length - 1)
			index = 0;
		else
			index++;
	}

	public void cameraToggleButtonHandle() {
		if (!GameManager.instance.started) {
			GameManager.instance.disablePlayer ();
			activeState = gameStates [index];
			PlayerPrefs.SetInt ("viewportIndex", index);

			if(index == 2)
				GetComponent<TopView> ().inited = false;
			if(index == 0)
				GetComponent<CameraFollow> ().needInitial = true;

			if (index >= gameStates.Length - 1) {
				index = 0;
			} else
				index++;


		}

	}


}
