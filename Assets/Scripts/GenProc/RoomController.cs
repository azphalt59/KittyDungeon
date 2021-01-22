using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInformation
{
    public string name;
    public int X;
    public int Y ;
}


public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    string currentWorldName = "Basement";
    RoomInformation currentLoadData;
    Queue<RoomInformation> loadRoomQueue = new Queue<RoomInformation>();
    Room currentRoom;

    public List<Room> loadedRooms = new List<Room>();
    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    void Awake() 
    {
        instance = this;
    }

    void Start() 
    {

    }
    public void LoadRoom( string name, int x, int y)
    {
        if(DoesRoomExist(x, y))
        {
            return;
        }

        RoomInformation newRoomData = new RoomInformation();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInformation info) 
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if(!DoesRoomExist(currentLoadData.X, currentLoadData.Y))
        {
        room.transform.position = new Vector3(currentLoadData.X * room.width, currentLoadData.Y * room.height, 0);
        room.X = currentLoadData.X;
        room.Y = currentLoadData.Y;
        room.name = currentWorldName + "-" + currentLoadData.name + " " + room.X + ", " + room.Y;
        room.transform.parent = transform;

        isLoadingRoom = false;
        if(loadedRooms.Count == 0)
        {
            CameraController.instance.currentRoom = room;
        }
        loadedRooms.Add(room);
        
        }
        else 
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find( item => item.X == x && item.Y == y) !=null;
    }
    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find( item => item.X == x && item.Y == y);
    }

    void Update() 
    {
        UpRoomQueue();
    }
    void UpRoomQueue()
    {
        if(isLoadingRoom)
        {
            return;
        }
        if(loadRoomQueue.Count == 0)
        {
            if(!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            } 
            else if(spawnedBossRoom && !updatedRooms)
            {
                foreach(Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }

        currentLoadData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadData));
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;

        // UpdateRooms();
    }

    /*private void UpdateRooms()
    {
        foreach(Room room in loadedRooms)
        {
            if(currentRoom !=room)
            {
                EnemyController[] enemies = room.GetComponentInChildren<EnemyController>();
                if(enemies != null)
                {
                    foreach(EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = true;
                        Debug.Log("Not in room");
                    }
                }
            }
            else
            {
                 if(currentRoom !=room)
            {
                EnemyController[] enemies = room.GetComponentInChildren<EnemyController>();
                if(enemies != null)
                {
                    foreach(EnemyController enemy in enemies)
                    {
                        enemy.notInRoom = false;
                        Debug.Log("in room");
                    }
                }
            }
            }
        }*/
    

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[] {
            "Empty",
            "Test1", 
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if(loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count -1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Y);
            
        }
    }
}
