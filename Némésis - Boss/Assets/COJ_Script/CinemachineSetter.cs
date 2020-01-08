using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var vCam = GetComponent<CinemachineVirtualCamera>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        vCam.Follow = player.transform;
    }

}
