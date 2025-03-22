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
    public GameObject atm;                  // �۱�, �ӱ�, ���
    public GameObject remittance;           // �۱� ������Ʈ

    public GameObject remittanceError;      // �۱� ����
    public GameObject cashError;            // ���� ����
    public GameObject recipientError;       // ��� ����

    private UserData userData;
    private PopupBank popupBank;
    // Start is called before the first frame update
    void Start()
    {
        userData = GameManager.Instance.userData;
        popupBank = FindObjectOfType<PopupBank>();  // FindObjectOfType : ���� ������ �ش� Ÿ���� ������Ʈ�� ã�Ƽ� ��ȯ

        moneyInputField.contentType = InputField.ContentType.IntegerNumber;

        remittanceButton.onClick.AddListener(OnRemittance);
        backButton.onClick.AddListener(OnBackButton);

        popupBank.ReFresh();
    }

    public void OnRemittance()
    {
        OnErrorCheck();
        Debug.Log("�۱� ��ư �Ϸ�");
    }
    private void OnErrorCheck()
    {
        // �۱� ��� / �ݾ��� �Է� ���ϸ� ����
        if (recipientInputField.text == "" || moneyInputField.text == "")
        {
            remittanceError.gameObject.SetActive(true);
            return;
        }
        // ����� ������ ����
        if (!IsRecipientValid(recipientInputField.text)) // json ���Ͽ��� ����� ã�ƾ���
        {
            recipientError.gameObject.SetActive(true);
            return;
        }
        // �ܾ��� �����ϸ� ����
        if (userData.bankBalance < int.Parse(moneyInputField.text))
        {
            cashError.gameObject.SetActive(true);
            return;
        }
        
        OnRemittanceCode(int.Parse(moneyInputField.text));
        GameManager.Instance.SaveUserData();    // �۱� �� ����
        popupBank.ReFresh(); // �۱� �� UI ������Ʈ

    }

    private bool IsRecipientValid(string recipient)
    {
        recipient = recipient.Trim(); // ���� ����
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

        // �۱� ó�� ����
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
