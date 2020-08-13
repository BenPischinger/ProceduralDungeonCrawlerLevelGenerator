using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Variant of the monster spawner for trap spawns
// Using the same weight system
public class TrapSpawner : MonoBehaviour
{
    public int roomQueueCount;

    float totalWeight;

    public List<Spawnable> traps = new List<Spawnable>();

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

        foreach (var spawnable in traps)
        {
            totalWeight += spawnable.weight;
        }
    }

    // Spawns a trap and updates the weight
    void Start()
    {
        float weightPool = Random.value * totalWeight;

        int index = 0;

        float cumulativeWeight = traps[0].weight;

        while (weightPool > cumulativeWeight && index < traps.Count - 1)
        {
            index++;

            cumulativeWeight += traps[index].weight;
        }

        GameObject trapObject = Instantiate(traps[index].gameObject, transform.position, Quaternion.identity) as GameObject;

        trapObject.transform.parent = transform;
    }

    void Update()
    {
        
    }
}
