using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Script for creating a grid to spawn monsters and traps on
public class GridController : MonoBehaviour
{
    public Room room;

    public Grid grid;

    public GameObject gridTile;

    public List<Vector3> availableTiles = new List<Vector3>();

    [System.Serializable]

    // Struct to define columns, rows and the offset for placement within a room
    public struct Grid
    {
        public int columns, rows;
        public float horizontalOffset, verticalOffset;
    }

    // Set up the grid for a room
    void Awake()
    {
        room = GetComponentInParent<Room>();

        grid.columns = 4;

        grid.rows = 4;

        GenerateGrid();
    }

    // Generates the script by evenly spacing out and placing tiles based on the room size
    public void GenerateGrid()
    {
        grid.horizontalOffset += room.transform.localPosition.x;
        grid.verticalOffset += room.transform.localPosition.z;

        for(int x = 0; x < 20; x+=5)
        {
            for(int z = 0; z < 20; z+=5)
            {
                GameObject gameObject = Instantiate(gridTile, transform);

                gameObject.transform.position = new Vector3 (x - (grid.columns - grid.horizontalOffset), 0.1f, z - (grid.rows - grid.verticalOffset));

                gameObject.name = "x: " + x + ", y: 0, z: " + z;

                availableTiles.Add(gameObject.transform.position);

                gameObject.SetActive(true);
            }
        }
        GetComponentInParent<SpawnerController>().InitialiseObjectSpawning();
    }
}
