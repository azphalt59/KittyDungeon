using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    PlayerDmg playerDmg;
    FishBulletController fishBulletController;
    EnemyController enemyController;
    GameObject roomController;
    public int maxHealth;
    public int health;

    [Header("Milk Drop")]
    public GameObject milkPrefab;
    public float spawnRateMilk;


    void Start()
    {
        health = maxHealth;
        roomController = GameObject.FindGameObjectWithTag("RoomManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(maxHealth < health)
        {
            health = maxHealth;
        }
        if(health == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int numberOfDamage)
    {
        health -= numberOfDamage;
        if(health <= 0)
        {
            Debug.Log("Meurt");
            RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
            roomController.GetComponent<RoomController>().UpdateRooms();
            if (Random.value <= spawnRateMilk)
            {
                Debug.Log("Du lait");
                Instantiate(milkPrefab, transform.position, Quaternion.identity);
            }
            else { Debug.Log("PAS DE LAIT"); }
            health = 0;
            return;
        }
    }

    /*public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "FishBullet")
        {
            TakeDamage(playerDmg.fishDamage);
        }
    }*/
}
