using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawning : MonoBehaviour
{
    public GameObject styShadowPreviz;
    public GameObject brkShadowPreviz;

    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;
    public int styRockNbr;
    public int brkRockNbrMin;
    public int brkRockNbrMax;
    public int brkRockNbr;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }


    public void SpawnObject()
    {

        Debug.Log(styRockNbr);

        brkRockNbr = Random.Range(brkRockNbrMin, brkRockNbrMax);

        Debug.Log(brkRockNbr);


        for (int i = 0; i < styRockNbr; i++)
        {
            float posix = Random.Range(-9, 9);
            float posiy = Random.Range(-10, -4);

            GameObject rocks = Instantiate(styShadowPreviz, new Vector3(posix, posiy, 0), Quaternion.identity);

            
        }

        for (int i = 0; i < brkRockNbr; i++)
        {
            float posix = Random.Range(-9f, 9f);
            float posiy = Random.Range(-11f, -4f);

            GameObject rocks = Instantiate(brkShadowPreviz, new Vector3(posix, posiy, 0), Quaternion.identity);

        }

        if (stopSpawning)
        {
            CancelInvoke("spawnObject");
        }

    }
    public void BossSpawnObject()
    {

        for (int i = 0; i < styRockNbr; i++)
        {
            float posix = Random.Range(-9, 9);
            float posiy = Random.Range(-10, -4);

            GameObject rocks = Instantiate(styShadowPreviz, new Vector3(posix, posiy, 0), Quaternion.identity);


        }

        for (int i = 0; i < brkRockNbr; i++)
        {
            float posix = Random.Range(-9f, 9f);
            float posiy = Random.Range(-11f, -4f);

            GameObject rocks = Instantiate(brkShadowPreviz, new Vector3(posix, posiy, 0), Quaternion.identity);

        }

        if (stopSpawning)
        {
            CancelInvoke("spawnObject");
        }
    }
}
