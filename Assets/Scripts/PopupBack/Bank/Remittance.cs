using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Remittance : MonoBehaviour
{
    [Header("InputField")]
    public InputField recipientInputField;
    public InputField moneyInputField;

    [Header("Button")]
    public Button remittanceButton;
    public Button backButton;

    [Header("GameObject")]
    public GameObject atm;                  // 송금, 임금, 출금
    public GameObject remittance;           // 송금 오브젝트

    public GameObject remittanceError;      // 송금 에러
    public GameObject cashError;            // 현금 에러
    public GameObject recipientError;       // 대상 에러

    private UserData userData;
    private PopupBank popupBank;
    // Start is called before the first frame update
    void Start()
    {
        userData = GameManager.Instance.userData;
        popupBank = FindObjectOfType<PopupBank>();  // FindObjectOfType : 현재 씬에서 해당 타입의 오브젝트를 찾아서 반환

        moneyInputField.contentType = InputField.ContentType.IntegerNumber;

        remittanceButton.onClick.AddListener(OnRemittance);
        backButton.onClick.AddListener(OnBackButton);

        popupBank.ReFresh();
    }

    public void OnRemittance()
    {
        OnErrorCheck();
        Debug.Log("송금 버튼 완료");
    }
    private void OnErrorCheck()
    {
        // 송금 대상 / 금액을 입력 안하면 에러
        if (recipientInputField.text == "" || moneyInputField.text == "")
        {
            remittanceError.gameObject.SetActive(true);
            return;
        }
        // 대상이 없으면 에러
        if (!IsRecipientValid(recipientInputField.text)) // json 파일에서 대상을 찾아야함
        {
            recipientError.gameObject.SetActive(true);
            return;
        }
        // 잔액이 부족하면 에러
        if (userData.bankBalance < int.Parse(moneyInputField.text))
        {
            cashError.gameObject.SetActive(true);
            return;
        }
        
        OnRemittanceCode(int.Parse(moneyInputField.text));
        GameManager.Instance.SaveUserData();    // 송금 후 저장
        popupBank.ReFresh(); // 송금 후 UI 업데이트

    }

    private bool IsRecipientValid(string recipient)
    {
        recipient = recipient.Trim(); // 공백 제거
        foreach (UserData user in GameManager.Instance.GetUserDataList())
        {
            if (user.userName == recipient)
            {
                return true;
            }
        }
        return false;
    }

    private void OnRemittanceCode(int money)
    {
        userData = GameManager.Instance.userData;

        // 송금 처리 로직
        userData.bankBalance -= money;
        foreach (UserData user in GameManager.Instance.userDataList)
        {
            if (user.userName == recipientInputField.text)
            {
                user.bankBalance += money;
                break;
            }
        }
        GameManager.Instance.SaveUserData();
    }

    public void OnInputField()
    {
        int money;

        if (int.TryParse(moneyInputField.text, out money))
        {
            OnRemittanceCode(money);
        }
    }
    public void OnBackButton()
    {
        remittance.gameObject.SetActive(false);
        atm.gameObject.SetActive(true);
    }
}
