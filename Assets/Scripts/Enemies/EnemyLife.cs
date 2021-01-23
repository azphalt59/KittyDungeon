using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    EnemyController enemyController;


    void Start()
    {
        enemyController.health = enemyController.maxHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyController.maxHealth < enemyController.health)
        {
            enemyController.health = enemyController.maxHealth;
        }
        if(enemyController.health == 0)
        {
           enemyController.EnemyIsDoing = DifferentState.Death;
        }
    }

    public void TakeDamage(int numberOfDamage)
    {
        enemyController.health -= numberOfDamage;
        if(enemyController.health <= 0)
        {
            enemyController.health = 0;
            return;
        }
    }
}
