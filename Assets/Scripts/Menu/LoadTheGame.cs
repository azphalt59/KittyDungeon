using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTheGame : MonoBehaviour
{
    public string BasementMain;
    public void PlayTheGame()
    {
        Debug.Log("sfdfd");
        SceneManager.LoadScene(BasementMain);
    }
    public void InputMenu()
    {
        SceneManager.LoadScene("Input");
    }
    public void CreditsMenu()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Quiiiit()
    {
        Debug.Log("sds");
        Application.Quit();
    }
}
