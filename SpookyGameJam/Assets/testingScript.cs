using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingScript : MonoBehaviour {

	public GameObject ghost;
	public WorldInfo worldInfo;
	public GameObject vect;

	// Use this for initialization
	void Start () {
		worldInfo = GetComponent<WorldInfo> ();
	}

	void Update(){
		Debug.Log(Utils.findiNodeFromHint(worldInfo, vect.transform.position));

	}
	

}
