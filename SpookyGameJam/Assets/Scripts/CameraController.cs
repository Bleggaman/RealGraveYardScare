using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	public GameObject player;
	public Vector3 offset;
	
	void Start () {
		this.transform.position = player.transform.position - offset;
	}
	// Update is called once per frame
	void Update () {
		this.transform.position = player.transform.position - offset;
	}
}
