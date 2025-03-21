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
        // FindObjectOfType : 현재 씬에서 해당 타입의 오브젝트를 찾아서 반환
        popupBankScript = FindObjectOfType<PopupBank>();
    }
    void Start()
    {
        loginPW.contentType = TMP_InputField.ContentType.Password;

        // 버튼 클릭 시 실행
        loginButton.onClick.AddListener(OnLogin);   // 로그인 버튼
        signUpButton.onClick.AddListener(OnSignUp); // 회원가입 버튼
    }

    public void OnLogin()
    {
        // ID, PW 입력값이 유저 정보와 맞을 경우
        foreach (UserData user in GameManager.Instance.userDataList)
        {
            if (user.id == loginID.text && user.pw == loginPW.text)
            {
                GameManager.Instance.SetCurrentUser(user.id, user.pw); // 현재 로그인한 유저 설정
                popupLogin.SetActive(false);
                popupBank.SetActive(true);
                popupBankScript.ReFresh(); // 로그인 후 UI 업데이트
                return;
            }

        }
        Debug.Log("아이디 또는 비밀번호가 틀렸습니다.");
    }

    public void OnSignUp()
    {
        popupSignUp.SetActive(true);
    }
}
