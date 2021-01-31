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
    bool spawnedItemRoom = false;
    bool spawnItem1 = false;
    bool spawnItem2 = false;
    bool spawnItem3 = false;
    bool updatedRooms = false;
    public float updateRoomsRefresh;
    public float uptadeRoomsDuration;
    public float currentTime;

    public bool canSpawnItem = false;


    public Animator anim;
    

    void Awake() 
    {
        instance = this;
    }

    void Start() 
    {   
        anim = GameObject.Find("Fade").GetComponent<Animator>();
        StartCoroutine(UpdateRoutineRooms());
        
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
        currentTime += Time.deltaTime;
    }

    public IEnumerator UpdateRoutineRooms()
    {
        yield return new WaitForSeconds(updateRoomsRefresh);
        UpdateRooms();
        if (currentTime < uptadeRoomsDuration)
        {
            StartCoroutine(UpdateRoutineRooms());
        }
        
    }
    public IEnumerator RoomCoroutine()
    {
        yield return new WaitForSeconds(1f);
        UpdateRooms();
    }

    public void FadeOff(){

        anim.SetBool("OnFade", false);

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

                UpdateRooms();
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

        anim.SetBool("OnFade", true);
        Invoke("FadeOff", 0.1f);

        Debug.Log("e");


        StartCoroutine(RoomCoroutine());
    }

    public void UpdateRooms()
    {
        // Debug.Log("Rooms Update");
        foreach (Room room in loadedRooms)
        {
            if (currentRoom != room)
            {
                EnemyController[] ennemies = room.GetComponentsInChildren<EnemyController>();
                if (ennemies != null)
                {
                    foreach (EnemyController enemy in ennemies)
                    {
                        enemy.notInRoom = true;
                    } 
                    foreach(Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                        door.doorOpen.SetActive(true);
                        door.canClose = false;
                        door.canOpen = true;
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                        door.doorOpen.SetActive(true);
                        door.canClose = false;
                        door.canOpen = true;
                        door.triggerCollider.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
            }
            
            else
            {
                EnemyController[] ennemies = room.GetComponentsInChildren<EnemyController>();
                if (ennemies.Length > 0)
                {
                    foreach (EnemyController enemy in ennemies)
                    {
                        enemy.notInRoom = false;
                    } 
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(true);
                        door.doorOpen.SetActive(false);
                        door.triggerCollider.GetComponent<BoxCollider2D>().enabled = false;
                        door.triggerZoneCloseDoor.GetComponent<BoxCollider2D>().enabled = true;
                        if (door.canClose == true)
                        {
                            door.canClose = false;
                            door.animDoorClose.Play("animDoorClose");
                            Debug.Log("LES PORTES SE FERMENT");
                        }
                        // Anim des portes qui se ferment (anim � l'envers)
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorOpen.SetActive(true);
                        door.doorCollider.SetActive(false);
                        door.triggerZoneCloseDoor.GetComponent<BoxCollider2D>().enabled = false;
                        door.triggerCollider.GetComponent<BoxCollider2D>().enabled = true;
                        

                        if (door.canOpen == true)
                        {
                            door.canOpen = false;
                            door.animDoorOpen.Play("animDoorOpen");
                            Debug.Log("Les portes s'ouvrent");
                        }
                        
                        
                        // Anim des portes qui s'ouvrent
                    }
                }
            }
        }
    }

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[] {
            "1Orange3Violet",
           "1Orange22",
           "2Gris2Violet",
           "3Gris",
           "3Orange",
           "3Violet",
           "111"

        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    IEnumerator SpawnBossRoom()
    {

        yield return new WaitForSeconds(0.5f);

        canSpawnItem = true;
        
        spawnedBossRoom = true;
        if (loadRoomQueue.Count == 0 && spawnItem1 == false)
        {
            spawnItem1 = true;
            //Wool Room
            Room woolRoom = loadedRooms[loadedRooms.Count - loadRoomQueue.Count - Random.Range(2,5)];
            Room woolTempRoom = new Room(woolRoom.X, woolRoom.Y);
            Destroy(woolRoom.gameObject);
            var woolRoomToRemove = loadedRooms.Single(r => r.X == woolTempRoom.X && r.Y == woolTempRoom.Y);
            loadedRooms.Remove(woolRoomToRemove);
            LoadRoom("ItemWool", woolTempRoom.X, woolTempRoom.Y);

            //Claw Room
            Room clawRoom = loadedRooms[loadedRooms.Count - loadRoomQueue.Count - Random.Range(6, 8)];
            Room clawTempRoom = new Room(clawRoom.X, clawRoom.Y);
            Destroy(clawRoom.gameObject);
            var clawRoomToRemove = loadedRooms.Single(r => r.X == clawTempRoom.X && r.Y == clawTempRoom.Y);
            loadedRooms.Remove(clawRoomToRemove);
            LoadRoom("ItemClaw", clawTempRoom.X, clawTempRoom.Y);

            //Croquette Room
            Room croquetteRoom = loadedRooms[loadedRooms.Count - loadRoomQueue.Count - Random.Range(11, 13)];
            Room croquetteTempRoom = new Room(croquetteRoom.X, croquetteRoom.Y);
            Destroy(croquetteRoom.gameObject);
            var croquetteRoomToRemove = loadedRooms.Single(r => r.X == croquetteTempRoom.X && r.Y == croquetteTempRoom.Y);
            loadedRooms.Remove(croquetteRoomToRemove);
            LoadRoom("ItemCroquette", croquetteTempRoom.X, croquetteTempRoom.Y);

            //Boss Room
            /*Room bossRoom = loadedRooms[loadedRooms.Count - 3];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Y);*/
            
            
        }
        
    }
}
