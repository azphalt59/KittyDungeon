using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }

    public GridController grid;
    public RandomSpawner[] spawnerData;
    RoomController roomController;

    void Start() 
    {
        
    }

    public void InitialiseObjectSpawning()
    {
        //StartCoroutine(WaitDungeonSpawn());
        foreach (RandomSpawner rs in spawnerData)
        {
            SpawnObjects(rs);
        }
    }

    IEnumerator WaitDungeonSpawn()
    {
        yield return new WaitForSeconds(2f);
        foreach (RandomSpawner rs in spawnerData)
        {
            SpawnObjects(rs);
        }
    }
        
    public void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minToSpawn, data.spawnerData.maxToSpawn +1);
        
        {
            for (int i = 0; i < randomIteration; i++)
            {
                int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
                GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.availablePoints[randomPos], Quaternion.identity, transform) as GameObject;
                grid.availablePoints.RemoveAt(randomPos);
            }
        }
            
    }
}
