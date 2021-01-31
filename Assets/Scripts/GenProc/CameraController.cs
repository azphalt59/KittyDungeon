using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Room currentRoom;
    public float speedWhenRoomChange;

    void Awake() 
    {
         instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if(currentRoom == null)
        {
            return;
        }
        Vector3 targetPos = GetCameraTargetPosition();
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speedWhenRoomChange);
    }

    Vector3 GetCameraTargetPosition()
    {
        if(currentRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = currentRoom.GetRoomCentre();
        targetPos.z = transform.position.z;

        return targetPos;
    }
    
    public bool isSwitchingRoom()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
