using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCloneInitiator : MonoBehaviour {

    public int triangleNum;
    public float cloneSpeedInitial;
    public float cloneSpeedInterval;
    public float cloneAngleInitial;
    public float cloneAngelInterval;
    public GameObject triangle;
	public Material material;

    private IGB283Transform[] triangleClone;
    private GameObject transformClass;
	private Material newMaterial;

    // Use this for initialization
    void Start () {

        //initialise clone array
        triangleClone = new IGB283Transform[triangleNum];
        //create clone of triangle

        
        for (int i = 0; i < triangleNum; i++)
        {

            transformClass = Instantiate(triangle);
			newMaterial = Instantiate (material);

            // creates instance of triangle in triangleClone[i]
            triangleClone[i] = transformClass.GetComponent<IGB283Transform>();
			triangleClone[i].material = newMaterial;
            triangleClone[i].speed = cloneSpeedInitial;
            triangleClone[i].angle = cloneAngleInitial;

            // draws clone triangle
            triangleClone[i].DrawTriangle();

            //increases speed and angle for next clone
            cloneSpeedInitial += cloneSpeedInterval;
            cloneAngleInitial += cloneAngelInterval;
        }
    }


	
	// Update is called once per frame
	void Update () {
        // updates cloned triangles
        for (int i = 0; i < triangleNum; i++)
        {
            triangleClone[i].Update();
        }
	}


        // Instantiate clone instance



}
