using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightRandom : MonoBehaviour {

	Lamp lamp1;
	Lamp lamp2;
	AudioSource lampSound;

	bool waiting;

	// Use this for initialization
	void Start () {
		lamp1 = transform.GetChild (0).gameObject.GetComponent<Lamp> ();
		lamp2 = transform.GetChild (1).gameObject.GetComponent<Lamp> ();
		lampSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		lampSound.enabled = lamp1.lampOn;
		if (!waiting) {
			StartCoroutine (lampRandomOn ());

		}
	}



	IEnumerator lampRandomOn(){
		waiting = true;
		int waitTime = Random.Range (0, 40);
		int chance = Random.Range (0, 100);
		yield return new WaitForSeconds(waitTime);
		if (chance > 10) {
			waiting = true;
			//flicker
			lamp1.activate (gameObject);
			lamp2.activate (gameObject);
			yield return new WaitForSeconds(.1f);
			lamp1.activate (gameObject);
			lamp2.activate (gameObject);
			yield return new WaitForSeconds(.01f);
			lamp1.activate (gameObject);
			lamp2.activate (gameObject);
			yield return new WaitForSeconds(.2f);
			lamp1.activate (gameObject);
			lamp2.activate (gameObject);
			yield return new WaitForSeconds(.01f);
			lamp1.activate (gameObject);
			lamp2.activate (gameObject);
			yield return new WaitForSeconds(.1f);

			waiting = false;
		}



	}
}
