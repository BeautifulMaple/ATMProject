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
    public GameObject atm;                  // �۱�, �ӱ�, ���
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
        popupBank = FindObjectOfType<PopupBank>(); // FindObjectOfType�� ����Ͽ� PopupBank �ν��Ͻ��� ã���ϴ�.

        depositTenThousandButton.onClick.AddListener(OnTenThousandButton);
        depositThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        depositFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);

        backButton.onClick.AddListener(OnBackButton);

        depositInputFieldButton.onClick.AddListener(OnInputField);

        popupBank.ReFresh();

    }

    //private void OnEnable() // Ȱ��ȭ �� �� ����
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
        //userData = GameManager.Instance.userData;     // �ּҰ� ����

        Debug.Log($" ���� ���� - ID: {userData.id}, UserName: {userData.userName}, Cash: {userData.cash}, BankBalance: {userData.bankBalance}");

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
