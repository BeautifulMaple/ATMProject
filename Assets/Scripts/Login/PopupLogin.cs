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

        // 버튼 클릭 시 실행
        loginButton.onClick.AddListener(OnLogin);   // 로그인 버튼
        singUpButton.onClick.AddListener(OnSignUp); // 회원가입 버튼
    }

    public void OnLogin()
    {

        //ID, PW 입력값이 유저 정보와 맞을 경우
        if (userData.id == loginID.text && userData.pw == loginPW.text)
        {
            popupLogin.SetActive(false);
            GameManager.instance.LoadUserData();
            popupBank.SetActive(true);
        }
        else
        {
            Debug.Log("아이디 또는 비밀번호가 틀렸습니다.");
        }
    }
    public void OnSignUp()
    {
        popupSignUp.SetActive(true);
    }
}
