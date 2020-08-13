using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to create each individual room
public class Room : MonoBehaviour
{
    public int width, height, depth, roomX, roomY, roomZ;

    private bool doorsUpdated = false;

    // Setting up doors and walls
    public DoorController leftDoor, rightDoor, topDoor, bottomDoor, leftWall, rightWall, topWall, bottomWall;

    public List<DoorController> doorList = new List<DoorController>();

    public Room(int x, int y, int z)
    {
        roomX = x;
        roomY = y;
        roomZ = z;
    }

    // Draws a rectangle around the rooms to draw a grid which replaces the world coordinates
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, 0, depth));
       
    }

    // Start method places doors or walls depending on neighbouring rooms
    private void Start()
    {
        if (RoomController.roomControllerInstance == null)
        {
            Debug.Log("Unless you're testing individual rooms, you're in the wrong scene. \n Use the 'DungeonMain' scene to generate a dungeon.");
            return;
        }

        DoorController[] doors = GetComponentsInChildren<DoorController>();

        foreach (DoorController door in doors)
        {
            doorList.Add(door);
            switch (door.doorType)
            {
                case DoorController.DoorType.leftDoor:
                    leftDoor = door;
                    break;

                case DoorController.DoorType.rightDoor:
                    rightDoor = door;
                    break;

                case DoorController.DoorType.topDoor:
                    topDoor = door;
                    break;

                case DoorController.DoorType.bottomDoor:
                    bottomDoor = door;
                    break;

                case DoorController.DoorType.leftWall:
                    leftWall = door;
                    break;

                case DoorController.DoorType.rightWall:
                    rightWall = door;
                    break;

                case DoorController.DoorType.topWall:
                    topWall = door;
                    break;

                case DoorController.DoorType.bottomWall:
                    bottomWall = door;
                    break;
            }
        }
        RoomController.roomControllerInstance.CheckForOverlap(this);
    }

    // Methods to determine whether a room has a neighbour or not
    public Room GetLeft()
    {
        if (RoomController.roomControllerInstance.DoesRoomExist(roomX - 1, 0, roomZ))
        {
            return RoomController.roomControllerInstance.FindRoom(roomX - 1, 0, roomZ);
        }
        return null;
    }

    public Room GetRight()
    {
        if (RoomController.roomControllerInstance.DoesRoomExist(roomX + 1, 0, roomZ))
        {
            return RoomController.roomControllerInstance.FindRoom(roomX + 1, 0, roomZ);
        }
        return null;
    }

    public Room GetTop()
    {
        if (RoomController.roomControllerInstance.DoesRoomExist(roomX, 0, roomZ + 1))
        {
            return RoomController.roomControllerInstance.FindRoom(roomX, 0, roomZ + 1);
        }
        return null;
    }

    public Room GetBottom()
    {
        if (RoomController.roomControllerInstance.DoesRoomExist(roomX, 0, roomZ - 1))
        {
            return RoomController.roomControllerInstance.FindRoom(roomX, 0, roomZ - 1);
        }
        return null;
    }

    // Checks which sides of the room need doors and which need walls and activates the GameObjects accordingly
    public void PlaceDoors()
    {
        foreach (DoorController door in doorList)
        {
            switch (door.doorType)
            {
                case DoorController.DoorType.leftDoor:
                    if (GetLeft() == null)
                        door.gameObject.SetActive(false);
                    break;

                case DoorController.DoorType.rightDoor:
                    if (GetRight() == null)
                        door.gameObject.SetActive(false);
                    break;

                case DoorController.DoorType.topDoor:
                    if (GetTop() == null)
                        door.gameObject.SetActive(false);
                    break;

                case DoorController.DoorType.bottomDoor:
                    if (GetBottom() == null)
                        door.gameObject.SetActive(false);
                    break;

                case DoorController.DoorType.leftWall:
                    if (GetLeft() != null)
                        door.gameObject.SetActive(false);
                    break;

                case DoorController.DoorType.rightWall:
                    if (GetRight() != null)
                        door.gameObject.SetActive(false);
                    break;

                case DoorController.DoorType.topWall:
                    if (GetTop() != null)
                        door.gameObject.SetActive(false);
                    break;

                case DoorController.DoorType.bottomWall:
                    if (GetBottom() != null)
                        door.gameObject.SetActive(false);
                    break;
            }
        }

    }

    // Returns the center coordinates of the room
    public Vector3 getRoomCenter()
    {
        return new Vector3(roomX * width, roomY * height, roomZ * depth);
    }

    // Boss room every frame to determine when the generation is finished and doors or walls can be placed
    void Update()
    {
        if (name.Contains("BossRoom") && !doorsUpdated)
        {
            PlaceDoors();
            doorsUpdated = true;
        }
    }
}
