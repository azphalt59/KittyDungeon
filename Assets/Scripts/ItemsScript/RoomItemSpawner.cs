using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomItemSpawner
    {
        public string name;
        public ItemSpawnerData itemSpawnerData;
    }
    public GridController grid;
    public RandomItemSpawner[] itemSpawnerData;

   
}
