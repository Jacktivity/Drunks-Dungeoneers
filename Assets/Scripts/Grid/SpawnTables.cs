using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTables : MonoBehaviour {

    public List<TableObject> tables;

    public GameObject tablePrefab;

    public List<Vector2> locations;

    public int numberOfTablesToSpawn = 7;

    private TableGrid grid;

    private void Start()
    {
        grid = GetComponentInParent<TableGrid>();

        while(numberOfTablesToSpawn > 0)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(0, grid.sizeX), Random.Range(0, grid.sizeY));
            TableObject tableToSpawn = tables[Random.Range(0, tables.Count)];

            if(!grid.CheckIfcanSpawn(spawnPosition, tableToSpawn.addedLocations))
            {
                //get a new position table combo
                continue;
            }

            bool canSpawn = true;

            foreach(Vector2 chairLocation in tableToSpawn.seatLocations)
            {
                Vector2 charLocation = new Vector2(spawnPosition.x + chairLocation.x, spawnPosition.y + chairLocation.y);
                if (!grid.CheckIfcanSpawn(charLocation, tableToSpawn.chair.addedLocations))
                {
                    canSpawn = false;
                    break;
                }
            }

            if (canSpawn)
            {
                //Spawn the table
                GameObject table = (GameObject)Instantiate(tablePrefab, transform);
                OTable tableObject = table.GetComponent<OTable>();
                tableObject.location = spawnPosition;
                tableObject.tableType = tableToSpawn;

                //Set the table spawn location to blocking
                grid.AddTileToLocation(spawnPosition, TileContent.Blocking);

                locations.Add(spawnPosition);

                //if the table is bigger than one square set all tiles as blocking
                Vector2 location;
                foreach (Vector2 tableLocations in tableToSpawn.addedLocations)
                {
                    location = new Vector2(spawnPosition.x + tableLocations.x, spawnPosition.y + tableLocations.y);

                    grid.AddTileToLocation(location, TileContent.Blocking);
                }

                //Set all seat tiles to seat
                foreach (Vector2 chairLocation in tableToSpawn.seatLocations)
                {
                    location = new Vector2(spawnPosition.x + chairLocation.x, spawnPosition.y + chairLocation.y);

                    grid.AddTileToLocation(location, TileContent.Seat);

                    Vector2 longChairLocation;
                    foreach(Vector2 extendedSeat in tableToSpawn.chair.addedLocations)
                    {
                        longChairLocation = new Vector2(location.x + extendedSeat.x, location.y + extendedSeat.y);

                        grid.AddTileToLocation(longChairLocation, TileContent.Seat);
                    }
                }

                numberOfTablesToSpawn--;
            }

        }

    }

}
