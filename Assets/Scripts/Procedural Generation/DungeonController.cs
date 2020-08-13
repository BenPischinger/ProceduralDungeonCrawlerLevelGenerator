using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Directions for the placement of rooms
public enum Direction
{
    xLeft = 0,
    xRight = 1,
    zUp = 2,
    zDown = 3
};

// Iterations for the dungeon generation based on the direction map
public class DungeonController : MonoBehaviour

{
    public static List<Vector3Int> positions = new List<Vector3Int>();

    private static readonly Dictionary<Direction, Vector3Int> directionMap = new Dictionary<Direction, Vector3Int>
    {
        {Direction.xLeft, Vector3Int.left },
        {Direction.xRight, Vector3Int.right },
        {Direction.zUp, new Vector3Int (0, 0, 1)},
        {Direction.zDown, new Vector3Int(0, 0, -1)}
    };

    public static List<Vector3Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrawler> dungeonList = new List<DungeonCrawler>();

        for (int i = 0; i < dungeonData.numberOfDungeons; i++)
        {
            dungeonList.Add(new DungeonCrawler(Vector3Int.zero));
        }

        int iterations = Random.Range(dungeonData.iterationMinimum, dungeonData.iterationMaximum);

        for(int i = 0; i < iterations; i++)
        {
            foreach (DungeonCrawler dungeonCrawler in dungeonList)
            {
                Vector3Int newPosition = dungeonCrawler.Move(directionMap);
                positions.Add(newPosition);
            }
        }

        return positions;

    }
}
