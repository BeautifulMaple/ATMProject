using UnityEngine.UI;
using UnityEngine;

public class Deposit : MonoBehaviour
{
    public InputField depositInputField; // �Ա� �ݾ� �Է� �ʵ�

    [Header("Money")]
    public Button depositTenThousandButton;   // 1�� �� �Ա� ��ư
    public Button depositThirtyThousandButton; // 3�� �� �Ա� ��ư
    public Button depositFiftyThousandButton; // 5�� �� �Ա� ��ư

    public Button backButton;  // �ڷ� ���� ��ư
    public Button depositInputFieldButton; // �Է��� �ݾ� �Ա� ��ư

    [Header("Object")]
    public GameObject atm;              // ATM UI (�۱�, �ӱ�, ��� �޴�)
    public GameObject depositObject;    // �Ա� UI
    public GameObject error;            // �Ա� ���� �� ���� �޽���

    private UserData userData => GameManager.Instance.userData; // ���� ����� ������
    private PopupBank popupBank; // ���� UI ������ ���� PopupBank �ν��Ͻ�

    private void Awake()
    {
        // �Ա� �Է� �ʵ带 ���ڸ� �Է� �����ϵ��� ����
        depositInputField.contentType = InputField.ContentType.IntegerNumber;
    }

    private void Start()
    {
        popupBank = FindObjectOfType<PopupBank>(); // PopupBank �ν��Ͻ��� ã�� �Ҵ�

        // ��ư Ŭ�� �̺�Ʈ �߰�
        depositTenThousandButton.onClick.AddListener(OnTenThousandButton);
        depositThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        depositFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);
        depositInputFieldButton.onClick.AddListener(OnInputField);
        backButton.onClick.AddListener(OnBackButton);

        // ���� UI �ʱ� ����
        popupBank.ReFresh();
    }

    /// <summary>
    /// �ڷ� ���� ��ư Ŭ�� �� ����Ǵ� �޼��� (�Ա� UI �ݱ�)
    /// </summary>
    public void OnBackButton()
    {
        depositObject.SetActive(false); // �Ա� UI ��Ȱ��ȭ
        atm.SetActive(true); // ATM UI Ȱ��ȭ
        popupBank.ReFresh(); // UI ����
    }

    /// <summary>
    /// 1�� �� �Ա� ��ư Ŭ�� �� ����Ǵ� �޼���
    /// </summary>
    public void OnTenThousandButton()
    {
        OnDepositMoney(MoneyUnit.tenThousand);
    }

    /// <summary>
    /// 3�� �� �Ա� ��ư Ŭ�� �� ����Ǵ� �޼���
    /// </summary>
    public void OnThirtyThousandButton()
    {
        OnDepositMoney(MoneyUnit.thirtyThousand);
    }

    /// <summary>
    /// 5�� �� �Ա� ��ư Ŭ�� �� ����Ǵ� �޼���
    /// </summary>
    public void OnFiftyThousandButton()
    {
        OnDepositMoney(MoneyUnit.fiftyThousand);
    }

    /// <summary>
    /// �Ա� ��� ���� �޼���
    /// </summary>
    /// <param name="money">�Ա��� �ݾ�</param>
    public void OnDepositMoney(int money)
    {
        // ���� ���� ���� �α� ��� (������)
        Debug.Log($" ���� ���� - ID: {userData.id}, UserName: {userData.userName}, Cash: {userData.cash}, BankBalance: {userData.bankBalance}");

        // ������� ������ �Ա��� �ݾ׺��� ���ų� ���� ��� �Ա� ����
        if (userData.cash >= money)
        {
            userData.cash -= money;           // ���� ����
            userData.bankBalance += money;    // ���� �ܾ� ����
            GameManager.Instance.SaveUserData(); // ����� ������ ����
            popupBank.ReFresh();  // UI ����
        }
        else
        {
            error.SetActive(true); // �Ա� �Ұ� �޽��� Ȱ��ȭ
        }
    }

    /// <summary>
    /// �Էµ� �ݾ��� �Ա��ϴ� �޼���
    /// </summary>
    public void OnInputField()
    {
        int money;
        // �Էµ� ���� ���ڷ� ��ȯ ������ ��� �Ա� ����
        if (int.TryParse(depositInputField.text, out money))
        {
            OnDepositMoney(money);
        }
    }
}
