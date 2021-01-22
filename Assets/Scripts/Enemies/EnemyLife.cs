using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [Header("La santé")]
    public int maxHealth;
    public int health;
    public int numberOfDamage;

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
            Destroy(this);
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
}
