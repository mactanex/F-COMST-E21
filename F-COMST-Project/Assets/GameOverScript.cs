using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject GamerOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOver()
    {
        GamerOverScreen.SetActive(true);
    }
    public void LoadStartMenu()
    {
        Debug.Log("Loading start menu");
        Time.timeScale = 1F;
        InGameMenu.GameIsPaused = false;
        GamerOverScreen.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Restart()
    {
        Time.timeScale = 1F;
        InGameMenu.GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
