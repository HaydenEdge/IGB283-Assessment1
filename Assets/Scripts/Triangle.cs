using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour {

    public Material material;
    private Mesh mesh;
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
		
	}
}
