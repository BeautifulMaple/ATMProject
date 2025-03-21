using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupSignUp : MonoBehaviour
{
    [Header("InputField")]
    public TMP_InputField signUpID;
    public TMP_InputField signUpName;
    public TMP_InputField signUpPW;
    public TMP_InputField signUpPWCheck;

    [Header("Object")]
    public GameObject popupSignUp;
    public TextMeshProUGUI errorText;
    public GameObject signUpErrorText;

    [Header("Button")]
    public Button signUpButton;
    public Button cancelButton;

    // Start is called before the first frame update
    void Start()
    {
        // 입력 시 ***로 표시
        signUpPW.contentType = TMP_InputField.ContentType.Password;
        signUpPWCheck.contentType = TMP_InputField.ContentType.Password;

        signUpButton.onClick.AddListener(OnSignUP);
        cancelButton.onClick.AddListener(OnCancel);
    }

    // Update is called once per frame
    public void OnSignUP()
    {

        if (signUpID.text == "")
        {
            ShowError("아이디를 입력해주세요.");
            return;
        }
        if (IsIDDuplicate(signUpID.text))
        {
            ShowError("아이디가 중복됩니다.");
            return;
        }

        if (signUpName.text == "")
        {
            ShowError("이름을 입력해주세요.");
            return;
        }
        if (signUpPW.text == "")
        {
            ShowError("비밀번호를 입력해주세요.");
            return;
        }
        if (signUpPWCheck.text == "")
        {
            ShowError("비밀번호 확인을 입력해주세요.");
            return;
        }
        if (signUpPW.text != signUpPWCheck.text)
        {
            ShowError("비밀번호가 일치하지 않습니다.");
            return;
        }

        // GameManager에 있는 userData에 저장
        GameManager.Instance.AddUserData(signUpID.text, signUpPW.text, signUpName.text, 0, 0);
        GameManager.Instance.SaveUserData();

        errorText.gameObject.SetActive(false);
        popupSignUp.SetActive(false);
    }

    public void OnCancel()
    {
        popupSignUp.SetActive(false);
    }

    // 에러 메시지 표시 메서드 추가
    private void ShowError(string message)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = message;
        signUpErrorText.gameObject.SetActive(true);
    }

    private bool IsIDDuplicate(string id)
    {
        foreach (UserData user in GameManager.Instance.GetUserDataList())
        {
            if (user.id == id)
            {
                return true;    //  중복되는 아이디가 있을 경우
            }
        }
        return false;   // 중복되는 아이디가 없을 경우
    }
}
