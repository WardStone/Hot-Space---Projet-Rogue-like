using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArm01Behaviour : MonoBehaviour
{
    public GameObject bossLeftArm;
    public Animator leftArmAnimator01;
    private bool leftCanAttack = true;

    void Start()
    {
        
    }


    void Update()
    {
        if (leftCanAttack == true)
        {
            StartCoroutine(RandomTrigger());
        }
    }
    IEnumerator RandomTrigger()
    {
        leftArmAnimator01.SetInteger("LeftArmTrigger", Random.Range(0,2));
        Debug.Log("trigger equal to" + leftArmAnimator01.GetInteger("LeftArmTrigger"));
        leftCanAttack = false;
        yield return new WaitForSeconds(5f);
        Debug.Log("new trigger");
        leftCanAttack = true;
    }
}
