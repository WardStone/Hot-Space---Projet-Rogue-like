using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryControl : MonoBehaviour
{
    public void ActivateVictoryButton()
    {
        GameObject button = gameObject.transform.GetChild(0).gameObject;
        button.SetActive(true);
    }
}
