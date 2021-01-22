using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLauncher : MonoBehaviour
{
    public GameObject fishBulletPrefab;
    public float bulletSpeed;
    public float fishCooldown;
    public float lastFishTimer =0;
    

    // Update is called once per frame
    void Update()
    {
        float xBulletDirection = Input.GetAxis("Horizontal");
        float yBulletDirection = Input.GetAxis("Vertical");

        if(Input.GetKeyDown("joystick button 2"))
        {
            if ((xBulletDirection != 0 || yBulletDirection != 0) && fishCooldown < lastFishTimer)
            {
                LaunchFish(xBulletDirection, yBulletDirection);
                lastFishTimer = 0;
            }
            
        }

        lastFishTimer += Time.deltaTime;
    }

    void LaunchFish(float xDir, float yDir)
    {

        GameObject fishBullet = Instantiate(fishBulletPrefab, transform.position, transform.rotation) as GameObject;
        fishBullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        fishBullet.GetComponent<Rigidbody2D>().velocity =
            new Vector3((xDir < 0) ? Mathf.Floor(xDir) * bulletSpeed : Mathf.Ceil(xDir) * bulletSpeed, (yDir < 0) ? Mathf.Floor(yDir) * bulletSpeed : Mathf.Ceil(yDir) * bulletSpeed, 0);
    }
}
