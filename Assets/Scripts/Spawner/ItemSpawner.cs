using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Variant of the monster spawner for item spawns
// Using the same weight system
public class ItemSpawner : MonoBehaviour
{
    float totalWeight;

    Vector3 positionOffset;

    public List<Spawnable> item = new List<Spawnable>();

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

        foreach (var spawnable in item)
        {
            totalWeight += spawnable.weight;
        }
    }

    // Spawns an item and updates the weight
    void Start()
    {

        float weightPool = Random.value * totalWeight;

        int index = 0;

        float cumulativeWeight = item[0].weight;

        while (weightPool > cumulativeWeight && index < item.Count - 1)
        {
            index++;

            cumulativeWeight += item[index].weight;
        }

        positionOffset = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        
        GameObject itemObject = Instantiate(item[index].gameObject, positionOffset, Quaternion.identity) as GameObject;

        itemObject.transform.parent = transform;
    }

    void Update()
    {

    }
}