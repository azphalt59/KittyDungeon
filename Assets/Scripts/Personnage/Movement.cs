using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Animator anim;
    public float speed;
    public int direction;

    private float LastMX;
    private float LastMY;

    public bool isMoving;

    ItemsController itemsController;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    


    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        isMoving = true;
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
           
            LastMX  = Input.GetAxis("Horizontal");
            LastMY = Input.GetAxis("Vertical");

        }

        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0){
            isMoving = false;
        }

        anim.SetFloat("MoveX", Input.GetAxis("Horizontal"));
        anim.SetFloat("MoveY",Input.GetAxis("Vertical"));
        anim.SetFloat("LastMoveX", LastMX);
        anim.SetFloat("LastMoveY", LastMY);
        anim.SetBool("IsMoving", isMoving);

        transform.position += movement * speed * Time.deltaTime;
  
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Wall")
            {Debug.Log("MUR DANS LA FACE");}
    }
    public void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "TriggerZone")
        {
          
        }
        if (other.tag == "objet")
        {
            
        }
    }

}
