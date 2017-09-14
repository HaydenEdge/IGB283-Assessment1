using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryChanger : MonoBehaviour {

	private bool isMoving = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		MouseClickAction();
		Move();
	}

	void Move () {
		// Find mouse position in world
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (isMoving == true) {
			this.transform.position = new Vector3(this.transform.position.x, mousePosition.y, 0.0f);
		}
	}

	// Determine if the mouse is over the object
	void MouseOverAction() {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition);
		if (hitCollider && hitCollider.transform.tag == "BoundaryMover") {
			isMoving = true;
		}
	}

	// Perform an action when mouse clicked
	void MouseClickAction() {
		if (Input.GetMouseButtonDown(0)) {
			MouseOverAction();
		} else if (Input.GetMouseButtonUp(0)) {
			isMoving = false;
		}
	}
}
