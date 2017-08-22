using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGB283Transform : MonoBehaviour {

	public float speed = 5.0f;
	public bool moveX = true;
	public bool moveY = false;


	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		Move (); //Just remove this line if you need
	}


	// Move the object (translation)
	void Move() {
		Vector3 position = this.transform.position;

		// Change x-axis position
		if (moveX == true) {
			position.x += Time.deltaTime * speed;
		}

		// Change y-axis position
		if (moveY == true) {
			position.y += Time.deltaTime * speed;
		}

		this.transform.position = position;
	}

	// Rotation

	// Scaling

}