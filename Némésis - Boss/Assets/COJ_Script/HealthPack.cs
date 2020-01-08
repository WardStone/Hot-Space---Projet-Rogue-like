using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    float healthRestored = 30f;
    PlayerStat playerStat;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerStat = other.GetComponent<PlayerStat>();
            playerStat.playerHealth += healthRestored;
            Destroy(gameObject);
        }
    }
}
