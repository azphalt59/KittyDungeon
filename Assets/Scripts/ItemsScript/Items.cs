using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [System.Serializable]
    public struct ItemsSpawnable
    {
        public GameObject gameObject;
        public float rarity;
    }

    public List<ItemsSpawnable> items = new List<ItemsSpawnable>();
    float totalRarity;

    void Awake()
    {
        totalRarity = 0;
        foreach(var spawnable in items)
        {
            totalRarity += spawnable.rarity;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        float loot = Random.value * totalRarity;
        int chosenIndex = 0;
        float cumulativeRarity = items[0].rarity;
        while (loot > cumulativeRarity && chosenIndex < items.Count - 1)
        {
            chosenIndex++;
            cumulativeRarity += items[chosenIndex].rarity;
        }
         GameObject item = Instantiate(items[chosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
