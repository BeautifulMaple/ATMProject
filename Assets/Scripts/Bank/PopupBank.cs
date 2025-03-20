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
    public GameObject error;

    [Header("InputField")]
    public InputField depositInputField;
    public InputField withdrawInputField;


    [Header("Checking account")]
    public Button depositButton;
    public Button withdrawButton;
    public Button depositBackButton;
    public Button withdrawBackButton;

    [Header("Meney")]
    public Button depositTenThousandButton;
    public Button depositThirtyThousandButton;
    public Button depositFiftyThousandButton;

    public Button withdrawTenThousandButton;
    public Button withdrawThirtyThousandButton;
    public Button withdrawFiftyThousandButton;

    private UserData userData;
    private void Awake()
    {
        userData = GameManager.Instance.userData;
        depositInputField.contentType = InputField.ContentType.IntegerNumber;
        withdrawInputField.contentType = InputField.ContentType.IntegerNumber;

    }

    void Start()
    {
        depositButton.onClick.AddListener(OoDepositButton);
        withdrawButton.onClick.AddListener(OoWithdrawButton);
        depositBackButton.onClick.AddListener(OnBackButton);
        withdrawBackButton.onClick.AddListener(OnBackButton);

        depositTenThousandButton.onClick.AddListener(OnTenThousandButton);
        depositThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        depositFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);

        withdrawTenThousandButton.onClick.AddListener(OnTenThousandButton);
        withdrawThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        withdrawFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);


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

    public void OnTenThousandButton()
    {
        OnDepositMoney(MoneyUnit.tenThousand);
    }
    public void OnThirtyThousandButton()
    {
        OnDepositMoney(MoneyUnit.thirtyThousand);
    }
    public void OnFiftyThousandButton()
    {
        OnDepositMoney(MoneyUnit.fiftyThousand);
    }

    public void OnDepositMoney(int money)
    {
        if (userData.cash >= money)
        {
            userData.cash -= money;
            userData.bankBalance += money;
            ReFresh();
        }
    }
    public void OnWithdrawMoney(int money)
    {
        if (userData.bankBalance >= money)
        {
            userData.cash += money;
            userData.bankBalance -= money;
            ReFresh();
        }
    }

    public void OnInputField()
    {
        int money;
        if (int.TryParse(depositInputField.text, out money))
        {
            OnDepositMoney(money);
        }

        else if(int.TryParse(withdrawInputField.text, out money))
        {
            OnWithdrawMoney(money);
        }
        else
        {
            error.gameObject.SetActive(true);
        }
    }
    
}
