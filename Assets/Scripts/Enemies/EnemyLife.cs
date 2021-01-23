using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    PlayerDmg playerDmg;
    FishBulletController fishBulletController;
    EnemyController enemyController;
    public int maxHealth;
    public int health;


    void Start()
    {
        health = maxHealth;   
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
