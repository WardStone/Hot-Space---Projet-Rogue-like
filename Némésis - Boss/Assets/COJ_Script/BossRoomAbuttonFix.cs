using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomAbuttonFix : MonoBehaviour
{
    public switchButton Abutton;

    // Start is called before the first frame update
    void Start()
    {
        Abutton = GameObject.Find("Abutton").GetComponent<switchButton>();
        Abutton.canInteract = false;
    }
}
