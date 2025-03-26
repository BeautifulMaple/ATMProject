using UnityEngine;
using UnityEngine.UI;

public class Withdraw : MonoBehaviour
{
    public InputField withdrawInputField; // ��� �ݾ� �Է� �ʵ�

    [Header("Money")]
    public Button withdrawTenThousandButton;   // 1�� �� ��� ��ư
    public Button withdrawThirtyThousandButton; // 3�� �� ��� ��ư
    public Button withdrawFiftyThousandButton; // 5�� �� ��� ��ư

    public Button backButton;  // �ڷ� ���� ��ư
    public Button withdrawInputFieldButton; // �Է��� �ݾ� ��� ��ư

    [Header("Object")]
    public GameObject atm;              // ATM UI (�۱�, �ӱ�, ��� �޴�)
    public GameObject withdrawObject;    // ��� UI
    public GameObject error;             // ��� ���� �� ���� �޽���

    private UserData userData;  // ���� ����� ������
    private PopupBank popupBank; // ���� UI ������ ���� PopupBank �ν��Ͻ�

    private void Awake()
    {
        // ��� �Է� �ʵ带 ���ڸ� �Է� �����ϵ��� ����
        withdrawInputField.contentType = InputField.ContentType.IntegerNumber;
    }

    void Start()
    {
        // ���� ����� ���� ��������
        userData = GameManager.Instance.userData;
        popupBank = FindObjectOfType<PopupBank>(); // PopupBank �ν��Ͻ��� ã�� �Ҵ�

        // ��ư Ŭ�� �̺�Ʈ �߰�
        withdrawTenThousandButton.onClick.AddListener(OnTenThousandButton);
        withdrawThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        withdrawFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);
        withdrawInputFieldButton.onClick.AddListener(OnInputField);
        backButton.onClick.AddListener(OnBackButton);

        // ���� UI �ʱ� ����
        popupBank.ReFresh();
    }

    /// <summary>
    /// �ڷ� ���� ��ư Ŭ�� �� ����Ǵ� �޼��� (��� UI �ݱ�)
    /// </summary>
    public void OnBackButton()
    {
        withdrawObject.SetActive(false); // ��� UI ��Ȱ��ȭ
        atm.SetActive(true); // ATM UI Ȱ��ȭ
    }

    /// <summary>
    /// 1�� �� ��� ��ư Ŭ�� �� ����Ǵ� �޼���
    /// </summary>
    public void OnTenThousandButton()
    {
        OnWithdrawMoney(MoneyUnit.tenThousand);
    }

    /// <summary>
    /// 3�� �� ��� ��ư Ŭ�� �� ����Ǵ� �޼���
    /// </summary>
    public void OnThirtyThousandButton()
    {
        OnWithdrawMoney(MoneyUnit.thirtyThousand);
    }

    /// <summary>
    /// 5�� �� ��� ��ư Ŭ�� �� ����Ǵ� �޼���
    /// </summary>
    public void OnFiftyThousandButton()
    {
        OnWithdrawMoney(MoneyUnit.fiftyThousand);
    }

    /// <summary>
    /// ��� UI�� Ȱ��ȭ�� �� ����Ǵ� �޼��� (����� ������ ����)
    /// </summary>
    private void OnEnable()
    {
        userData = GameManager.Instance.userData;
    }

    /// <summary>
    /// ��� ��� ���� �޼���
    /// </summary>
    /// <param name="money">����� �ݾ�</param>
    public void OnWithdrawMoney(int money)
    {
        // ���� �ܾ��� ����� �ݾ׺��� ���ų� ���� ��� ��� ����
        if (userData.bankBalance >= money)
        {
            userData.cash += money;           // ���� ����
            userData.bankBalance -= money;    // ���� �ܾ� ����
            GameManager.Instance.SaveUserData(); // ����� ������ ����
            popupBank.ReFresh();  // UI ����
        }
        else
        {
            error.SetActive(true); // ��� �Ұ� �޽��� Ȱ��ȭ
        }
    }

    /// <summary>
    /// �Էµ� �ݾ��� ����ϴ� �޼���
    /// </summary>
    public void OnInputField()
    {
        int money;
        // �Էµ� ���� ���ڷ� ��ȯ ������ ��� ��� ����
        if (int.TryParse(withdrawInputField.text, out money))
        {
            OnWithdrawMoney(money);
        }
    }
}
