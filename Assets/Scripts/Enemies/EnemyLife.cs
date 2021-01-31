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
    public AudioSource wafMal;
    public GameObject AnimationMort;

    public AudioSource Explosion;


    [Header("Milk Drop")]
    public GameObject milkPrefab;
    public float spawnRateMilk;


    void Start()
    {
        health = maxHealth;
        roomController = GameObject.FindGameObjectWithTag("RoomManager");
        wafMal = GameObject.Find("sonWafMal").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(maxHealth < health)
        {
            health = maxHealth;
        }
        if(health == 0 )
        {
            StartCoroutine(Mort());
        }
    }

    public void TakeDamage(int numberOfDamage)
    {
        health -= numberOfDamage;
        wafMal.Play();

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

    IEnumerator Mort ()
    {
        
        AnimationMort.SetActive(true);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }

    /*public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "FishBullet")
        {
            TakeDamage(playerDmg.fishDamage);
        }
    }*/
}
