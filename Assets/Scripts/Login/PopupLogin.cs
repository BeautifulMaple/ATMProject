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

    void Start()
    {

        loginPW.contentType = TMP_InputField.ContentType.Password;

        // ��ư Ŭ�� �� ����
        loginButton.onClick.AddListener(OnLogin);   // �α��� ��ư
        singUpButton.onClick.AddListener(OnSignUp); // ȸ������ ��ư
    }

    public void OnLogin()
    {
        foreach (UserData user in GameManager.Instance.userDataList)
        {
            //ID, PW �Է°��� ���� ������ ���� ���
            if (user.id == loginID.text && user.pw == loginPW.text
            && loginID.text != "" && loginPW.text != "")
            {
                GameManager.Instance.userData = user; // ���� �α����� ���� ����
                popupLogin.SetActive(false);
                GameManager.Instance.LoadUserData();
                popupBank.SetActive(true);
            }
        }
            Debug.Log("���̵� �Ǵ� ��й�ȣ�� Ʋ�Ƚ��ϴ�.");
    }
    public void OnSignUp()
    {
        popupSignUp.SetActive(true);
    }
}
