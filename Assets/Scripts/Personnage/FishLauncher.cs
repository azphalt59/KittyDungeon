using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLauncher : MonoBehaviour
{
    public GameObject fishBulletPrefab;
    
    public float bulletSpeed;
    public float fishCooldown;
    public float lastFishTimer =0;

    private float xBulletDirection;
    private float yBulletDirection;

    public AudioSource sonMiouTir;
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
           
            xBulletDirection  = Input.GetAxis("Horizontal");
            yBulletDirection = Input.GetAxis("Vertical");

        }

        if (Input.GetKeyDown("joystick button 0"))
        {
            if ((xBulletDirection != 0 || yBulletDirection != 0) && fishCooldown < lastFishTimer)
            {
                //sonMiouTir.Play();
                LaunchFish(xBulletDirection, yBulletDirection);
                lastFishTimer = 0;
            }

        }

        if (Input.GetKeyDown(KeyCode.K))
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
        fishBullet.transform.Rotate(0f, 0f, (xDir < 0) ?  90 : ((xDir > 0) ? -90 : 0), Space.World);
        fishBullet.transform.Rotate(0f, 0f, (yDir < 0) ?  90 :  -90, Space.World);
        fishBullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        fishBullet.GetComponent<Rigidbody2D>().velocity =
            new Vector3((xDir < 0) ? Mathf.Floor(xDir) * bulletSpeed : Mathf.Ceil(xDir) * bulletSpeed, (yDir < 0) ? Mathf.Floor(yDir) * bulletSpeed : Mathf.Ceil(yDir) * bulletSpeed, 0);
    }
}
