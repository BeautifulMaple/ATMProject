using UnityEngine.UI;
using UnityEngine;
using UnityEditor.PackageManager;

public class Deposit : MonoBehaviour
{

    [Header("Meney")]
    public Button depositTenThousandButton;
    public Button depositThirtyThousandButton;
    public Button depositFiftyThousandButton;
    public InputField depositInputField;
    public Button depositInputFieldButton;

    private UserData userData;
    private PopupBank popupBank;

    private void Start()
    {
        userData = GameManager.Instance.userData;
        popupBank = gameObject.GetComponent<PopupBank>();
        depositTenThousandButton.onClick.AddListener(OnTenThousandButton);
        depositThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        depositFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);

        depositInputFieldButton.onClick.AddListener(OnInputField);

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
            popupBank.ReFresh();
        }
        else
        {
            popupBank.error.gameObject.SetActive(true);
        }
    }
    public void OnInputField()
    {
        int money;
        if (int.TryParse(depositInputField.text, out money))
        {
            OnDepositMoney(money);
        }
    }
}
