
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner.asset", menuName = "Spawners/Spawner")]
public class SpawnerData : ScriptableObject
{
    public GameObject itemToSpawn;
    public int minToSpawn;
    public int maxToSpawn;
}
