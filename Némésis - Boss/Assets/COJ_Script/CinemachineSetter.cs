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
        var camConfiner = GetComponent<CinemachineConfiner>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        vCam.Follow = player.transform;
        camConfiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("CameraConfiner").GetComponent<PolygonCollider2D>();

    }

}
