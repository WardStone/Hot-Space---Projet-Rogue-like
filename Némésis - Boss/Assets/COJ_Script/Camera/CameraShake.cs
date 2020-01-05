using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]float strenght;
    [SerializeField]float duration;

    private Vector3 initialCameraPos;
    private float remainTime;

    public void Shake()
    {
        remainTime = duration;
        enabled = true;
    }

    private void Awake()
    {
        initialCameraPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        initialCameraPos = transform.localPosition;

        if(remainTime <= 0)
        {
            transform.localPosition = initialCameraPos;
            enabled = false;
        }

        transform.Translate(Random.insideUnitCircle * strenght);
        remainTime -= Time.deltaTime;
    }
}
