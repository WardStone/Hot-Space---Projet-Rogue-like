using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterArmsBehaviour : MonoBehaviour
{
    private Animator armsAnimator;
    void Start()
    {
        armsAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        armsAnimator.SetFloat("HorizontalJoystick", Input.GetAxis("HorizontalSecondJoystick"));
        armsAnimator.SetFloat("VerticalJoystick", Input.GetAxis("VerticalSecondJoystick"));
    }
}
