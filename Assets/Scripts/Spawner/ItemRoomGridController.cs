using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Variant of the regular grid script for item rooms
public class ItemRoomGridController : MonoBehaviour
{
    public Room room;

    public ItemRoomGrid itemRoomGrid;

    public GameObject gridTile;

    public List<Vector3> availableTiles = new List<Vector3>();

    [System.Serializable]

    public struct ItemRoomGrid
    {
        public int columns, rows;
        public float horizontalOffset, verticalOffset;
        public Vector3 position;
    }

    // Set up the grid for a room
    void Awake()
    {
        room = GetComponentInParent<Room>();

        itemRoomGrid.columns = 3;

        itemRoomGrid.rows = 1;

        GenerateGrid();
    }

    // Generates a more static grid by placing tiles for item spawns
    public void GenerateGrid()
    {
        itemRoomGrid.position = new Vector3(6.0f, 0.1f, itemRoomGrid.verticalOffset);

        for (int i = 0; i < 3; ++i)
        {
                GameObject gameObject = Instantiate(gridTile, transform);

                gameObject.transform.position = itemRoomGrid.position;

                gameObject.name = "x: " + itemRoomGrid.position.x + ", y: 0, z: 0";

                availableTiles.Add(gameObject.transform.position);

                itemRoomGrid.position.x -= 6;

                gameObject.SetActive(true);
        }
        GetComponentInParent<ItemSpawnerController>().InitialiseItemSpawning();
    }
}
