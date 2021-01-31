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
    public int damageOnCollision;
    
    [Header ("Movement + Range")]
    public float enemySpeed;
    public float RangeVision;
    public float RangeAttack;
    public bool notInRoom = true;
    
    [Header("Bullet Stats")]
    public float reloadBulletTime;
    private bool reloadBullet = false;
    public float bulledSpeed;
    public GameObject boneBulletPrefab;

    
    
    

    private bool chooseDir = false;
    private bool isDead = false;
    private Vector3 randomDir;
    private float waitForSeconds;
    private AnimationCurve animNextPos;

    public AudioSource sonWafTir;

    
    GameObject roomController;

    [Header("Anim")]
    
    public float speedX;
    public float speedY;
    public Animator anim;
    Vector3 lastPosition = Vector3.zero;

    public BoxCollider2D boundBox;
    private Vector3 minBounds; 
    private Vector3 maxBounds;


    PlayerLife playerLife;
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boundBox = GameObject.Find("AIBound").GetComponent<BoxCollider2D>();
        sonWafTir = GameObject.Find("sonWafTir").GetComponent<AudioSource>();

        
        anim = GetComponent<Animator>();

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }

    
    void FixedUpdate()
    {
        speedX = (transform.position.x - lastPosition.x);
        speedY = (transform.position.y - lastPosition.y);
        lastPosition = transform.position;


        anim.SetFloat("SpeedY", speedY);

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

        if(minBounds.x < transform.position.x && transform.position.x < maxBounds.x){
			
			randomDir.x = randomDir.x;
			
		}else{
			
			randomDir.x = 0;
			
		}
		
		if(minBounds.y < transform.position.y && transform.position.y < maxBounds.y){
			
			randomDir.y = randomDir.y;
			
		}else{
			
			randomDir.y = 0;
			
		}


        transform.position +=  randomDir * enemySpeed * Time.deltaTime;
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
        waitForSeconds = Random.Range(1f, 4f);
        yield return new WaitForSeconds(waitForSeconds);
        randomDir = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0f);
        
        //new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
        //Vector3 nextRotation = randomDir;
        //transform.position = Vector3.Lerp(transform.position, nextRotation, animNextPos.Evaluate(5));
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
                    player.GetComponent<PlayerLife>().LoseHealth(damageOnCollision);
                    break;
                case (EnemyClass.Shooter):
                    sonWafTir.Play();
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
        
        Destroy(gameObject);
    }

    
}

