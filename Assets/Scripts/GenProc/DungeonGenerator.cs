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
            if(roomLocation == dungeonRooms[dungeonRooms.Count/2] &&!(roomLocation == Vector2Int.zero))
            {
                RoomController.instance.LoadRoom("Shop", roomLocation.x, roomLocation.y);
                // roomController.UpdateRooms();
            }
            /*if (roomLocation == dungeonRooms[dungeonRooms.Count *(3/4)] && !(roomLocation == Vector2Int.zero))
            {
                RoomController.instance.LoadRoom("ItemCroquette", roomLocation.x, roomLocation.y);
                // roomController.UpdateRooms();

            }
            //if (roomLocation == dungeonRooms[dungeonRooms.Count *(1/4)] && !(roomLocation == Vector2Int.zero))
            {
                RoomController.instance.LoadRoom("ItemClaw", roomLocation.x, roomLocation.y);
                // roomController.UpdateRooms();
            }
            //if (roomLocation == dungeonRooms[dungeonRooms.Count *(2/5)] && !(roomLocation == Vector2Int.zero))
            {
                RoomController.instance.LoadRoom("ItemWool", roomLocation.x, roomLocation.y);
                // roomController.UpdateRooms();
            }*/
            else
            {
                RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
            }
            
        }
    }
}
