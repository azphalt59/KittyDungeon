using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifferentState
{
      Idle,
      Patrol,
      Rush,
      Death,
      Attack,
};

public enum EnemyClass
{
    Rusher,
    Shooter,
};

public class EnemyController : MonoBehaviour
{
    GameObject player;
    
    [Header ("Class & State")]
    public DifferentState EnemyIsDoing = DifferentState.Idle;
    public EnemyClass enemyClass;
    
    [Header ("Movement + Range")]
    public float enemySpeed;
    public float RangeVision;
    public float RangeAttack;
    public bool notInRoom = true;
    
    [Header("Bullet Stats")]
    public float reloadBulletTime;
    private bool reloadBullet = false;
    public float bulledSpeed;
    public float bulletTimeLife;
    public int bulletDamage;
    public GameObject boneBulletPrefab;
    
    [Header("Health")]
    public int maxHealth;
    public int health;
    private int numberOfDamage;

    private bool chooseDir = false;
    private bool isDead = false;
    private Vector3 randomDir;

    
    GameObject roomController;




    PlayerLife playerLife;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        roomController = GameObject.FindGameObjectWithTag("RoomManager");
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (EnemyIsDoing)
        {
            //case(DifferentState.Idle):
            //Idle();
            //break;
            case(DifferentState.Patrol):
                Patrol();
                break;
            case(DifferentState.Rush):
                Rush();
                break;
            case(DifferentState.Death):
                break;
            case(DifferentState.Attack):
                Attack();
                break;
        }

       
        if (!notInRoom)
        {
            if (inRangeVision(RangeVision) && EnemyIsDoing != DifferentState.Death)
            {
                EnemyIsDoing = DifferentState.Rush;
            }
            else if (inRangeVision(RangeVision) == false && EnemyIsDoing != DifferentState.Death)
            {
                EnemyIsDoing = DifferentState.Patrol;
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= RangeAttack)
            {
                EnemyIsDoing = DifferentState.Attack;
            }
        }
        else
        {
                EnemyIsDoing = DifferentState.Idle;
        }
        
    }

    void Patrol()
    {
        if(!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * enemySpeed * Time.deltaTime;
        if(inRangeVision(RangeVision))
        {
            EnemyIsDoing = DifferentState.Rush;
        }
    }

    void Idle()
    {
        transform.position += new Vector3(0, 0, 0);
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(1f, 10f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
      
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }
    private IEnumerator ReloadBullet()
    {
        reloadBullet = true;
        yield return new WaitForSeconds(reloadBulletTime);
        reloadBullet = false;
    }

    public bool inRangeVision(float RangeVision)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= RangeVision;
    }

    void Rush()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemySpeed * Time.deltaTime);
    }
    void Attack()
    {
        if(!reloadBullet)
        {
            switch (enemyClass)
            {
                case (EnemyClass.Rusher):
                    player.GetComponent<PlayerLife>().LoseHealth(bulletDamage);
                    break;
                case (EnemyClass.Shooter):
                    GameObject boneBullet = Instantiate(boneBulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    boneBullet.GetComponent<BoneBulletController>().Target(player.transform);
                    boneBullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    StartCoroutine(ReloadBullet());
                    break;
            }
        }
        
        
    }
    public void Death()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        Destroy(gameObject);
    }

    
}

