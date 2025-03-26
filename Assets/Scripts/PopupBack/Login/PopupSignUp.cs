using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSignUp : MonoBehaviour
{
    [Header("InputField")]
    public TMP_InputField signUpID;       // ȸ������ ���̵� �Է� �ʵ�
    public TMP_InputField signUpName;     // ȸ������ �̸� �Է� �ʵ�
    public TMP_InputField signUpPW;       // ȸ������ ��й�ȣ �Է� �ʵ�
    public TMP_InputField signUpPWCheck;  // ȸ������ ��й�ȣ Ȯ�� �Է� �ʵ�

    [Header("Object")]
    public GameObject popupSignUp;         // ȸ������ �˾� UI
    public TextMeshProUGUI errorText;      // ���� �޽����� ǥ���� �ؽ�Ʈ
    public GameObject signUpErrorText;     // ���� �޽��� ������Ʈ

    [Header("Button")]
    public Button signUpButton;    // ȸ������ ��ư
    public Button cancelButton;    // ��� ��ư

    public PopupBank popupBank;    // PopupBank �ν��Ͻ� ����

    void Start()
    {
        // PopupBank�� �ν��Ͻ��� ã�Ƽ� �Ҵ�
        popupBank = FindObjectOfType<PopupBank>();

        // ��й�ȣ �Է� �� ***�� ǥ�õǵ��� ����
        signUpPW.contentType = TMP_InputField.ContentType.Password;
        signUpPWCheck.contentType = TMP_InputField.ContentType.Password;

        // ��ư Ŭ�� �� �̺�Ʈ ����
        signUpButton.onClick.AddListener(OnSignUP);
        cancelButton.onClick.AddListener(OnCancel);
    }

    /// <summary>
    /// ȸ������ ��ư Ŭ�� �� ����Ǵ� �޼���
    /// </summary>
    public void OnSignUP()
    {
        // ���̵� �Է� Ȯ��
        if (signUpID.text == "")
        {
            ShowError("���̵� �Է����ּ���.");
            return;
        }

        // ���̵� �ߺ� �˻�
        if (IsIDDuplicate(signUpID.text))
        {
            ShowError("���̵� �ߺ��˴ϴ�.");
            return;
        }

        // �̸� �Է� Ȯ��
        if (signUpName.text == "")
        {
            ShowError("�̸��� �Է����ּ���.");
            return;
        }

        // ��й�ȣ �Է� Ȯ��
        if (signUpPW.text == "")
        {
            ShowError("��й�ȣ�� �Է����ּ���.");
            return;
        }

        // ��й�ȣ Ȯ�� �Է� Ȯ��
        if (signUpPWCheck.text == "")
        {
            ShowError("��й�ȣ Ȯ���� �Է����ּ���.");
            return;
        }

        // ��й�ȣ ��ġ ���� Ȯ��
        if (signUpPW.text != signUpPWCheck.text)
        {
            ShowError("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }

        // GameManager�� ���� ȸ�� ���� ����
        GameManager.Instance.AddUserData(signUpID.text, signUpPW.text, signUpName.text, 0, 0);
        GameManager.Instance.SaveUserData();

        // ȸ������ �� ������ ����
        popupBank.ReFresh();

        // ȸ������ �Ϸ� �� ���� �޽��� ����� �˾� �ݱ�
        errorText.gameObject.SetActive(false);
        popupSignUp.SetActive(false);
    }

    /// <summary>
    /// ��� ��ư Ŭ�� �� ����Ǵ� �޼��� (ȸ������ �˾� �ݱ�)
    /// </summary>
    public void OnCancel()
    {
        popupSignUp.SetActive(false);
    }

    /// <summary>
    /// ���� �޽����� ȭ�鿡 ǥ���ϴ� �޼���
    /// </summary>
    /// <param name="message">ǥ���� ���� �޽���</param>
    private void ShowError(string message)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = message;
        signUpErrorText.gameObject.SetActive(true);
    }

    /// <summary>
    /// ���̵� �ߺ� ���θ� Ȯ���ϴ� �޼���
    /// </summary>
    /// <param name="id">Ȯ���� ���̵�</param>
    /// <returns>�ߺ� ���� (true: �ߺ�, false: ��� ����)</returns>
    private bool IsIDDuplicate(string id)
    {
        foreach (UserData user in GameManager.Instance.GetUserDataList())
        {
            if (user.id == id)
            {
                return true; // �ߺ��Ǵ� ���̵� ���� ���
            }
        }
        return false; // �ߺ��Ǵ� ���̵� ���� ���
    }
}
