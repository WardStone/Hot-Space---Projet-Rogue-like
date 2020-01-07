using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGenerator : MonoBehaviour
{
    public GameObject KeyPrefab;
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("RightKey").Length == 0)
        {
            GameObject Key;
            Key = Instantiate(KeyPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f), Quaternion.identity);
            Key.tag = "RightKey";
        }
        else if (GameObject.FindGameObjectsWithTag("LeftKey").Length == 0 && GameObject.FindGameObjectsWithTag("RightKey").Length == 1)
        {
            GameObject Key2;
            Key2 = Instantiate(KeyPrefab,new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 0.5f), Quaternion.identity);
            Key2.tag = "LeftKey";
        }
    }
}
