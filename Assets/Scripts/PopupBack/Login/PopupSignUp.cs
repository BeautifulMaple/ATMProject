using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSignUp : MonoBehaviour
{
    [Header("InputField")]
    public TMP_InputField signUpID;       // 회원가입 아이디 입력 필드
    public TMP_InputField signUpName;     // 회원가입 이름 입력 필드
    public TMP_InputField signUpPW;       // 회원가입 비밀번호 입력 필드
    public TMP_InputField signUpPWCheck;  // 회원가입 비밀번호 확인 입력 필드

    [Header("Object")]
    public GameObject popupSignUp;         // 회원가입 팝업 UI
    public TextMeshProUGUI errorText;      // 에러 메시지를 표시할 텍스트
    public GameObject signUpErrorText;     // 에러 메시지 오브젝트

    [Header("Button")]
    public Button signUpButton;    // 회원가입 버튼
    public Button cancelButton;    // 취소 버튼

    public PopupBank popupBank;    // PopupBank 인스턴스 참조

    void Start()
    {
        // PopupBank의 인스턴스를 찾아서 할당
        popupBank = FindObjectOfType<PopupBank>();

        // 비밀번호 입력 시 ***로 표시되도록 설정
        signUpPW.contentType = TMP_InputField.ContentType.Password;
        signUpPWCheck.contentType = TMP_InputField.ContentType.Password;

        // 버튼 클릭 시 이벤트 연결
        signUpButton.onClick.AddListener(OnSignUP);
        cancelButton.onClick.AddListener(OnCancel);
    }

    /// <summary>
    /// 회원가입 버튼 클릭 시 실행되는 메서드
    /// </summary>
    public void OnSignUP()
    {
        // 아이디 입력 확인
        if (signUpID.text == "")
        {
            ShowError("아이디를 입력해주세요.");
            return;
        }

        // 아이디 중복 검사
        if (IsIDDuplicate(signUpID.text))
        {
            ShowError("아이디가 중복됩니다.");
            return;
        }

        // 이름 입력 확인
        if (signUpName.text == "")
        {
            ShowError("이름을 입력해주세요.");
            return;
        }

        // 비밀번호 입력 확인
        if (signUpPW.text == "")
        {
            ShowError("비밀번호를 입력해주세요.");
            return;
        }

        // 비밀번호 확인 입력 확인
        if (signUpPWCheck.text == "")
        {
            ShowError("비밀번호 확인을 입력해주세요.");
            return;
        }

        // 비밀번호 일치 여부 확인
        if (signUpPW.text != signUpPWCheck.text)
        {
            ShowError("비밀번호가 일치하지 않습니다.");
            return;
        }

        // GameManager를 통해 회원 정보 저장
        GameManager.Instance.AddUserData(signUpID.text, signUpPW.text, signUpName.text, 0, 0);
        GameManager.Instance.SaveUserData();

        // 회원가입 후 데이터 갱신
        popupBank.ReFresh();

        // 회원가입 완료 후 에러 메시지 숨기고 팝업 닫기
        errorText.gameObject.SetActive(false);
        popupSignUp.SetActive(false);
    }

    /// <summary>
    /// 취소 버튼 클릭 시 실행되는 메서드 (회원가입 팝업 닫기)
    /// </summary>
    public void OnCancel()
    {
        popupSignUp.SetActive(false);
    }

    /// <summary>
    /// 에러 메시지를 화면에 표시하는 메서드
    /// </summary>
    /// <param name="message">표시할 에러 메시지</param>
    private void ShowError(string message)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = message;
        signUpErrorText.gameObject.SetActive(true);
    }

    /// <summary>
    /// 아이디 중복 여부를 확인하는 메서드
    /// </summary>
    /// <param name="id">확인할 아이디</param>
    /// <returns>중복 여부 (true: 중복, false: 사용 가능)</returns>
    private bool IsIDDuplicate(string id)
    {
        foreach (UserData user in GameManager.Instance.GetUserDataList())
        {
            if (user.id == id)
            {
                return true; // 중복되는 아이디가 있을 경우
            }
        }
        return false; // 중복되는 아이디가 없을 경우
    }
}
