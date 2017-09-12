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

    private IGB283Transform[] triangleClone;
    private GameObject transformClass;


    // Use this for initialization
    void Start () {

        transformClass = Instantiate(triangle);
        
        for (int i = 0; i < triangleNum; i++)
        {
            // creates instance of triangle in triangleClone[i]
            triangleClone[i] = triangle.GetComponent<IGB283Transform>();
            triangleClone[i].speed = cloneSpeedInitial;
            triangleClone[i].angle = cloneAngleInitial;

            triangleClone[i].DrawTriangle();
            triangleClone[i].Start();

            Instantiate(triangleClone[i]);
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
