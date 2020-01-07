using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnerBehaviour : MonoBehaviour
{

    public GameObject boss;
    SimpleCameraShakeInCinemachine camShake;
    // Start is called before the first frame update
    void Start()
    {
        camShake = GameObject.FindGameObjectWithTag("CameraShakeManager").GetComponent<SimpleCameraShakeInCinemachine>();
        StartCoroutine(SpawnBoss());
    }

   IEnumerator SpawnBoss()
    {
        camShake.Shake();
        yield return new WaitForSeconds(3.5f);
        camShake.Shake();
        yield return new WaitForSeconds(1f);
        camShake.Shake();
        yield return new WaitForSeconds(0.5f);
        camShake.Shake();
        Instantiate(boss, gameObject.transform.position, Quaternion.identity);
    }
}
