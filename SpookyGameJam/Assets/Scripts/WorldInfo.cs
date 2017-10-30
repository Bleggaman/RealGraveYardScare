using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInfo : MonoBehaviour {

	public List<iNode> iNodes = new List<iNode>();
	public GameObject nodesParent;

	void Start(){
		nodesParent = GameObject.Find ("Nodes/Hidings");

		foreach (Transform node in nodesParent.transform) {
			iNodes.Add (node.gameObject.GetComponent<iNode>());
		}

		foreach (iNode node in iNodes){
//			print (node);
		}
	}
}
