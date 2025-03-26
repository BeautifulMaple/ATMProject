using UnityEngine.UI;
using UnityEngine;

public class Deposit : MonoBehaviour
{

    public InputField depositInputField;

    [Header("Meney")]
    public Button depositTenThousandButton;
    public Button depositThirtyThousandButton;
    public Button depositFiftyThousandButton;

    public Button backButton;

    public Button depositInputFieldButton;

    [Header("Object")]
    public GameObject atm;                  // 송금, 임금, 출금
    public GameObject depositObject;
    public GameObject error;

    private UserData userData => GameManager.Instance.userData;
    private PopupBank popupBank;
    private void Awake()
    {
        depositInputField.contentType = InputField.ContentType.IntegerNumber;
    }
    private void Start()
    {
        //userData = GameManager.Instance.userData;
        popupBank = FindObjectOfType<PopupBank>(); // FindObjectOfType를 사용하여 PopupBank 인스턴스를 찾습니다.

        depositTenThousandButton.onClick.AddListener(OnTenThousandButton);
        depositThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        depositFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);

        backButton.onClick.AddListener(OnBackButton);

        depositInputFieldButton.onClick.AddListener(OnInputField);

        popupBank.ReFresh();

    }

    //private void OnEnable() // 활성화 될 때 마다
    //{
    //    userData = GameManager.Instance.userData;
    //}
    public void OnBackButton()
    {
        depositObject.gameObject.SetActive(false);
        atm.gameObject.SetActive(true);
        popupBank.ReFresh();
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
        //userData = GameManager.Instance.userData;     // 주소값 참조

        Debug.Log($" 유저 정보 - ID: {userData.id}, UserName: {userData.userName}, Cash: {userData.cash}, BankBalance: {userData.bankBalance}");

        if (userData.cash >= money)
        {
            userData.cash -= money;
            userData.bankBalance += money;
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
        if (int.TryParse(depositInputField.text, out money))
        {
            OnDepositMoney(money);
        }
    }
}
