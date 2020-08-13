using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to spawn a random amount of monsters into a room
// Chances of a specific monster spawning are based on their weight
public class MonsterSpawner : MonoBehaviour
{
    public int roomQueueCount;

    float totalWeight;

    public List<Spawnable> monsters = new List<Spawnable>();

    [System.Serializable]

    public struct Spawnable
    {
        public GameObject gameObject;

        public float weight;
    }

    // Initiates the total weight based on the specific spawner data
    private void Awake()
    {
        totalWeight = 0;

        foreach (var spawnable in monsters)
        {
            totalWeight += spawnable.weight;
        }
    }

    // Spawns a monster and updates the weight
    void Start()
    {
        float weightPool = Random.value * totalWeight;

        int index = 0;

        float cumulativeWeight = monsters[0].weight;

        while (weightPool > cumulativeWeight && index < monsters.Count - 1)
        {
            index++;

            cumulativeWeight += monsters[index].weight;
        }

        GameObject monsterObject = Instantiate(monsters[index].gameObject, transform.position, Quaternion.identity) as GameObject;

        monsterObject.transform.parent = transform;
    }

    void Update()
    {

    }
}