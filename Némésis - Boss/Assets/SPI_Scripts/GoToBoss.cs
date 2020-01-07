using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToBoss : MonoBehaviour
{
    public bool canOpenBoss;
    void Start()
    {

        canOpenBoss = false;
        

    }

    void Update()
    {
        if (canOpenBoss & Input.GetButtonDown("Interact"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canOpenBoss = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canOpenBoss = false;
        }
    }
}
