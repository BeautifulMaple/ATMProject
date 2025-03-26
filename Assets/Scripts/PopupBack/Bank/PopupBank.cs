using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    [Header("User Data")]
    public TextMeshProUGUI userNameText;    // ����� �̸� UI �ؽ�Ʈ
    public TextMeshProUGUI cashMoneyText;   // ���� ������ UI �ؽ�Ʈ
    public TextMeshProUGUI bankBalanceText; // ���� �ܰ� UI �ؽ�Ʈ

    [Header("GameObject")]
    public GameObject atm;         // ATM �޴� UI
    public GameObject deposit;     // �Ա� UI
    public GameObject withdraw;    // ��� UI
    public GameObject remittance;  // �۱� UI
    public GameObject popupBank;   // ���� �˾� UI
    public GameObject login;       // �α��� UI

    [Header("Checking account")]
    public Button depositButton;    // �Ա� ��ư
    public Button withdrawButton;   // ��� ��ư
    public Button remittanceButton; // �۱� ��ư
    public Button loginBackButton;  // �ڷ� ���� ��ư

    private UserData userData; // ���� �α����� ����� ����

    void Start()
    {
        // GameManager���� ���� ����� ������ ��������
        userData = GameManager.Instance.userData;

        // ��ư Ŭ�� �� ������ �޼��� ����
        depositButton.onClick.AddListener(OoDepositButton);
        withdrawButton.onClick.AddListener(OoWithdrawButton);
        remittanceButton.onClick.AddListener(OoRemittanceButton);
        loginBackButton.onClick.AddListener(OnBackButton);

        // UI ���� ����
        ReFresh();
    }

    /// <summary>
    /// ����� ������ UI�� �ݿ��Ͽ� �����ϴ� �޼���
    /// </summary>
    public void ReFresh()
    {
        // GameManager���� ���� ����� ������ ����
        userData = GameManager.Instance.userData;

        // UI �ؽ�Ʈ ����
        userNameText.text = userData.userName;
        cashMoneyText.text = string.Format("{0}", userData.cash.ToString("N0")); // ������ õ ������ ǥ��
        bankBalanceText.text = string.Format("{0}", userData.bankBalance.ToString("N0")); // ���� �ܰ� õ ���� ǥ��
    }

    /// <summary>
    /// �Ա� ��ư Ŭ�� �� �Ա� UI Ȱ��ȭ
    /// </summary>
    public void OoDepositButton()
    {
        atm.gameObject.SetActive(false);     // ATM UI ��Ȱ��ȭ
        deposit.gameObject.SetActive(true);  // �Ա� UI Ȱ��ȭ
    }

    /// <summary>
    /// ��� ��ư Ŭ�� �� ��� UI Ȱ��ȭ
    /// </summary>
    public void OoWithdrawButton()
    {
        atm.gameObject.SetActive(false);     // ATM UI ��Ȱ��ȭ
        withdraw.gameObject.SetActive(true); // ��� UI Ȱ��ȭ
    }

    /// <summary>
    /// �۱� ��ư Ŭ�� �� �۱� UI Ȱ��ȭ
    /// </summary>
    public void OoRemittanceButton()
    {
        atm.gameObject.SetActive(false);      // ATM UI ��Ȱ��ȭ
        remittance.gameObject.SetActive(true); // �۱� UI Ȱ��ȭ
    }

    /// <summary>
    /// �ڷ� ���� ��ư Ŭ�� �� �α��� UI �� ATM UI Ȱ��ȭ
    /// </summary>
    public void OnBackButton()
    {
        // �α��� UI�� ��Ȱ��ȭ ������ ��� Ȱ��ȭ
        if (!login.activeSelf)
        {
            login.gameObject.SetActive(true);       // �α��� UI Ȱ��ȭ
            popupBank.gameObject.SetActive(false);  // ���� �˾� UI ��Ȱ��ȭ
        }

        atm.gameObject.SetActive(true); // ATM UI Ȱ��ȭ
    }
}
