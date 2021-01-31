using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [Header("La santé")]
    public int maxHealth;
    public int health;
    public int numberOfDammage;

    [Header("Invicibilité")]
    public bool isInvincible;
    public float invincibilityDuration;
    //public float invincibilityDeltaTime;

    [Header("UI de la santé")]
    public Image[] healthPointImage; 
    public Sprite healthPointSprite;
    public Sprite emptyHealthPointSprite;
    public GameObject model;

    public GameObject AnimationMort;
    public AudioSource miouMal;

    
    // Start is called before the first frame update
    void Start()
    {
        isInvincible = false;
        health = maxHealth/2; 
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
       /* if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy projectile" && isInvincible == false)
        {
            LoseHealth(numberOfDammage);
        } */
    }

    public void LoseHealth(int amount)
    {
        if(isInvincible) return;
        health -= amount;
        miouMal.Play();
        if(health <= 0)
        {
            miouMal.Play();
            health = 0;
            return;
        }
        //StartCoroutine(BecomeInvincible());
    }

    private IEnumerator BecomeInvincible()
    {
        Debug.Log("INVINCIBLE");
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        Debug.Log("Mortel");
        
        isInvincible = false;
    }
   

    void Update() 
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }

        for( int i = 0; i < healthPointImage.Length; i++)
        {
            if (i < health)
            {healthPointImage[i].sprite = healthPointSprite;} 
            else 
            {healthPointImage[i].sprite = emptyHealthPointSprite;}
            if (i < maxHealth)
            {healthPointImage[i].enabled = true;}
            else
            {healthPointImage[i].enabled = false;}
        }
        if (health == 0)
        {
            StartCoroutine(PlayerMort());
            // GO ECRAN DEF
        }

        IEnumerator PlayerMort(){

        AnimationMort.SetActive(true);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("BasementMain");
        
        
        }
    }
    
}
