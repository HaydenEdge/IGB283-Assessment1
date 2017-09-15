using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IGB283Transform : MonoBehaviour {

	public float angle;
    public float speed;
	public float speedIncrement = 3.0f;
	public bool moveX = true;
	public bool moveY = false;
	public float newY;

	public float maxX = 5.0f;
	public float minX = -9.0f;
	private float totalLength;

	private float red = 1.0f;
	private float green = 1.0f;
	private float blue = 1.0f;
	private float rightMain;
	private float leftMain;
	public bool redLeft;
	public bool greenLeft;
	public bool blueLeft;

	private Vector3 offset;
    private Mesh mesh;
    
	public Material material;


	// Use this for initialization
	public void Start () {   

		// Define totalLength based on range of movement for objects
		totalLength = maxX - minX;

    }


	// Update is called once per frame
	public void Update () {
        Move ();

        // Get the vertices from the mesh
        Vector3[] vertices = mesh.vertices;

        // Get the transformation matrix
        Matrix3x3 T = Translate(offset);
        Matrix3x3 R = Rotate(angle * Time.deltaTime);
        Matrix3x3 T2 = Translate(-offset);
        Matrix3x3 M = T * R * T2;

        // Rotate each point in the mesh to its new position
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = M.MultiplyPoint(vertices[i]);
        }

		// Update the colours based on position
		// Define relevant variables
		rightMain = ((this.transform.position.x - minX) / totalLength);
		leftMain = 1.0f - ((this.transform.position.x - minX) / totalLength);

		// Reverse color calculations for each of the three colors if necessary
		if (redLeft == true) {
			red = leftMain;
		} else if (redLeft == false) {
			red = rightMain;
		}

		if (greenLeft == true) {
			green = leftMain;
		} else if (greenLeft == false) {
			green = rightMain;
		}

		if (blueLeft == true) {
			blue = leftMain;
		} else if (blueLeft == false) {
			blue = rightMain;
		}

		// Set the colors to the mesh
		mesh.colors = new Color[] {
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue)
		};

        // Set the vertices in the mesh to their new position
        mesh.vertices = vertices;

        // Recalculate the bounding volume
        mesh.RecalculateBounds();

    }


	// Draw the moving objects/triangles
    public void DrawTriangle() {

		// Add a MeshFilter and MeshRenderer to the empty GameObject
		gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

		// Get the Mesh from the MeshFilter
        mesh = GetComponent<MeshFilter>().mesh;

		// Set the material to the selected material
        GetComponent<MeshRenderer>().material = material;

		// Clear all vertex and index data from the mesh
        mesh.Clear();

		// Create necessary points
        mesh.vertices = new Vector3[] {
            new Vector3 (0, 0, 0),
            new Vector3 (0, 1, 0),
			new Vector3 (0, 2, 0),
			new Vector3 (0, 3, 0),
			new Vector3 (1, 1, 0),
			new Vector3 (1, 2, 0),
			new Vector3 (2, 0, 0),
			new Vector3 (2, 1, 0),
			new Vector3 (2, 2, 0),
			new Vector3 (2, 3, 0),
			new Vector3 (3, 1, 0),
			new Vector3 (3, 2, 0),
			new Vector3 (4, 0, 0),
			new Vector3 (4, 1, 0),
			new Vector3 (4, 2, 0),
			new Vector3 (4, 3, 0)
        };

		// Set vertex indices to create triangles
        mesh.triangles = new int[] {
			0, 1, 4,
			1, 2, 5,
			1, 4, 5,
			2, 3, 5,
			4, 5, 8,
			4, 7, 8,
			4, 6, 7,
			5, 8, 9,
			6, 7, 10,
			7, 8, 11,
			7, 10, 11,
			9, 8, 11,
			11, 15, 14,
			10, 11, 14,
			10, 13, 14,
			10, 13, 12
		};

		// Allow object to rotate around mesh center
        offset.x = mesh.bounds.size.x / 2;
        offset.y = mesh.bounds.size.y / 2;
    }


	// Move the object (translation)
	void Move() {

		// Get current position
		Vector3 position = this.transform.position;
        
		// Check if triangle has hit a boundary
        speed = speed * BoundryCollision(position);

		// Change x-axis position
		if (moveX == true) {
			position.x += Time.deltaTime * speed;
		}

		// Change y-axis position
		position.y = newY;

		// Apply new position
		this.transform.position = position;
	}


	// Create the rotation function
    Matrix3x3 Rotate(float angle)
    {
        // Create a new matrix
        Matrix3x3 matrix = new Matrix3x3();

        // Set the rows of the matrix
        matrix.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f));
        matrix.SetRow(1, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
        matrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        // Return the matrix
        return matrix;
    }
    

	// Create the translation function
    Matrix3x3 Translate(Vector3 offset)
    {
        // Create a new matrix
        Matrix3x3 matrix = new Matrix3x3();

        // Set the rows of the matrix
        matrix.SetRow(0, new Vector3(1.0f, 0.0f, offset.x));
        matrix.SetRow(1, new Vector3(0.0f, 1.0f, offset.y));
        matrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        // Return the matrix
        return matrix;
    }


    // Check if triangle has reached outer boundries
    int BoundryCollision (Vector3 position) {
        // Inverts speed and thus direction of object if hit minimum/maximum range
        if (position.x >= maxX) {
            return -1;
        } else if (position.x <= minX) {
            return -1;
        } else {
            return 1;
        }
        

    }

}