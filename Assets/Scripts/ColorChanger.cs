using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

	//public GameObject boundary;
	public Material material;
	public Color color = Color.white;
	public float yoffset = 0.0f;
	private float boundaryWidth = 0.5f;
	public bool isActive = false;
	private Mesh mesh;
	private Vector3[] meshorigin = new Vector3[3];


	// Use this for initialization
	public void Start() {
		DrawBoundary(0, 0, 0.5f, boundaryWidth, 0);

	}


	// Update is called once per frame
	void Update() {
		meshorigin = mesh.vertices;
		MouseClickAction();

	}


	void DrawBoundary(float x, float y, float width, float height, float z)
	{
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();

		mesh = GetComponent<MeshFilter>().mesh;

		GetComponent<MeshRenderer>().material = material;

		mesh.Clear();

		mesh.vertices = new Vector3[] {
			new Vector3(x, y, z),
			new Vector3(x,  y + height, z),
			new Vector3(x + width,  y + height, z),
			new Vector3(x + width, y, z)

		};

		mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

		gameObject.AddComponent<BoxCollider2D>();

	}


	void MouseLeftAction() {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition);

		if (hitCollider && hitCollider.name == this.name) {
			isActive = true;
		}

	}

	void MouseRightAction() {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition);

		if (hitCollider && hitCollider.name == this.name) {
			isActive = false;
		}

	}


	void MouseClickAction() {
		if (Input.GetMouseButton(0)) {
			MouseLeftAction ();
		} else if (Input.GetMouseButton(1)) {
			MouseRightAction ();
		}
	}


	Vector2 ToVector2(Vector3 v3) {
		return new Vector2(v3.x, v3.y);
	}


}
