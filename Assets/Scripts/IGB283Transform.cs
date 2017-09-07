using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IGB283Transform : MonoBehaviour {

	public float speed = 5.0f;
	public bool moveX = true;
	public bool moveY = false;
    public float angle = 10f;
    private Mesh mesh;
    public Material material;
    



	// Use this for initialization
	void Start () {
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

        mesh.colors = new Color[] {
            new Color (0.8f, 0.3f, 1.0f),
            new Color (0.8f, 0.3f, 1.0f),
            new Color (0.8f, 0.3f, 1.0f),

        };

        mesh.triangles = new int[] { 0, 1, 2 };
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

    Matrix3x3 RotateMatrix( float angle)
    {
        Matrix3x3 matrix = new Matrix3x3();
        // set the rows of the matrix
        matrix.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f));
        matrix.SetRow(1, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
        matrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));
        //return the matrix
        return matrix;
    }
    
    void rotate()
    {
        Vector3[] verticies = mesh.vertices;
        Matrix3x3 M = RotateMatrix(angle * Time.deltaTime);

        // rotate each point on mesh to new position

        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i] = M.MultiplyPoint(verticies[i]);
        }
        mesh.vertices = verticies;
        //recalculate bounding volume of mesh
        mesh.RecalculateBounds();
    }

	// Rotation

	// Scaling

}