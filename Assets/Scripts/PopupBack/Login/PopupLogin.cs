using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupLogin : MonoBehaviour
{
    [Header("InputField")]
    public TMP_InputField loginID;
    public TMP_InputField loginPW;

    [Header("Button")]
    public Button loginButton;
    public Button signUpButton;

    [Header("Popup")]
    public GameObject popupLogin;
    public GameObject popupSignUp;
    public GameObject popupBank;

    private PopupBank popupBankScript;
    private void Awake()
    {
        // FindObjectOfType : ���� ������ �ش� Ÿ���� ������Ʈ�� ã�Ƽ� ��ȯ
        popupBankScript = FindObjectOfType<PopupBank>();
    }
    void Start()
    {
        loginPW.contentType = TMP_InputField.ContentType.Password;

        // ��ư Ŭ�� �� ����
        loginButton.onClick.AddListener(OnLogin);   // �α��� ��ư
        signUpButton.onClick.AddListener(OnSignUp); // ȸ������ ��ư
    }

    public void OnLogin()
    {
        // ID, PW �Է°��� ���� ������ ���� ���
        foreach (UserData user in GameManager.Instance.userDataList)
        {
            if (user.id == loginID.text && user.pw == loginPW.text)
            {
                GameManager.Instance.SetCurrentUser(user.id, user.pw); // ���� �α����� ���� ����
                popupLogin.SetActive(false);
                popupBank.SetActive(true);
                popupBankScript.ReFresh(); // �α��� �� UI ������Ʈ
                return;
            }

        }
        Debug.Log("���̵� �Ǵ� ��й�ȣ�� Ʋ�Ƚ��ϴ�.");
    }

    public void OnSignUp()
    {
        popupSignUp.SetActive(true);
    }
}
