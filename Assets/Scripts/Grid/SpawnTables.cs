using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTables : MonoBehaviour {

    public List<TableObject> tables;

    public GameObject tablePrefab;

    public int tablesToSpawn = 7;

    private TableGrid grid;

    private void Start()
    {
        grid = GetComponentInParent<TableGrid>();


    }

}
