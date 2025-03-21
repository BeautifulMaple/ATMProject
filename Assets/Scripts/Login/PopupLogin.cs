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
    public Button singUpButton;

    [Header("Button")]
    public GameObject popupLogin;
    public GameObject popupSignUp;
    public GameObject popupBank;

    private UserData userData;
    // Start is called before the first frame update
    void Start()
    {
        userData = GameManager.instance.userData;

        loginPW.contentType = TMP_InputField.ContentType.Password;

        // ��ư Ŭ�� �� ����
        loginButton.onClick.AddListener(OnLogin);   // �α��� ��ư
        singUpButton.onClick.AddListener(OnSignUp); // ȸ������ ��ư
    }

    public void OnLogin()
    {

        //ID, PW �Է°��� ���� ������ ���� ���
        if (userData.id == loginID.text && userData.pw == loginPW.text)
        {
            popupLogin.SetActive(false);
            GameManager.instance.LoadUserData();
            popupBank.SetActive(true);
        }
        else
        {
            Debug.Log("���̵� �Ǵ� ��й�ȣ�� Ʋ�Ƚ��ϴ�.");
        }
    }
    public void OnSignUp()
    {
        popupSignUp.SetActive(true);
    }
}
