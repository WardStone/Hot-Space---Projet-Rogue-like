using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void RestartGame()
    {
        Destroy(player);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
