﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

    //public GameObject boundary;
    public Material material;
    public Color color = Color.white;
    public float xpos = 6;

    public float yoffset = 0.0f;
    public bool isMoving = true;
    private Mesh mesh;
    private Vector2[] meshboudries = new Vector2[3];
    private Vector3[] meshorigin = new Vector3[3];




    // Use this for initialization
    public void Start() {
        DrawBoundary(xpos, 0, 0.5f, 2, 0);
    }

    // Update is called once per frame
    void Update() {
        meshorigin = mesh.vertices;
        meshboudries = ArrayToVector2(mesh.vertices);
        //MouseOverAction();
        MouseClick();

    }

    void DrawBoundary(float x, float y, float width, float height, float z)
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        mesh = GetComponent<MeshFilter>().mesh;

        GetComponent<MeshRenderer>().material = material;

        mesh.Clear();

        mesh.vertices = new Vector3[] {
            new Vector3(x, y, z),
            new Vector3(x,  y + height, z),
            new Vector3(x + width,  y + height, z),
            new Vector3(x + width, y, z)

        };

        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
        


    }

    void MoveVertical()
    {
        Vector2 position = this.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        yoffset = mousePosition.y;

        position.y = yoffset;
        this.transform.position = position;

    }

    void MouseOverAction()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

        hitCollider.GetComponent<PolygonCollider2D>().points = meshboudries;

        if (hitCollider && hitCollider.transform.tag == "Boundary")
        {
            isMoving = true;

        } else
        {
            isMoving = false;
        }


    }

    void MouseClick()
    {
        if (Input.GetMouseButton(0)) {

            MoveVertical();
        } else if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }
    }

    Vector2 ToVector2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }

    Vector2[] ArrayToVector2(Vector3[] v3)
    {
        int length = v3.Length;
        Vector2[] newArray = new Vector2[length];

        for (int i = 0; i <= length; i++)
        {
            newArray[i] = ToVector2(v3[i]);
        }
        return newArray;
    }

}
