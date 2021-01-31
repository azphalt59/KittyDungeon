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
    private GameObject player;
    public AudioSource sonTirFish;


    // Start is called before the first frame update
    void Start()

    {
        sonTirFish = this.gameObject.GetComponent<AudioSource>();
        sonTirFish.Play();
        player = GameObject.FindGameObjectWithTag("Player");
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
            other.GetComponent<EnemyLife>().TakeDamage(player.GetComponent<PlayerDmg>().fishDamage);
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "BulletCollider")
        {
            Destroy(this.gameObject);
        }
    }
}