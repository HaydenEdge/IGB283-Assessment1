using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundrySpawnerTEMPCHANGE : MonoBehaviour {

    

    public GameObject boundary;

    private GameObject boundaryClass;
    private Boundary[] boundaryClone;
    private int cloneCount = 0;


    // Use this for initialization
    void Start () {
        boundaryClone = new Boundary[2];

        for (int i = 0; i <= 1; i++)
        {
            // creates clone of boundary gameobject
            boundaryClass = Instantiate(boundary);

            //creates instance of boundary in boundaryClone[i]
            boundaryClone[i] = boundaryClass.GetComponent<Boundary>();
            
            // if the second boundary draws it on the left other side
            if (cloneCount == 1)
            {
                boundaryClone[i].xpos *= -1;
            }
            boundaryClone[i].Start();

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   
}
