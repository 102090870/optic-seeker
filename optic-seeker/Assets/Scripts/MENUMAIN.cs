using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MENUMAIN : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Play");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("destroyed_city");
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
    public void MenuGame()
    {
        SceneManager.LoadScene("MENU");
    }
}
