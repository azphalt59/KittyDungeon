using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBulletController : MonoBehaviour
{

    public float fishBulletTime;
    EnemyLife enemyLife;
    EnemyController enemyController;
    FishLauncher fishLauncher;
    PlayerDmg playerDmg;
    public int fishBulletDamage;



    // Start is called before the first frame update
    void Start()
    {
        Invoke("Disable", fishBulletTime);
    }
    
    public void Disable(){

        Destroy(gameObject);

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Enemy took " + fishBulletDamage + " damage");
            other.GetComponent<EnemyLife>().TakeDamage(fishBulletDamage);
        }
    }
}