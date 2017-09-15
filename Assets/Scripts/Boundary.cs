using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

    public Material material;
    public Color color = Color.white;
    public float xpos = 6;
    public float yoffset = 0.0f;
	private float boundaryWidth = 2.0f;
    public bool isMoving = false;
    private Mesh mesh;


    // Use this for initialization
    public void Start() {
		// Draw the mesh
        DrawBoundary(xpos, 0, 0.5f, boundaryWidth, 0);
    }


    // Update is called once per frame
    void Update() {

        MouseClickAction();
		MoveVertical ();

    }


	// Draw the shape of the boundary
    void DrawBoundary(float x, float y, float width, float height, float z) {

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


	// Allow the object to move along the y-axis
    void MoveVertical() {

		// Define current position
        Vector2 position = this.transform.position;

		// Get position of mouse
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		// If the object is allowed to move, move it along the y-axis to match the mouse
		if (isMoving == true) {
			yoffset = mousePosition.y - (boundaryWidth / 2);
			position.y = yoffset;
			this.transform.position = position;
		}

    }


	// Determine if the mouse is over the object
    void MouseOverAction() {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition);

		// If this object is meant to be selected, allow it to move
		if (hitCollider && hitCollider.name == this.name) {
			isMoving = true;
		}

    }


	// Only allow object to move if the user is holding left-click
    void MouseClickAction() {
        if (Input.GetMouseButton(0)) {
			MouseOverAction ();
        } else if (Input.GetMouseButtonUp(0)) {
            isMoving = false;
        }
    }


}
