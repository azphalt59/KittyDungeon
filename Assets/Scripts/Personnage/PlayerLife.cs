using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [Header("La santé")]
    public int maxHealth;
    public int health;
    public int numberOfDammage;

    [Header("Invicibilité")]
    public bool isInvincible;
    public float invincibilityDuration;
    public float invincibilityDeltaTime;

    [Header("UI de la santé")]
    public Image[] healthPointImage;
    public Sprite healthPointSprite;
    public Sprite emptyHealthPointSprite;
    public GameObject model;

    
    // Start is called before the first frame update
    void Start()
    {
        isInvincible = false;
        health = maxHealth; 
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
        if(health <= 0)
        {
            health = 0;
            return;
        }
        StartCoroutine(BecomeInvincible());
    }

    private IEnumerator BecomeInvincible()
    {
        //Debug.Log("INVINCIBLE");
        isInvincible = true;

        for (float i=0; i <invincibilityDuration; i += invincibilityDeltaTime)
        {
            if(model.transform.localScale == Vector3.one *10)
            {
                ScaleModelTo(Vector3.zero);
            }
            else
            {
                ScaleModelTo(Vector3.one *10);
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        //Debug.Log("Mortel");
        ScaleModelTo(Vector3.one *10);
        isInvincible = false;
    }
    
    private void ScaleModelTo(Vector3 scale)
    {
        model.transform.localScale = scale;
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
            Time.timeScale = 0f;
            // GO ECRAN DEF
        }
    }
    
}
