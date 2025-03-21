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
    public GameObject error;

    [Header("InputField")]
    public InputField depositInputField;
    public InputField withdrawInputField;

    [Header("Checking account")]
    public Button depositButton;
    public Button withdrawButton;
    public Button remittanceButton;

    [Header("BackButton")]
    public Button depositBackButton;
    public Button withdrawBackButton;

    public Button depositInputFieldButton;
    public Button withdrawInputFieldButton;

    public Button loginBackButton;

    [Header("Meney")]
    public Button depositTenThousandButton;
    public Button depositThirtyThousandButton;
    public Button depositFiftyThousandButton;

    public Button withdrawTenThousandButton;
    public Button withdrawThirtyThousandButton;
    public Button withdrawFiftyThousandButton;

    private UserData userData;
    private bool isDeposit;
    private void Awake()
    {
        depositInputField.contentType = InputField.ContentType.IntegerNumber;
        withdrawInputField.contentType = InputField.ContentType.IntegerNumber;
    }

    void Start()
    {
        //userData = GameManager.Instance.userData;

        depositButton.onClick.AddListener(OoDepositButton);
        withdrawButton.onClick.AddListener(OoWithdrawButton);
        remittanceButton.onClick.AddListener(OoRemittanceButton);

        depositBackButton.onClick.AddListener(OnBackButton);
        withdrawBackButton.onClick.AddListener(OnBackButton);

        depositTenThousandButton.onClick.AddListener(OnTenThousandButton);
        depositThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        depositFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);

        withdrawTenThousandButton.onClick.AddListener(OnTenThousandButton);
        withdrawThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        withdrawFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);

        depositInputFieldButton.onClick.AddListener(OnInputField);
        withdrawInputFieldButton.onClick.AddListener(OnInputField);

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
        OnDeposit();
    }
    public void OoWithdrawButton()
    {
        atm.gameObject.SetActive(false);
        OnWithdraw();
    }
    public void OoRemittanceButton()
    {
        atm.gameObject.SetActive(false);
        remittance.gameObject.SetActive(true);
    }

    private void OnDeposit()
    {
        isDeposit = true;
        deposit.gameObject.SetActive(true);
        withdraw.gameObject.SetActive(false);
    }
    private void OnWithdraw()
    {
        isDeposit = false;
        deposit.gameObject.SetActive(false);
        withdraw.gameObject.SetActive(true);
    }
    public void OnBackButton()
    {
        if (deposit.activeSelf)
        {
            deposit.gameObject.SetActive(false);
        }
        else if (withdraw.activeSelf)
        {
            withdraw.gameObject.SetActive(false);
        }
        else if (login.activeSelf == false)
        {
            GameManager.Instance.Logout();
            login.gameObject.SetActive(true);
            popupBank.gameObject.SetActive(false);
        }
        atm.gameObject.SetActive(true);

    }

    public void OnTenThousandButton()
    {
        if(isDeposit) OnDepositMoney(MoneyUnit.tenThousand);
        else OnWithdrawMoney(MoneyUnit.tenThousand);
    }
    public void OnThirtyThousandButton()
    {
        if (isDeposit) OnDepositMoney(MoneyUnit.thirtyThousand);
        else OnWithdrawMoney(MoneyUnit.thirtyThousand);
    }
    public void OnFiftyThousandButton()
    {
        if (isDeposit) OnDepositMoney(MoneyUnit.fiftyThousand);
        else OnWithdrawMoney(MoneyUnit.fiftyThousand);
    }

    public void OnDepositMoney(int money)
    {
        if (userData.cash >= money)
        {
            userData.cash -= money;
            userData.bankBalance += money;
            GameManager.Instance.SaveUserData();
            ReFresh();
        }
        else
        {
            error.gameObject.SetActive(true);
        }
    }
    public void OnWithdrawMoney(int money)
    {
        if (userData.bankBalance >= money)
        {
            userData.cash += money;
            userData.bankBalance -= money;
            GameManager.Instance.SaveUserData();
            ReFresh();
        }
        else
        {
            error.gameObject.SetActive(true);
        }
    }

    public void OnInputField()
    {
        int money;

        if (isDeposit)
        {
            if (int.TryParse(depositInputField.text, out money))
            {
                OnDepositMoney(money);
            }
        }
        else
        {
            if (int.TryParse(withdrawInputField.text, out money))
            {
                OnWithdrawMoney(money);
            }
        }

        error.gameObject.SetActive(true);
    }

}
