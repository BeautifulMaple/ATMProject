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

        // 버튼 클릭 시 실행
        loginButton.onClick.AddListener(OnLogin);   // 로그인 버튼
        singUpButton.onClick.AddListener(OnSignUp); // 회원가입 버튼
    }

    public void OnLogin()
    {
        foreach (UserData user in GameManager.Instance.userDataList)
        {
            //ID, PW 입력값이 유저 정보와 맞을 경우
            if (user.id == loginID.text && user.pw == loginPW.text
            && loginID.text != "" && loginPW.text != "")
            {
                GameManager.Instance.userData = user; // 현재 로그인한 유저 저장
                popupLogin.SetActive(false);
                GameManager.Instance.LoadUserData();
                popupBank.SetActive(true);
            }
        }
            Debug.Log("아이디 또는 비밀번호가 틀렸습니다.");
    }
    public void OnSignUp()
    {
        popupSignUp.SetActive(true);
    }
}
