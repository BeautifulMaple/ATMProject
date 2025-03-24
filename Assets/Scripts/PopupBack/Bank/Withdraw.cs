using UnityEngine;
using UnityEngine.UI;

public class Withdraw : MonoBehaviour
{

    public InputField withdrawInputField;

    [Header("Meney")]
    public Button withdrawTenThousandButton;
    public Button withdrawThirtyThousandButton;
    public Button withdrawFiftyThousandButton;

    public Button backButton;

    public Button withdrawInputFieldButton;

    [Header("Object")]
    public GameObject atm;                  // 송금, 임금, 출금
    public GameObject withdrawObject;
    public GameObject error;

    private UserData userData;
    private PopupBank popupBank;
    private void Awake()
    {
        withdrawInputField.contentType = InputField.ContentType.IntegerNumber;

    }
    // Start is called before the first frame update
    void Start()
    {
        userData = GameManager.Instance.userData;
        popupBank = FindObjectOfType<PopupBank>(); // FindObjectOfType를 사용하여 PopupBank 인스턴스를 찾습니다.

        withdrawTenThousandButton.onClick.AddListener(OnTenThousandButton);
        withdrawThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        withdrawFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);

        backButton.onClick.AddListener(OnBackButton);

        withdrawInputFieldButton.onClick.AddListener(OnInputField);

                    popupBank.ReFresh();

    }
    public void OnBackButton()
    {
        withdrawObject.gameObject.SetActive(false);
        atm.gameObject.SetActive(true);
    }

    public void OnTenThousandButton()
    {
        OnWithdrawMoney(MoneyUnit.tenThousand);
    }
    public void OnThirtyThousandButton()
    {
        OnWithdrawMoney(MoneyUnit.thirtyThousand);
    }
    public void OnFiftyThousandButton()
    {
        OnWithdrawMoney(MoneyUnit.fiftyThousand);
    }

    public void OnWithdrawMoney(int money)
    {
        userData = GameManager.Instance.userData;

        if (userData.bankBalance >= money)
        {
            userData.cash += money;
            userData.bankBalance -= money;
            GameManager.Instance.SaveUserData();
            popupBank.ReFresh();
        }
        else
        {
            error.gameObject.SetActive(true);
        }
    }
    public void OnInputField()
    {
        int money;
        if (int.TryParse(withdrawInputField.text, out money))
        {
            OnWithdrawMoney(money);
        }
    }
}
