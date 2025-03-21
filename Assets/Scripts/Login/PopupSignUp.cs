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
        // �Է� �� ***�� ǥ��
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
            ShowError("���̵� �Է����ּ���.");
            return;
        }
        if (IsIDDuplicate(signUpID.text))
        {
            ShowError("���̵� �ߺ��˴ϴ�.");
            return;
        }

        if (signUpName.text == "")
        {
            ShowError("�̸��� �Է����ּ���.");
            return;
        }
        if (signUpPW.text == "")
        {
            ShowError("��й�ȣ�� �Է����ּ���.");
            return;
        }
        if (signUpPWCheck.text == "")
        {
            ShowError("��й�ȣ Ȯ���� �Է����ּ���.");
            return;
        }
        if (signUpPW.text != signUpPWCheck.text)
        {
            ShowError("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }

        // GameManager�� �ִ� userData�� ����
        GameManager.Instance.AddUserData(signUpID.text, signUpPW.text, signUpName.text, 0, 0);
        GameManager.Instance.SaveUserData();

        errorText.gameObject.SetActive(false);
        popupSignUp.SetActive(false);
    }

    public void OnCancel()
    {
        popupSignUp.SetActive(false);
    }

    // ���� �޽��� ǥ�� �޼��� �߰�
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
                return true;    //  �ߺ��Ǵ� ���̵� ���� ���
            }
        }
        return false;   // �ߺ��Ǵ� ���̵� ���� ���
    }
}
