using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCloneInitiator : MonoBehaviour {

    public int triangleNum;
	public float maxSpeed = 20.0f;
	public int yPos = -4;
	public int yPosInterval = 5;
    public float cloneSpeedInitial;
    public float cloneSpeedInterval;
    public float cloneAngleInitial;
    public float cloneAngleInterval;
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

			transformClass = Instantiate(triangle, new Vector3(0, yPos, 0), Quaternion.identity);
			yPos += yPosInterval;
			newMaterial = Instantiate (material);

            // creates instance of triangle in triangleClone[i]
            triangleClone[i] = transformClass.GetComponent<IGB283Transform>();
			triangleClone[i].material = newMaterial;
            triangleClone[i].speed = cloneSpeedInitial;
            triangleClone[i].angle = cloneAngleInitial;

            // draws clone triangle
            triangleClone[i].DrawTriangle();

            //increases speed and angle for next clone
			if ((cloneSpeedInitial + cloneSpeedInterval) <= maxSpeed) {
				cloneSpeedInitial += cloneSpeedInterval;
			}
            cloneAngleInitial += cloneAngleInterval;
        }
    }
		
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < triangleNum; i++) {

			// Speed up or slow down on mouse clicks
			if (Input.GetMouseButtonDown (0) && (triangleClone[i].speed < maxSpeed) && (triangleClone[i].speed > -maxSpeed)) {
				if (triangleClone[i].speed >= 0) {
					triangleClone[i].speed = triangleClone[i].speed + (triangleClone[i].speedIncrement);
				} else if (triangleClone[i].speed < 0) {
					triangleClone[i].speed = triangleClone[i].speed - (triangleClone[i].speedIncrement);
				}
			} else if (Input.GetMouseButtonDown (1) && (triangleClone[i].speed != 0)) {
				if (triangleClone[i].speed > 0) {
					triangleClone[i].speed = triangleClone[i].speed - (triangleClone[i].speedIncrement);
				} else if (triangleClone[i].speed < 0) {
					triangleClone[i].speed = triangleClone[i].speed + (triangleClone[i].speedIncrement);
				}
			}

		}

	}
		
}
