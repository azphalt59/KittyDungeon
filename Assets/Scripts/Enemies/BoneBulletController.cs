using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneBulletController : MonoBehaviour
{

    [Header("Position")]
    Vector2 target;
    Vector2 pos;
    Vector2 posLastFrame;
    Transform targetTransform;

    [Header("BulletStats")]
    public float bulletTimeLife;
    public int bulletDamage;
    public float bulletSpeed;

    
    PlayerLife playerLife;
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifeTime());
        
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(bulletTimeLife);
        Destroy(gameObject);
    }
    public void Target(Transform player)
    {
        target = player.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerLife>().LoseHealth(bulletDamage);
            Destroy(gameObject);
        }
        if(other.tag == "BulletCollider")
        {
            Destroy(this.gameObject);        
        }
    }

    void Update()
    {
        pos = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target, bulletSpeed * Time.deltaTime);
        if (pos == posLastFrame)
        {
            Destroy(gameObject);
        }
        posLastFrame = pos;
    }



}
