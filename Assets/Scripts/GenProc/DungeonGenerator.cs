using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    RoomController roomController;

    private void Start() 
    {
        dungeonRooms = DungeonController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> SpawnRooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach(Vector2Int roomLocation in SpawnRooms)
        {
            
                RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
            
            
        }
    }
}
