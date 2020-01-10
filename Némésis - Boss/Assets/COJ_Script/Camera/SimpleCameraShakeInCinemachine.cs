using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class SimpleCameraShakeInCinemachine : MonoBehaviour {

    public float ShakeDuration;          // Time the Camera Shake effect will last
    public float ShakeAmplitude;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency;         // Cinemachine Noise Profile Parameter
    public bool envrionmentShake = false;
    public bool canShake = true;

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    // Use this for initialization
    void Start()
    {
        VirtualCamera = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        Shake();
    }

    // Update is called once per frame
    void Update()
    {

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }

    public IEnumerator Shake()
    {
        envrionmentShake = true;
        ShakeDuration = 0.5f;
        ShakeAmplitude = 10f;
        ShakeElapsedTime = ShakeDuration;
        yield return new WaitForSeconds(0.5f);
        envrionmentShake = false;      
    }

    public IEnumerator bulletShake()
    {
        ShakeDuration = 0.1f;
        ShakeAmplitude = 1f;
        if(envrionmentShake == false && canShake == true)
        {
            canShake = false;
            ShakeElapsedTime = ShakeDuration;
        }
        yield return new WaitForSeconds(0.1f);
        canShake = true;
    }
}
