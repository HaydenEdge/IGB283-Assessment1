using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCloneInitiator : MonoBehaviour {

    public int triangleNum = 2;
	private int colorChangerNum = 3;
	public float maxSpeed = 20.0f;

	public float yPos = -4.0f;
	public float yPosInterval = 5.0f;
	private float colorXPos = 8.0f;
	private float colorXPosInterval = 1.0f;
	private float boundaryYAdjust = 0.5f;
	private float boundaryXAdjust = -4.0f;
	private float boundaryminXAdjust = -0.5f;

    public float cloneSpeedInitial;
    public float cloneSpeedInterval;
    public float cloneAngleInitial;
    public float cloneAngleInterval;

    public GameObject triangle;
	public GameObject boundary;
	public GameObject colorChangerObject;
	public Material material;

	private IGB283Transform[] triangleClone;
	private Boundary[] boundaryClone;
	private ColorChanger[] colorChangerObjectClone;
    private GameObject transformClass;
	private GameObject boundaryClass;
	private GameObject colorChangerClass;
	private Material newMaterial;


    // Use this for initialization
    void Start () {

        // TRIANGLES - Initialise clone array
        triangleClone = new IGB283Transform[triangleNum];

		// BOUNDARY - Initialise clone array
		boundaryClone = new Boundary[triangleNum * 2];
        
		// TRIANGLES + BOUNDARY - Create clone        
        for (int i = 0; i < triangleNum; i++) {

			// Initialise TRIANGLE + material
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

			// Update variables that need to be increased
            cloneAngleInitial += cloneAngleInterval;
			yPos += yPosInterval;
        }
			
		// COLOR CHANGER - Initialise clone array - one for each color
		colorChangerObjectClone = new ColorChanger[colorChangerNum];

		// COLOR CHANGER - Create clone        
		for (int i = 0; i < colorChangerNum; i++) {

			// Initialise Color Changer + material
			colorChangerClass = Instantiate(colorChangerObject, new Vector3(colorXPos, -5.0f, 0.0f), Quaternion.identity);
			newMaterial = Instantiate(material);

			// Create instance of Color Changer in colorChangerObjectClone[i]
			colorChangerObjectClone[i] = colorChangerClass.GetComponent<ColorChanger>();
			colorChangerObjectClone[i].material = material;

			// Name each instance
			if (i == 0) {
				colorChangerObjectClone [i].name = "Red";
			} else if (i == 1) {
				colorChangerObjectClone [i].name = "Green";
			} else if (i == 2) {
				colorChangerObjectClone [i].name = "Blue";
			}

			// Update variables that need to be increased
			colorXPos += colorXPosInterval;
		}
    }
		
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < triangleNum; i++) {

			// TRIANGLES - Speed up when pressing ]
			if (Input.GetKeyDown ("]") && (triangleClone[i].speed < maxSpeed) && (triangleClone[i].speed > -maxSpeed)) {
				if (triangleClone[i].speed >= 0) {
					triangleClone[i].speed = triangleClone[i].speed + (triangleClone[i].speedIncrement);
				} else if (triangleClone[i].speed < 0) {
					triangleClone[i].speed = triangleClone[i].speed - (triangleClone[i].speedIncrement);
				}
			
			// TRIANGLES - Slow down when pressing [
			} else if (Input.GetKeyDown ("[") && (triangleClone[i].speed != 0)) {
				if (triangleClone[i].speed > 0) {
					triangleClone[i].speed = triangleClone[i].speed - (triangleClone[i].speedIncrement);
				} else if (triangleClone[i].speed < 0) {
					triangleClone[i].speed = triangleClone[i].speed + (triangleClone[i].speedIncrement);
				}
			}

			// TRIANGLES + BOUNDARY - Change Y-axis position of triangles when boundary is moved
			if (boundaryClone [i].isMoving == true) {
				triangleClone [i].newY = boundaryClone [i].transform.position.y - boundaryYAdjust;
			}

			// TRIANGLES + COLORCHANGER - For Red/Green/Blue send message to triangles to swap color calculation if needed
			if (colorChangerObjectClone [0].isActive == true) {
				triangleClone [i].redLeft = true;
			} else {
				triangleClone [i].redLeft = false;
			}

			if (colorChangerObjectClone [1].isActive == true) {
				triangleClone [i].greenLeft = true;
			} else {
				triangleClone [i].greenLeft = false;
			}

			if (colorChangerObjectClone [2].isActive == true) {
				triangleClone [i].blueLeft = true;
			} else {
				triangleClone [i].blueLeft = false;
			}

		}

	}
		
}
