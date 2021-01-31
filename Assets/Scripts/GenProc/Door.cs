using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left, right, top, bottom, middle
    }

    public DoorType doorType;
    public GameObject doorCollider;
    public GameObject doorOpen;
    public GameObject triggerCollider;
    public GameObject triggerZoneCloseDoor;
    public GameObject wallSiPasDePorte;
    public Animator animDoorOpen;
    public Animator animDoorClose;
    public bool canOpen = true;
    public bool canClose = true;
    private GameObject player;
    private float widthOffset = 0;
    private float xOffset = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch(doorType)
            {
                case DoorType.bottom:
                    //player.transform.position = new Vector2(transform.position.x, transform.position.y - widthOffset);
                    //Debug.Log("offset");
                    break;
                case DoorType.left:
                    //player.transform.position = new Vector2(transform.position.x- xOffset, transform.position.y);
                    //Debug.Log("offset");
                    break;
                case DoorType.right:
                    //player.transform.position = new Vector2(transform.position.x + xOffset, transform.position.y);
                    //Debug.Log("offset");
                    break;
                case DoorType.top:
                    //player.transform.position = new Vector2(transform.position.x, transform.position.y + widthOffset);
                    //Debug.Log("offset");
                    break;
                case DoorType.middle:
                    Debug.Log("On peut ouvrir");
                    canClose = true;
                    break;
                    
            }


        }
    }
}
