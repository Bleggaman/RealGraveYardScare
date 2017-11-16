using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour, iScarable {

	public bool qPressed = false;
	public ScareEquipItem[] equipItems = new ScareEquipItem[3];
	public ScareEquipItem flashLightPrefab;
	public ScareEquipItem scaryMaskPrefab;
	public ScareEquipItem regularScarePrefab;

	public ScareEquipItem flashLightRef;
	public List<iActivate> inRange = new List<iActivate>();
	bool playerSpawning = false;
	private AudioSource audio2;

	//score texts:
	public int count;
	public Text countText;
	private int high;
	public Text highScore;

	// Use this for initialization
	void Start () {
		countText.text = "Space = Flashlight";
		highScore.text = "Q turns on/off Lamps";
		count = 0;
		flashLightRef = GameObject.Find ("FlashLight").GetComponent<ScareEquipItem>();
		equipItems [0] = flashLightRef;

		audio2 = GetComponent<playerMovement2> ().audio2;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Q)){


			foreach (iActivate collidingObject in this.inRange) {
//				Debug.Log (collidingObject);
				collidingObject.activate (gameObject);
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (equipItems[0] != null) {
				equipItems [0].activate (gameObject);

			}
		}

		//not in use
		if (Input.GetKeyDown (KeyCode.E)) {
			if (equipItems[1] != null) {
				equipItems [1].activate (gameObject);
		
			}
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			if (equipItems[2] != null) {
				equipItems [2].activate (gameObject);
			}
		}
	}



	void OnTriggerEnter(Collider other){
		if (other.CompareTag("MapActivateObject")){
			if (other.gameObject != gameObject) {
				this.inRange.Add (other.GetComponent<iActivate> ());
			}
		}
	}


	void OnTriggerExit(Collider other){
		if (other.CompareTag("MapActivateObject")){
			if (other.gameObject != gameObject) {
				this.inRange.Remove (other.GetComponent<iActivate> ());
			}
		}
	}

	#region iScarable implementation
	public void scare (int scarePower)
	{if (playerSpawning == false) {
			Debug.Log ("eek : " + scarePower);
			GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");
			//too many ghosts can lag the game.
			for (int i = 7; i < ghosts.Length; i++) {
				Destroy(ghosts[i]);
			}
			StartCoroutine (playerSpawnTime ());
			playerSpawning = true;
			audio2.Play ();
		}

	}
	#endregion

	IEnumerator playerSpawnTime(){
		countText.text = "Ghosts Scared: 0";
		high =  Mathf.Max(count, high);
		highScore.text = "HighScore: " + high;

		count = 0;
		yield return new WaitForSeconds (.2f);
		transform.position = new Vector3 (10, 0, -50);
		playerSpawning = false;
	}
}
