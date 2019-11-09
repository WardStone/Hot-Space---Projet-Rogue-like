using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawningScript : MonoBehaviour
{
    public GameObject myPrefab;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }


    public void SpawnObject()
    {

        int rocknbr = Random.Range(2,4);
        Debug.Log(rocknbr);

        for(int i = 0; i < rocknbr; i++)
        {
            float posix = Random.Range(-8f, 8f);
            float posiy = Random.Range(-5f, 5f);

            GameObject rocks = Instantiate(myPrefab, new Vector3(posix, posiy, 0), Quaternion.identity);

            if (stopSpawning)
            {
                CancelInvoke("spawnObject");
            }
        }

    }
}
