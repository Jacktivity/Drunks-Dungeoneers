using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class BlockSpawner : MonoBehaviour 
{
    public TableGrid grid;

    public GameObject blockingPrefab;

    public int blockingCount = 10;

    private void Start()
    {
        for(int i = 0; i < blockingCount; i++)
        {
            Instantiate(blockingPrefab, transform);
            blockingPrefab.GetComponent<GridBlock>().gridLocation = new Vector2(UnityEngine.Random.Range(0, grid.sizeX), UnityEngine.Random.Range(0, grid.sizeY));
        }
    }
}
