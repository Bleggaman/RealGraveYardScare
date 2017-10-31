using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScareMapObject : ScareProp , iNode {

	public float activateRange = 5;


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


