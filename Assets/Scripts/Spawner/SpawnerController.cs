using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for the handling of object spawning
public class SpawnerController : MonoBehaviour
{
    public GridController grid;

    public RandomSpawner[] spawnerData;

    [System.Serializable]

    public struct RandomSpawner
    {
        public string name;

        public SpawnerData spawnerData;
    }

    private void Start()
    {
        
    }

    // Calls the method to spawn objects for each spawner attached to a room
    public void InitialiseObjectSpawning()
    {
        foreach (RandomSpawner randomSpawns in spawnerData)
        {
            SpawnObjects(randomSpawns);
        }
    }

    // Spawns a random object at the position of an available tile from the grid
    void SpawnObjects(RandomSpawner spawnerData)
    {
        int randomIteration = Random.Range(spawnerData.spawnerData.minimalSpawn, spawnerData.spawnerData.maximalSpawn + 1);

        for(int i = 0; i < randomIteration; i++)
        {
            int randomPosition = Random.Range(0, grid.availableTiles.Count - 1);
           
            if (spawnerData.name == "Boss Spawner")
            {
                GameObject gameObject = Instantiate(spawnerData.spawnerData.objectToSpawn, new Vector3(0, 0, 0), Quaternion.identity, transform) as GameObject;
            }
            else
            {
                GameObject gameObject = Instantiate(spawnerData.spawnerData.objectToSpawn, grid.availableTiles[randomPosition], Quaternion.identity, transform) as GameObject;
            }

            grid.availableTiles.RemoveAt(randomPosition);
        }
    }
}
