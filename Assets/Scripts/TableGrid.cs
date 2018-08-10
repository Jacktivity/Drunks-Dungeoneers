using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableGrid : MonoBehaviour {
    public int xSize, ySize;
    private int xSizePos, ySizePos;
    //public int startPosX, startPosY;

    private Vector3[] vertices;

    // Use this for initialization
    void Start() {
        xSizePos = (int)transform.position.x + xSize;

        ySizePos = (int)transform.position.y + ySize;

        GenerateGrid();
    }

    private void GenerateGrid() {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = (int)transform.position.y; y <= ySizePos; y++)
        {
            for (int x = (int)transform.position.x; x <= xSizePos; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
            }
        }
    }

    private void OnDrawGizmos() {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
