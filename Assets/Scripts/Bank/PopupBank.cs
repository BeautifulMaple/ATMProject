using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    [Header("User Data")]
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI cashMoneyText;
    public TextMeshProUGUI bankBalanceText;

    [Header("Checking account")]
    public GameObject atm;
    public GameObject deposit;
    public GameObject withdraw;
    public Button depositButton;
    public Button withdrawButton;
    public Button depositBackButton;
    public Button withdrawBackButton;

    private UserData userData;

    void Start()
    {
        userData = GameManager.Instance.userData;

        depositButton.onClick.AddListener(OoDepositButton);
        withdrawButton.onClick.AddListener(OoWithdrawButton);
        depositBackButton.onClick.AddListener(OnBackButton);
        withdrawBackButton.onClick.AddListener(OnBackButton);
        ReFresh();
    }

    public void ReFresh()
    {
            userNameText.text = userData.userName;
            cashMoneyText.text = string.Format("{0}", userData.cash.ToString("N0"));
            bankBalanceText.text = string.Format("{0}", userData.bankBalance.ToString("N0"));
    }
    
    public void OoDepositButton()
    {
        atm.gameObject.SetActive(false);
        OnDeposit();
    }
    public void OoWithdrawButton()
    {
        atm.gameObject.SetActive(false);
        OnWithdraw();
    }

    private void OnDeposit()
    {
        deposit.gameObject.SetActive(true);
        withdraw.gameObject.SetActive(false);
    }
    private void OnWithdraw()
    {
        deposit.gameObject.SetActive(false);
        withdraw.gameObject.SetActive(true);
    }

    public void OnBackButton()
    {
        atm.gameObject.SetActive(true);

        if (deposit.activeSelf)
        {
            deposit.gameObject.SetActive(false);
        }
        else if (withdraw.activeSelf)
        {
            withdraw.gameObject.SetActive(false);
        }

    }
}
