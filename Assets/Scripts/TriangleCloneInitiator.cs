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


	// Use this for initialization
	void Start () {
        CreateClones();
        
	}
	
	// Update is called once per frame
	void Update () {
        // updates cloned triangles
        for (int i = 0; i < triangleNum; i++)
        {
            triangleClone[i].Update();
        }
	}

    void CreateClones()
    {
        // Instantiate clone instance
        GameObject transformClass = Instantiate(triangle);
        triangleClone = transformClass.GetComponent<IGB283Transform[]>();

        for (int i = 0; i < triangleNum; i++)
        {
            triangleClone[i].speed = cloneSpeedInitial;
            triangleClone[i].angle = cloneAngleInitial;
            triangleClone[i].Start();
            cloneSpeedInitial += cloneSpeedInterval;
            cloneAngleInitial += cloneAngelInterval;
        }
    }
}
