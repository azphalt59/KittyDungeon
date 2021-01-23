using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBulletController : MonoBehaviour
{

    public float fishBulletTime;
    EnemyLife enemyLife;
    EnemyController enemyController;
    FishLauncher fishLauncher;



    // Start is called before the first frame update
    void Start()
    {
        Invoke("Disable", fishBulletTime);
    }
    
    public void Disable(){

        Destroy(gameObject);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemyLife.TakeDamage(fishLauncher.fishBulletDamage);
            Debug.Log("L'ennemi prend " + fishLauncher.fishBulletDamage + " damages");
        }
    }
}