using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Possible door types for a room
public class DoorController : MonoBehaviour
{
   public enum DoorType
    {
        leftDoor, rightDoor, topDoor, bottomDoor, leftWall, rightWall, topWall, bottomWall
    }

    public DoorType doorType;
}
