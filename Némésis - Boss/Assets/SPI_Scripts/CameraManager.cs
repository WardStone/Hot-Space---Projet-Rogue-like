using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject camConfiner;

    public GameObject rootRoom;
    public GameObject tpManager;

    private void Start()
    {
        rootRoom = GameObject.FindGameObjectWithTag("RootRoom");
        tpManager = rootRoom.transform.GetChild(0).gameObject;

        camConfiner = GameObject.Find("CamConfiner");
    }
    public IEnumerator MoveCam(Vector2 positionToGo)
    {
        float timerMovement = 0.3f;

        while (timerMovement > 0)
        {
            camConfiner.transform.position = Vector2.MoveTowards(camConfiner.transform.position, positionToGo, 3f);

            timerMovement -= Time.deltaTime;

            yield return null;
        }

        yield break;
    }

    public IEnumerator CoolDownTp()
    {
        /*float cooldowntime = 1.5f;
        while (cooldowntime > 0)
        {
            tpManager.gameObject.tag = ("lock");
            cooldowntime -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("fini");
        yield break;*/
        tpManager.gameObject.tag = ("lock");
        //Debug.Log("début cool");
        yield return new WaitForSeconds(1f);
        //Debug.Log("fin cool");
        tpManager.gameObject.tag = ("Untagged");

    }
}
