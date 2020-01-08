using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public int playerMoney;
    public Text moneyText;
    void Start()
    {
        playerMoney = 1;
    }

    private void Update()
    {
        MoneyManagement();
    }

    void MoneyManagement()
    {
        moneyText.text = playerMoney.ToString();
    }
}
