using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    public GameObject InGameMenuUI;
    private void Start()
    {
        GameIsPaused = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        InGameMenuUI.SetActive(false);
        Time.timeScale = 1F;
        GameIsPaused = false;
    }

    void Pause()
    {
        InGameMenuUI.SetActive(true);
        Time.timeScale = 0F;
        GameIsPaused = true;
    }

    public void LoadStartMenu()
    {
        Debug.Log("Loading start menu");
        Time.timeScale = 1F;
        GameIsPaused = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }



}
