using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data for the generation of a dungeon
[CreateAssetMenu(fileName ="DungeonGenerationData.asset", menuName = "DungeonGenerationData/Dungeon Data")]
public class DungeonGenerationData : ScriptableObject
{
    public int numberOfDungeons;

    public int numberOfItemRooms;

    //public int numberOfShopRooms;

    public int iterationMinimum;

    public int iterationMaximum;
}
