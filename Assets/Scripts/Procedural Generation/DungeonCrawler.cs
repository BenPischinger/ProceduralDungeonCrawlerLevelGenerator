using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonCrawler : MonoBehaviour
{
    public Vector3Int Position { get; set; }
    public DungeonCrawler(Vector3Int startPosition)
    {
        Position = startPosition;
    }

    // Choses a random direction for the next room
    public Vector3Int Move(Dictionary<Direction, Vector3Int> directionMap)
    {
        Direction movementDirection = (Direction)Random.Range(0, directionMap.Count);
        Position += directionMap[movementDirection];
        return Position;
    }
}
