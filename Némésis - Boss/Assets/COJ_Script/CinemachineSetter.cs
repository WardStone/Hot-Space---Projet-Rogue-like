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

    private void Update()
    {
        var camConfiner = GetComponent<CinemachineConfiner>(); ;
        if ( camConfiner.m_BoundingShape2D == null)
        {
            camConfiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("CameraConfiner").GetComponent<PolygonCollider2D>();
        }
    }

}
