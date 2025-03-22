using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    [Header("User Data")]
    public TextMeshProUGUI userNameText;
    public TextMeshProUGUI cashMoneyText;
    public TextMeshProUGUI bankBalanceText;

    [Header("GameObject")]
    public GameObject atm;
    public GameObject deposit;
    public GameObject withdraw;
    public GameObject remittance;

    public GameObject popupBank;
    public GameObject login;

    [Header("Checking account")]
    public Button depositButton;
    public Button withdrawButton;
    public Button remittanceButton;
    public Button loginBackButton;

    private UserData userData;


    void Start()
    {
        userData = GameManager.Instance.userData;
        depositButton.onClick.AddListener(OoDepositButton);
        withdrawButton.onClick.AddListener(OoWithdrawButton);
        remittanceButton.onClick.AddListener(OoRemittanceButton);

        loginBackButton.onClick.AddListener(OnBackButton);

        ReFresh();
    }


    public void ReFresh()
    {
        userData = GameManager.Instance.userData;
        userNameText.text = userData.userName;
        cashMoneyText.text = string.Format("{0}", userData.cash.ToString("N0"));
        bankBalanceText.text = string.Format("{0}", userData.bankBalance.ToString("N0"));
    }
    
    public void OoDepositButton()
    {
        atm.gameObject.SetActive(false);
        deposit.gameObject.SetActive(true);
    }
    public void OoWithdrawButton()
    {
        atm.gameObject.SetActive(false);
        withdraw.gameObject.SetActive(true);
    }
    public void OoRemittanceButton()
    {
        atm.gameObject.SetActive(false);
        remittance.gameObject.SetActive(true);
    }
    
    public void OnBackButton()
    {
        if (login.activeSelf == false)
        {
            //GameManager.Instance.Logout();
            login.gameObject.SetActive(true);
            popupBank.gameObject.SetActive(false);
        }
        atm.gameObject.SetActive(true);

    }

}
