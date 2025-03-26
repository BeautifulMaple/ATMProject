using UnityEngine;
using UnityEngine.UI;

public class Remittance : MonoBehaviour
{
    [Header("InputField")]
    public InputField recipientInputField; // ������ �Է� �ʵ�
    public InputField moneyInputField; // �ݾ� �Է� �ʵ�

    [Header("Button")]
    public Button remittanceButton; // �۱� ��ư
    public Button backButton; // �ڷ� ���� ��ư

    [Header("GameObject")]
    public GameObject atm; // ATM ������Ʈ
    public GameObject remittance; // �۱� ������Ʈ

    public GameObject remittanceError; // �۱� ���� ������Ʈ
    public GameObject cashError; // ���� ���� ������Ʈ
    public GameObject recipientError; // ��� ���� ������Ʈ

    private UserData userData; // ���� ���� ������
    private PopupBank popupBank; // PopupBank ��ũ��Ʈ ����

    // Start is called before the first frame update
    void Start()
    {
        userData = GameManager.Instance.userData; // ���� ���� ������ ����
        popupBank = FindObjectOfType<PopupBank>(); // FindObjectOfType�� ����Ͽ� PopupBank �ν��Ͻ��� ã���ϴ�.

        moneyInputField.contentType = InputField.ContentType.IntegerNumber; // �ݾ� �Է� �ʵ带 ���������� ����

        remittanceButton.onClick.AddListener(OnRemittance); // �۱� ��ư Ŭ�� �� OnRemittance �޼��� ȣ��
        backButton.onClick.AddListener(OnBackButton); // �ڷ� ���� ��ư Ŭ�� �� OnBackButton �޼��� ȣ��

        popupBank.ReFresh(); // UI ������Ʈ
    }

    // �۱� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnRemittance()
    {
        OnErrorCheck(); // ���� üũ
        Debug.Log("�۱� ��ư �Ϸ�");
    }

    // ������ üũ�ϴ� �޼���
    private void OnErrorCheck()
    {
        // �۱� ��� / �ݾ��� �Է� ���ϸ� ����
        if (recipientInputField.text == "" || moneyInputField.text == "")
        {
            remittanceError.gameObject.SetActive(true);
            return;
        }
        // ����� ������ ����
        if (!IsRecipientValid(recipientInputField.text)) // json ���Ͽ��� ����� ã�ƾ���
        {
            recipientError.gameObject.SetActive(true);
            return;
        }
        // �ܾ��� �����ϸ� ����
        if (userData.bankBalance < int.Parse(moneyInputField.text))
        {
            cashError.gameObject.SetActive(true);
            return;
        }

        OnRemittanceCode(int.Parse(moneyInputField.text)); // �۱� ó��
        GameManager.Instance.SaveUserData(); // �۱� �� ����
        popupBank.ReFresh(); // �۱� �� UI ������Ʈ
    }

    // �����ڰ� ��ȿ���� Ȯ���ϴ� �޼���
    private bool IsRecipientValid(string recipient)
    {
        recipient = recipient.Trim(); // ���� ����
        foreach (UserData user in GameManager.Instance.GetUserDataList())
        {
            if (user.userName == recipient)
            {
                return true;
            }
        }
        return false;
    }

    // �۱��� ó���ϴ� �޼���
    private void OnRemittanceCode(int money)
    {
        userData = GameManager.Instance.userData;

        // �۱� ó�� ����
        userData.bankBalance -= money;
        foreach (UserData user in GameManager.Instance.userDataList)
        {
            if (user.userName == recipientInputField.text)
            {
                user.bankBalance += money;
                break;
            }
        }
        GameManager.Instance.SaveUserData();
    }

    // �ݾ� �Է� �ʵ��� ���� ó���ϴ� �޼���
    public void OnInputField()
    {
        int money;

        if (int.TryParse(moneyInputField.text, out money))
        {
            OnRemittanceCode(money);
        }
    }

    // �ڷ� ���� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnBackButton()
    {
        remittance.gameObject.SetActive(false); // �۱� ������Ʈ ��Ȱ��ȭ
        atm.gameObject.SetActive(true); // ATM ������Ʈ Ȱ��ȭ
    }
}
