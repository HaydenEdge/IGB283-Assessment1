using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IGB283Transform : MonoBehaviour {

    public float angle;
    public float speed;
	public float speedIncrement = 1.0f;
	public bool moveX = true;
	public bool moveY = false;

	public int maxX = 5;
	public int minX = -5;

	private float red = 1.0f;
	private float green = 1.0f;
	private float blue = 1.0f;

	private Vector3 offset;
    private Mesh mesh;
    
	public Material material;


	// Use this for initialization
	public void Start () {  

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
		red = (maxX / maxX) - (this.transform.position.x / maxX);
		green = (maxX / maxX) - (this.transform.position.x / minX);
		blue = (maxX / maxX) - (this.transform.position.x / maxX);

		mesh.colors = new Color[] {
			new Color (red, green, blue),
			new Color (red, green, blue),
			new Color (red, green, blue),
		};

        // Set the vertices in the mesh to their new position
        mesh.vertices = vertices;

        // Recalculate the bounding volume
        mesh.RecalculateBounds();

    }


    public void DrawTriangle()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        mesh = GetComponent<MeshFilter>().mesh;

        GetComponent<MeshRenderer>().material = material;

        mesh.Clear();

        mesh.vertices = new Vector3[] {
            new Vector3 (0, 0, 0),
            new Vector3 (0, 1, 0),
            new Vector3 (1, 1, 0),

        };

        mesh.triangles = new int[] { 0, 1, 2 };

        offset.x = mesh.bounds.size.x / 2;
        offset.y = mesh.bounds.size.y / 2;
    }


	// Move the object (translation)
	void Move() {
		Vector3 position = this.transform.position;
        
		//check if triangle has hit a boundry
        speed = speed * BoundryCollision(position);

		// Change x-axis position
		if (moveX == true) {
			position.x += Time.deltaTime * speed;
		}

		// Change y-axis position
		if (moveY == true) {
			position.y += (Time.deltaTime * speed);
		}

		this.transform.position = position;
	}


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


    // check if triangle has reached outer boundries
    int BoundryCollision (Vector3 position)
    {
        if (position.x >= maxX) {
            return -1;
		} else if (position.x <= minX) {
            return -1;
        } else {
            return 1;
        }
        

    }

}