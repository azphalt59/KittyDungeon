using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTheGame : MonoBehaviour
{
    
    public void PlayTheGame()
    {
        SceneManager.LoadScene("BasementMain");
    }
    public void InputMenu()
    {
        SceneManager.LoadScene("Input");
    }
    public void CreditsMenu()
    {
        SceneManager.LoadScene("Credits");
    }
}
