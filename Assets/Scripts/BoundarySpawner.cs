using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundarySpawner : MonoBehaviour {

	public int boundaryNum = 2;
	public int yPos = -4;
	public int yPosInterval = 5;
	public GameObject boundary;

	private BoundaryChanger[] boundaryClone;
	private GameObject boundaryClass;


	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
