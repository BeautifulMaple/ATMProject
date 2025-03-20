using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupBank : MonoBehaviour
{
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI cashMoneyText;
    public TextMeshProUGUI bankBalanceText;

    private UserData userData;

    //private List<UserData> userDataList = new List<UserData>();

    void Start()
    {
        userData = GameManager.Instance.userData;
        ReFresh();
    }

    public void ReFresh()
    {
            userNameText.text = userData.userName;
            cashMoneyText.text = string.Format("{0}", userData.cash.ToString("N0"));
            bankBalanceText.text = string.Format("{0}", userData.bankBalance.ToString("N0"));
    }
}
