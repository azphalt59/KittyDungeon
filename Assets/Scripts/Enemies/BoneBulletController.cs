using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneBulletController : MonoBehaviour
{

    [Header("Position")]
    Vector2 target;
    Vector2 pos;
    Vector2 posLastFrame;

    [Header("Stats")]
    public float bulletTime;
    public int bulletDammage;
    public float bulletSpeed;

    

    PlayerLife playerLife;
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifeTime());
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(bulletTime);
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
            other.gameObject.GetComponent<PlayerLife>().LoseHealth(bulletDammage);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        pos = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target, bulletSpeed * Time.deltaTime);
        if(pos == posLastFrame)
        {
            Destroy(gameObject);
        }
        posLastFrame = pos;
    }



}
