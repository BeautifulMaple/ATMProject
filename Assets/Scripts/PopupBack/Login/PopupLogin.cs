using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupLogin : MonoBehaviour
{
    [Header("InputField")]
    public TMP_InputField loginID;  // �α��� ID �Է� �ʵ�
    public TMP_InputField loginPW;  // �α��� PW �Է� �ʵ�

    [Header("Button")]
    public Button loginButton;      // �α��� ��ư
    public Button signUpButton;     // ȸ������ ��ư

    [Header("Popup")]
    public GameObject popupLogin;   // �α��� �˾�
    public GameObject popupSignUp;  // ȸ������ �˾�
    public GameObject popupBank;    // ���� �˾�

    private PopupBank popupBankScript; // PopupBank ��ũ��Ʈ ����

    private void Awake()
    {
        // FindObjectOfType : ���� ������ �ش� Ÿ���� ������Ʈ�� ã�Ƽ� ��ȯ
        popupBankScript = FindObjectOfType<PopupBank>();
    }

    void Start()
    {
        // ��й�ȣ �Է� �� ***�� ǥ��
        loginPW.contentType = TMP_InputField.ContentType.Password;

        // ��ư Ŭ�� �� ����
        loginButton.onClick.AddListener(OnLogin);   // �α��� ��ư
        signUpButton.onClick.AddListener(OnSignUp); // ȸ������ ��ư
    }

    // �α��� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnLogin()
    {
        // �Էµ� ID�� PW�� �α׷� ���
        Debug.Log($"�α��� �õ� - ID: {loginID.text}, PW: {loginPW.text}");

        // ID, PW �Է°��� ���� ������ ���� ���
        foreach (UserData user in GameManager.Instance.userDataList)
        {
            if (user.id == loginID.text && user.pw == loginPW.text)
            {
                // ��ġ�ϴ� ���� �����͸� �α׷� ���
                Debug.Log($"�α��� ���� - ID: {user.id}, PW: {user.pw}, UserName: {user.userName}, Cash: {user.cash}, BankBalance: {user.bankBalance}");

                // ���� �α����� ���� ����
                GameManager.Instance.SetCurrentUser(user.id, user.pw);
                popupLogin.SetActive(false); // �α��� �˾� ��Ȱ��ȭ
                popupBank.SetActive(true); // ���� �˾� Ȱ��ȭ
                popupBankScript.ReFresh(); // �α��� �� UI ������Ʈ
                return;
            }
        }
        Debug.Log("���̵� �Ǵ� ��й�ȣ�� Ʋ�Ƚ��ϴ�.");
    }

    // ȸ������ ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnSignUp()
    {
        popupSignUp.SetActive(true); // ȸ������ �˾� Ȱ��ȭ
    }
}
