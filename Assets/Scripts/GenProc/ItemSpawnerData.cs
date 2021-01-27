using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/ItemSpawner")]
public class ItemSpawnerData : ScriptableObject
{
    public List<GameObject> items = new List<GameObject>();

}
