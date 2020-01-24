using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAndDestroy : MonoBehaviour {

	GameManager gameManager;
	public GameObject bomb;
	public GameObject star;
	public GameObject cloud;
	public GameObject platform2;
	public GameObject platform3;
	public GameObject platform4;
	public GameObject health;
	public GameObject heightSetter;

	void Awake() {
		bomb = null;
	}

	// Use this for initialization
	void Start () {
		gameManager = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider other) {
		if (other.transform.tag == "Player")
			Invoke ("fall", .1f);
	}

	void fall() {
		gameManager.score++;
		SpawnManager.instance.spawn ();
		fallObj (bomb);
		fallObj (star);
		fallObj (cloud);
		fallObj (platform2);
		fallObj (platform3);
		fallObj (platform4);
		fallObj (health);
		fallObj (this.gameObject);
		fallObj (heightSetter);
	}

	void fallObj(GameObject obj) {
		if (obj == null)
			return;
		if (obj.GetComponent<BombExplode> () != null)
			StartCoroutine (explodeBomb (obj, .5f));
		obj.GetComponent<Rigidbody>().useGravity = true;
		obj.GetComponent<Rigidbody>().isKinematic = false;
		obj.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 2f, 0);
		Destroy (obj, 3f);
	}

	IEnumerator explodeBomb(GameObject obj, float delay) {
		yield return new WaitForSeconds (delay);
		obj.GetComponent<BombExplode>().explode ();
	}
}
