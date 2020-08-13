using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that generates the rooms for the dungeon, starting with the "StartRoom" scene
public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;

    private List<Vector3Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DungeonController.GenerateDungeon(dungeonGenerationData);

        SpawnRooms(dungeonRooms);

    }

    private void SpawnRooms(IEnumerable<Vector3Int> rooms)
    {
        RoomController.roomControllerInstance.LoadRoomQueue("StartRoom", 0, 0, 0);

        foreach(Vector3Int roomLocation in rooms)
        {
           
            RoomController.roomControllerInstance.LoadRoomQueue(RoomController.roomControllerInstance.GetRandomRoom(), roomLocation.x, 0, roomLocation.z);
                       
        }

    }
}
