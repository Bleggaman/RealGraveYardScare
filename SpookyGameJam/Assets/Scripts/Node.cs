using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, iNode {


	public float waitTime;
	public List<iNode> nodeLinks = new List<iNode>(); 


	public List<iNode> getNodes(){

		//gets a node
		return nodeLinks;

	}

	public GameObject getGameObject ()
	{
		return gameObject;
	}
}
