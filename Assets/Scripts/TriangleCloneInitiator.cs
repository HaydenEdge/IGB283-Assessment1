using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCloneInitiator : MonoBehaviour {

    public GameObject triangle;

	// Use this for initialization
	void Start () {
		Instantiate (triangle, new Vector3 (0, 2, 0), Quaternion.identity);
		Instantiate (triangle, new Vector3 (0, -2, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
