using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawner")]

public class SpawnerData : ScriptableObject
{
    public GameObject objectToSpawn;

    public int minimalSpawn;

    public int maximalSpawn;
}
