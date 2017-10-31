using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Utils {


	//FIND: not already on a iNode, identifying iNode to go to
	public static iNode findiNode(GameObject ghost, WorldInfo worldInfo){

		int number = (int)Random.Range (2, worldInfo.iNodes.Count);
		return worldInfo.iNodes[number - 1];
	}

	public static iNode findiNodeFromHint (WorldInfo worldInfo, Vector3 hintLocation){ //CONFIRMED WORKS
		
		List<iNode> nodesSorted = new List<iNode> ();
		nodesSorted = worldInfo.iNodes.OrderBy (node => Vector3.Distance (node.getGameObject().transform.position, hintLocation)).ToList();
		int number = (int)Random.Range (0, 4);
		for (int i = 0; i < 3; i++) {
//			Debug.Log(nodesSorted[i]);
		}
		return worldInfo.iNodes [number];
	}



	public static iNode findCloseHiding(GameObject ghost, WorldInfo worldInfo){

		List<iNode> hidingList = new List<iNode> ();

		foreach (iNode node in worldInfo.iNodes) {
			if (node is iHidingSpot) {
				hidingList.Add (node);
			}
		}
		int number = (int)Random.Range (2, hidingList.Count);
		return hidingList[number -1 ];
	}


	public static iNode findFarHiding(GameObject ghost, WorldInfo worldInfo){

		return worldInfo.iNodes
			.Where (asdf => asdf is iHidingSpot)
			.FirstOrDefault ();
	}

	public static iNode findHidingFromHint(GameObject ghost, WorldInfo worldInfo, Vector3 hintLocation){

		return worldInfo.iNodes
			.Where (asdf => asdf is iHidingSpot)
			.FirstOrDefault ();
	}
		
	//GET: on an iNode, choosing next iNode
	public static iNode getFirstiNode(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[0];
	}

	public static iNode getLastiNode(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[nodes.Count -1];
	}

	public static iNode getRandomiNode(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[(int) Random.Range (0, nodes.Count)];

	}

	public static iNode getCloseiNode(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[(int) Random.Range (0, nodes.Count)];

	}

	public static iNode getCloseHidingSpot(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[(int) Random.Range (0, nodes.Count)];

	}

	public static iNode getStrategicHidingSpot(iNode node){

		List<iNode> nodes = node.getNodes ();
		return nodes[(int) Random.Range (0, nodes.Count)];

	}


}
