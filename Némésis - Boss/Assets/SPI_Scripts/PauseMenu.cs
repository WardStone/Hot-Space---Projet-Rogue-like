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
    private float lifeBeforeGodMod;
    private float maxLifebeforeGodMod;
    public GameObject MenuConfirmeSoundPrefab;
    private GameObject player;

    private void Start()
    {
       player = GameObject.Find("Player");
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
        Instantiate(MenuConfirmeSoundPrefab);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }

    void Pause ()
    {
        Instantiate(MenuConfirmeSoundPrefab);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; 
        GameIsPaused = true;
        
    }

    public void LoadingMenu ()
    {
        GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
        Time.timeScale = 1f;
        Destroy(thePlayer);
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void God()
    {
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        

        if (godMode == false)
        {
            lifeBeforeGodMod = playerStat.playerHealth;
            maxLifebeforeGodMod = playerStat.maxHealth;
            playerStat.maxHealth = 99999;
            playerStat.playerHealth = 99999;
            gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.SetActive(true);
            godMode = true;
        }
        else
        {
            playerStat.maxHealth = maxLifebeforeGodMod;
            playerStat.playerHealth = lifeBeforeGodMod;
            gameObject.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            godMode = false;
        }
    }

}
