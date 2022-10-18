using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject MenuUIPause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Plause();
            }
        }
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MenuUIPause.SetActive(false);
        isPaused = false;
        Time.timeScale = 1.0f;
        
        
    }
    void Plause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MenuUIPause.SetActive(true);
        isPaused = true;
        Time.timeScale = 0.0f;       

    }
    public void Menu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MENU");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
