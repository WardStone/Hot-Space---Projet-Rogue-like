using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArm01SpawnerBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    protected GameObject bullet;
    public Transform spawnerPos;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bossRightArm01"))
        {
            bullet = Instantiate(bulletPrefab, spawnerPos.position, Quaternion.identity);
        }
     
    }
}

   

