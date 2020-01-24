using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager instance;
	public GameObject platform;
	public GameObject bomb;
	public GameObject star;
	public GameObject cloud;
	public GameObject platform2;
	public GameObject platform3;
	public GameObject platform4;
	public GameObject health;
	public GameObject heightSetter;
	public GameObject gap;
	Transform platforms;
	Transform bombs;
	Transform stars;
	Transform clouds;
	Transform platforms2;
	Transform platforms3;
	Transform platforms4;
	Transform healths;
	Transform heightSetters;
	Vector3 oldPosition;
	Vector3 offset;
	Vector3 bombOffset;
	Vector3 up;
	Vector3 heightSetterOffset;
	Quaternion starQuaternion;
	Quaternion platform2Quaternion;
	Quaternion platform3Quaternion;
	public char direction;
	float chance;
	float starChance;
	int starsLeft;
	int upMin;
	float bombChance;
	float cloudChance;
	float bombRandom;
	float healthChance;
	float upChance;
	float platform2Chance;
	bool mustInvoke;
	int gapCount;
	int directionCount;
	float gapChance;
	float currentHeight;
	GameManager gameManager;



	void Awake() {
		instance = this;
	}
	
	// Use this for initialization
	void Start () {
		gameManager = GameManager.instance;
		platforms = GameObject.Find ("Platforms").transform;
		bombs = GameObject.Find ("Bombs").transform;
		stars = GameObject.Find ("Stars").transform;
		clouds = GameObject.Find ("Clouds").transform;
		platforms2 = GameObject.Find("platforms2").transform;
		platforms3 = GameObject.Find("platforms3").transform;
		platforms4 = GameObject.Find("platforms4").transform;
		healths = GameObject.Find ("healths").transform;
		heightSetters = GameObject.Find ("heightSetters").transform;
		oldPosition = transform.position;
		bombChance = 90;
		starChance = 95;
		healthChance = 98;
		cloudChance = 60;
		platform2Chance = 60;
		upChance = 70;
		gapChance = 70;
		currentHeight = 2f;
		upMin = 0;
		direction = 'x';
		mustInvoke = true;
		offset = new Vector3 (platform.transform.localScale.x, 0, 0);
		starQuaternion = Quaternion.identity;
		platform2Quaternion = Quaternion.identity;
		platform3Quaternion = Quaternion.identity;
		for (int i = 0; i < 20; i++) {
			spawn ();
			chance = 90f;
		}
		chance = 70f;

	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.started && mustInvoke) {
			//InvokeRepeating ("spawn", .1f, .2f);
			mustInvoke = false;
		}

		if(gameManager.gameOver)
			CancelInvoke ("spawn");

	}

	public void spawn () {
		oldPosition = oldPosition + offset;
		if (Random.Range (0, 100) > gapChance && gapCount == 0 && upMin == 0 && directionCount == 0) {
			Instantiate (gap, oldPosition + up + Vector3.down * .5f, Quaternion.identity, platforms);
			gapCount = 3;
			return;
		}
		if(gapCount > 0)
			gapCount--;
		if(chance >= 50)
			chance -= gameManager.difficulter / 100f;
		if (bombChance >= 70)
			bombChance -= gameManager.difficulter / 100f;
		if (upChance >= 60)
			upChance -= gameManager.difficulter / 100f;
		if (gapChance >= 60)
			gapChance -= gameManager.difficulter / 100f;
		bombRandom = (int) Random.Range (-2, 3);
		if (bombRandom == 0)
			bombRandom++;

		if (Random.Range (0, 100) > starChance)
			starsLeft = 3;

		if (Random.Range (0, 100) > chance && upMin == 0 && gapCount == 0) {
			changeDirection ();
			directionCount = 3;
		}
		if (directionCount > 0)
			directionCount--;
		if (direction == 'x') {
			heightSetterOffset = Vector3.left * (transform.localScale.x);
			bombOffset = new Vector3 (0, 0.5f, bombRandom);
		} else if (direction == 'z') {
			heightSetterOffset = Vector3.back * (transform.localScale.x);
			bombOffset = new Vector3 (bombRandom, 0.5f, 0);
		}

		

		bool shouldSpawnHeightSetter = false;
		if (Random.Range (0, 100) > upChance && upMin == 0 && gapCount == 0 && directionCount == 0) {
			up = new Vector3 (0, currentHeight, 0);
			currentHeight += 2f;
			upMin = 3;
			shouldSpawnHeightSetter = true;
		}

		if(upMin > 0)
			upMin--;



		GameObject platformCreated = Instantiate (platform, oldPosition + up, Quaternion.identity, platforms);
		gameManager.setPlatformColor (platformCreated);
		if (shouldSpawnHeightSetter && gapCount == 0) {
			GameObject heightSetterCreated = Instantiate (heightSetter, oldPosition + heightSetterOffset + up, Quaternion.identity, heightSetters);
			platformCreated.GetComponent<FallAndDestroy> ().heightSetter = heightSetterCreated.gameObject;
		}
		if (Random.Range (0, 100) > bombChance && upMin < 4 && !shouldSpawnHeightSetter && directionCount != 2 &&  gapCount != 2) {
			GameObject bombCreated = Instantiate (bomb, oldPosition + up + bombOffset, Quaternion.identity, bombs);
			platformCreated.GetComponent<FallAndDestroy> ().bomb = bombCreated.gameObject;
		}
		if (starsLeft > 0) {
			GameObject starCreated = Instantiate (star, oldPosition + up + Vector3.up, starQuaternion, stars);
			platformCreated.GetComponent<FallAndDestroy> ().star = starCreated.gameObject;
			starsLeft--;
		}
		if (Random.Range(0, 100) > cloudChance) {
			GameObject cloudCreated = Instantiate (cloud, oldPosition + up + Vector3.up * 7 + (bombOffset + Vector3.down * 0.5f + Vector3.left * 2) * 3f, Quaternion.identity, clouds);
			Vector3 cloudCreatedScale = cloudCreated.transform.localScale;
			cloudCreated.transform.localScale = new Vector3(cloudCreatedScale.x * Random.Range (.8f, 1.2f), cloudCreatedScale.y * Random.Range (.8f, 1.2f), cloudCreatedScale.z  * Random.Range(.8f, 1.2f));
			platformCreated.GetComponent<FallAndDestroy> ().cloud = cloudCreated.gameObject;
			cloudCreated.GetComponent<Rigidbody> ().AddExplosionForce (Random.Range(-300f, -100f), cloudCreated.transform.position, 100f);
		}

		if (Random.Range(0, 100) > platform2Chance) {
			GameObject platform2Created = Instantiate (platform2, oldPosition + up + bombOffset * -3f + Vector3.left, platform2Quaternion, platforms2);
			gameManager.setPlatformColor (platform2Created);
			Material material = platform2Created.GetComponent<Renderer> ().material;
			int r = Random.Range (-10, 10);
			material.SetColor ("_Color", new Color(material.color.r + r, material.color.g, material.color.b));
			Vector3 platform2CreatedScale = platform2Created.transform.localScale;
			platform2Created.transform.localScale = new Vector3(platform2CreatedScale.x * Random.Range (.1f, 3f), platform2CreatedScale.y * Random.Range (.2f, 5f), platform2CreatedScale.z  * Random.Range(.2f, 5f));
			platformCreated.GetComponent<FallAndDestroy> ().platform2 = platform2Created.gameObject;
		}

		if (Random.Range(0, 100) > platform2Chance) {
			GameObject platform3Created = Instantiate (platform3, oldPosition + up + bombOffset * -3f + Vector3.left, platform3Quaternion, platforms3);
			gameManager.setPlatformColor (platform3Created);
			Material material = platform3Created.GetComponent<Renderer> ().material;
			int r = Random.Range (-10, 10);
			material.SetColor ("_Color", new Color(material.color.r + r, material.color.g, material.color.b));
			float scale = Random.Range (.1f, 3f);
			Vector3 platform3CreatedScale = platform3Created.transform.localScale;
			platform3Created.transform.localScale = new Vector3(platform3CreatedScale.x * scale, platform3CreatedScale.y * Random.Range (.2f, 5f), platform3CreatedScale.z  * scale);
			platform3Created.transform.position += Vector3.down * platform3Created.transform.localScale.y;
			platformCreated.GetComponent<FallAndDestroy> ().platform3 = platform3Created.gameObject;
		}

		if (Random.Range(0, 100) > platform2Chance) {
			GameObject platform4Created = Instantiate (platform4, oldPosition + up + bombOffset * -3f + Vector3.left, platform3Quaternion, platforms4);
			gameManager.setPlatformColor (platform4Created);
			Material material = platform4Created.GetComponent<Renderer> ().material;
			int r = Random.Range (-10, 10);
			material.SetColor ("_Color", new Color(material.color.r + r, material.color.g, material.color.b));
			float scale = Random.Range (.5f, 5f);
			Vector3 platform4CreatedScale = platform4Created.transform.localScale;
			platform4Created.transform.localScale = new Vector3(platform4CreatedScale.x * scale, platform4CreatedScale.y * scale, platform4CreatedScale.z  * scale);
			platform4Created.transform.position += Vector3.down * platform4Created.transform.localScale.y;
			platformCreated.GetComponent<FallAndDestroy> ().platform4 = platform4Created.gameObject;
		}

		if (Random.Range (0, 100) > healthChance && starsLeft == 0) {
			GameObject healthCreated = Instantiate (health, oldPosition + up + Vector3.up * 1.5f, Quaternion.identity, healths);
			platformCreated.GetComponent<FallAndDestroy> ().health = healthCreated.gameObject;
		}

	}

	void changeDirection() {
		upMin++;
		gameManager.difficulter++;
		if (direction == 'x') {
			direction = 'z';
			offset = new Vector3 (0, 0, platform.transform.localScale.z);
			starQuaternion = Quaternion.Euler (new Vector3 (0, 90, 90));
			platform2Quaternion = Quaternion.Euler (new Vector3 (0, 0, 90));
		} else if (direction == 'z'){
			direction = 'x';
			offset = new Vector3 (platform.transform.localScale.x, 0, 0);
			starQuaternion = Quaternion.Euler (new Vector3 (0, 0, 90));
			platform2Quaternion = Quaternion.Euler (new Vector3 (0, 90, 90));

		}
	}
}
