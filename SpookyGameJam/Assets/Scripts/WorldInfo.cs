using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInfo : MonoBehaviour {

	public List<iNode> iNodes = new List<iNode>();
	public GameObject nodesParent;
	public List<iLightable> lightables = new List<iLightable> ();
	public GameObject[] unecessaryJank;
	public bool playerLiit;
	public GameObject[] ghostArray;

	void Start(){
		ghostArray = GameObject.FindGameObjectsWithTag ("Ghost");

		nodesParent = GameObject.Find ("Nodes/Hidings");

		foreach (Transform node in nodesParent.transform) {    //should get nodes based on type
			iNodes.Add (node.gameObject.GetComponent<iNode>());
		}

		foreach (iNode node in iNodes){
//			print (node);
		}

		//lightables = FindObjectsOfType(typeof(iLightable)) as iLightable[]; didnt work?
		unecessaryJank = GameObject.FindGameObjectsWithTag("lightable");
		foreach (GameObject light in unecessaryJank) {
			if (light.GetComponent<iLightable> () == null) {
				lightables.Add(light.transform.GetChild (0).GetComponent<iLightable> ());
			} else {
				lightables.Add(light.GetComponent<iLightable> ());
			}
		} 
	}

	void Update(){
		playerLiit = isLit ();
	}
	public bool isLit(){
		foreach (iLightable lightSource in lightables) {
			if (lightSource.playerIsLit ()) {
				return true;
			}
		} 
		return false;
	}
}
