using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomAppear : MonoBehaviour {
	
	public List<MeshRenderer> soldiersList;
	public int randomIndex;
	public Transform soldierPos;

	void Start () {
		randomIndex = Random.Range(0,soldiersList.Capacity);
		soldiersList[randomIndex].enabled =true;
		//soldierPos = soldiersList[randomIndex].transform;
		soldiersList[randomIndex].tag = "RandomEnemy";
	}
}