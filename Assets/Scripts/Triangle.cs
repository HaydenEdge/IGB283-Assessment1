using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour {


    public Material material;
	// Use this for initialization
	void Start () {
        // get and store Mesh functions on variable for easier use
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        GetComponent<MeshRenderer>().material = material;

        mesh.Clear();

        mesh.vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0)
        };

        mesh.colors = new Color[]
        {
            new Color(0.8f, 0.3f, 1.0f),
            new Color(0.8f, 1.0f, 1.0f),
            new Color(0.0f, 0.3f, 1.0f)
        };

        mesh.triangles = new int[] { 0, 1, 2 };
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 IGB283Transform
    {
        get {
            Vector3 newTriangle = new Vector3();
            return newTriangle;
        }
    }
}
