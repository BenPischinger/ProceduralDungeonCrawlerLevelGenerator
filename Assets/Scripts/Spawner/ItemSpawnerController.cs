using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Variant of the spawner controller for item spawns
public class ItemSpawnerController : MonoBehaviour
{
    public ItemRoomGridController itemRoomGrid;

    public RandomItemSpawner[] spawnerData;

    [System.Serializable]

    public struct RandomItemSpawner
    {
        public string name;

        public SpawnerData spawnerData;
    }

    private void Start()
    {

    }

    // Calls the method to spawn items for each spawner attached to a room
    public void InitialiseItemSpawning()
    {
        foreach (RandomItemSpawner randomSpawns in spawnerData)
        {
            SpawnItems(randomSpawns);
        }
    }

    // Spawns a random item at the position of an available tile from the grid
    void SpawnItems(RandomItemSpawner itemSpawnerData)
    {
        int randomIteration = Random.Range(itemSpawnerData.spawnerData.minimalSpawn, itemSpawnerData.spawnerData.maximalSpawn + 1);

        for (int i = 0; i < randomIteration; i++)
        {
            int randomPosition = Random.Range(0, itemRoomGrid.availableTiles.Count - 1);

            GameObject itemGameObject = Instantiate(itemSpawnerData.spawnerData.objectToSpawn, itemRoomGrid.availableTiles[randomPosition], Quaternion.identity, transform) as GameObject;

            itemRoomGrid.availableTiles.RemoveAt(randomPosition);
        }
    }
}
