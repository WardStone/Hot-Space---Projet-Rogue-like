using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public PlayerStat playerStat;
    public bool godMode;

    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }
    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadingMenu ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void God()
    {
        if (godMode == false)
        {
            playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
            playerStat.maxHealth = 99999;
            playerStat.playerHealth = 99999;
            gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            godMode = true;
        }
        else
        {
            playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
            playerStat.maxHealth = 200;
            playerStat.playerHealth = 200;
            gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            godMode = false;
        }
    }

}
