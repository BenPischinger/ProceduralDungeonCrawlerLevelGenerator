using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Variant of the monster spawner for boss spawns
// Using the same weight system
public class BossSpawner : MonoBehaviour
{
    float totalWeight;

    public List<Spawnable> boss = new List<Spawnable>();

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

        foreach (var spawnable in boss)
        {
            totalWeight += spawnable.weight;
        }
    }

    // Spawns an boss and updates the weight
    void Start()
    {
        float weightPool = Random.value * totalWeight;

        int index = 0;

        float cumulativeWeight = boss[0].weight;

        while (weightPool > cumulativeWeight && index < boss.Count - 1)
        {
            index++;

            cumulativeWeight += boss[index].weight;
        }

        GameObject bossObject = Instantiate(boss[index].gameObject, transform.position, Quaternion.identity) as GameObject;

        bossObject.transform.parent = transform;
        
    }

    void Update()
    {

    }
}