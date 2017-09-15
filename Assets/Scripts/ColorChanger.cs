using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

	public Material material;
	public Color color = Color.white;
	public float yoffset = 0.0f;
	private float objectWidth = 0.5f;
	public bool isActive = false;
	private Mesh mesh;


	// Use this for initialization
	public void Start() {
		// Draw the mesh
		DrawObject(0, 0, 0.5f, objectWidth, 0);

	}


	// Update is called once per frame
	void Update() {
		
		MouseClickAction();

	}

	// Draw the shape of the object
	void DrawObject(float x, float y, float width, float height, float z) {

		// Add a MeshFilter and MeshRenderer to the empty GameObject
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();

		// Get the Mesh from the MeshFilter
		mesh = GetComponent<MeshFilter>().mesh;

		// Set the material to the material we have selected
		GetComponent<MeshRenderer>().material = material;

		// Clear all vertex and index data from the mesh
		mesh.Clear();

		// Create tris
		mesh.vertices = new Vector3[] {
			new Vector3(x, y, z),
			new Vector3(x,  y + height, z),
			new Vector3(x + width,  y + height, z),
			new Vector3(x + width, y, z)
		};

		// Set vertex indices
		mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

		// Apply a BoxCollider
		gameObject.AddComponent<BoxCollider2D>();

	}


	// Determine if the mouse is over the object
	void MouseLeftAction() {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition);

		// If this object is selected, change 'active' to true
		// 'active' is read elsewhere to allow the buttons to affect colour changes
		if (hitCollider && hitCollider.name == this.name) {
			isActive = true;
		}

	}


	// Determine if the mouse is over the object
	void MouseRightAction() {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition);

		// If this object is selected, change 'active' to false
		if (hitCollider && hitCollider.name == this.name) {
			isActive = false;
		}

	}


	// Determine if left or right clicking
	void MouseClickAction() {
		if (Input.GetMouseButton(0)) {
			MouseLeftAction ();
		} else if (Input.GetMouseButton(1)) {
			MouseRightAction ();
		}
	}

}
