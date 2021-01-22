using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBulletController : MonoBehaviour
{

    public float fishBulletTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LifeTimeFishBullet());
    }

    IEnumerator LifeTimeFishBullet()
    {
        yield return new WaitForSeconds(fishBulletTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().Death();
        }
    }
}
