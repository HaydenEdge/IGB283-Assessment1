using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCloneInitiator : MonoBehaviour {

    public int triangleNum = 2;
	public float maxSpeed = 20.0f;
	public float yPos = -4.0f;
	public float yPosInterval = 5.0f;
	private float boundaryYAdjust = 0.5f;
	private float boundaryXAdjust = -4.0f;
	private float boundaryminXAdjust = -0.5f;
    public float cloneSpeedInitial;
    public float cloneSpeedInterval;
    public float cloneAngleInitial;
    public float cloneAngleInterval;
    public GameObject triangle;
	public GameObject boundary;
	public Material material;

	private Boundary[] boundaryClone;
    private IGB283Transform[] triangleClone;
    private GameObject transformClass;
	private GameObject boundaryClass;
	private Material newMaterial;


    // Use this for initialization
    void Start () {

        // TRIANGLES - Initialise clone array
        triangleClone = new IGB283Transform[triangleNum];

		// BOUNDARY - Initialise clone array
		boundaryClone = new Boundary[triangleNum * 2];
        
		// TRIANGLES + BOUNDARY - Create clone        
        for (int i = 0; i < triangleNum; i++) {

			// Initialise TRIANGLE
			transformClass = Instantiate(triangle, new Vector3(0.0f, yPos, 0.0f), Quaternion.identity);
			newMaterial = Instantiate(material);

            // Create instance of TRIANGLE in triangleClone[i]
            triangleClone[i] = transformClass.GetComponent<IGB283Transform>();
			triangleClone[i].newY = yPos;
			triangleClone[i].material = newMaterial;
            triangleClone[i].speed = cloneSpeedInitial;
            triangleClone[i].angle = cloneAngleInitial;

			// Initialise maxBOUNDARY
			boundaryClass = Instantiate (boundary, new Vector3 (triangleClone[i].maxX + boundaryXAdjust, yPos + boundaryYAdjust, 0.0f), Quaternion.identity);

			// Create instance of maxBOUNDARY in boundaryClone[i]
			boundaryClone[i] = boundaryClass.GetComponent<Boundary>();
			boundaryClone[i].name = (i.ToString());

			// Initialise minBOUNDARY
			boundaryClass = Instantiate (boundary, new Vector3 (triangleClone[i].minX + boundaryXAdjust + boundaryminXAdjust, yPos + boundaryYAdjust, 0.0f), Quaternion.identity);

			// Create instance of minBOUNDARY in boundaryClone[i]
			boundaryClone[i + triangleNum] = boundaryClass.GetComponent<Boundary>();
			boundaryClone[i + triangleNum].name = (i.ToString());


            // Draw clone TRIANGLE
            triangleClone[i].DrawTriangle();

            // Increase speed and angle for next clone TRIANGLE
			if ((cloneSpeedInitial + cloneSpeedInterval) <= maxSpeed) {
				cloneSpeedInitial += cloneSpeedInterval;
			}
            cloneAngleInitial += cloneAngleInterval;
			yPos += yPosInterval;
        }

    }
		
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < triangleNum; i++) {
			// TRIANGLES - Speed up when pressing ] | down when pressing [
			if (Input.GetKeyDown ("]") && (triangleClone[i].speed < maxSpeed) && (triangleClone[i].speed > -maxSpeed)) {
				if (triangleClone[i].speed >= 0) {
					triangleClone[i].speed = triangleClone[i].speed + (triangleClone[i].speedIncrement);
				} else if (triangleClone[i].speed < 0) {
					triangleClone[i].speed = triangleClone[i].speed - (triangleClone[i].speedIncrement);
				}
			} else if (Input.GetKeyDown ("[") && (triangleClone[i].speed != 0)) {
				if (triangleClone[i].speed > 0) {
					triangleClone[i].speed = triangleClone[i].speed - (triangleClone[i].speedIncrement);
				} else if (triangleClone[i].speed < 0) {
					triangleClone[i].speed = triangleClone[i].speed + (triangleClone[i].speedIncrement);
				}
			}

			// TRIANGLES + BOUNDARY - Change Y-axis position of triangles when boundary moved
			if (boundaryClone [i].isMoving == true) {
				triangleClone [i].newY = boundaryClone [i].transform.position.y - boundaryYAdjust;
			}
		}

	}
		
}
