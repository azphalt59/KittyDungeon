using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{







    public void BruitDePas()
    {
        FindObjectOfType<AudioManager>().Play("Pas1");
        Debug.Log("tarace");  
    }

    
    public void Explosion()
    {
        FindObjectOfType<AudioManager>().Play("ExplosionMort");
        Debug.Log("tarace");  
    }

    
  
}
