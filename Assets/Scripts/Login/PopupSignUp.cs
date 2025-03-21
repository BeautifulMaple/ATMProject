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

    private UserData userData;
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
        if(signUpID.text == "")
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "���̵� �Է����ּ���.";
            signUpErrorText.gameObject.SetActive(true);
            return;
        }
        if (signUpName.text == "")
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "�̸��� �Է����ּ���.";
            signUpErrorText.gameObject.SetActive(true);
            return;
        }
        if (signUpPW.text == "")
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "��й�ȣ�� �Է����ּ���.";
            signUpErrorText.gameObject.SetActive(true);
            return;
        }
        if (signUpPWCheck.text == "")
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "��й�ȣ Ȯ���� �Է����ּ���.";
            signUpErrorText.gameObject.SetActive(true);
            return;
        }
        if (signUpPW.text != signUpPWCheck.text)
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            signUpErrorText.gameObject.SetActive(true);
            return;
        }

        userData = new UserData(signUpID.text, signUpPW.text, signUpName.text, 0, 0);

        // GameManager�� �ִ� userData�� ����
        GameManager.instance.userData = userData;
        GameManager.instance.SaveUserData();

        errorText.gameObject.SetActive(false);

        popupSignUp.SetActive(false);
    }

    public void OnCancel()
    {
        popupSignUp.SetActive(false);
    }
}
