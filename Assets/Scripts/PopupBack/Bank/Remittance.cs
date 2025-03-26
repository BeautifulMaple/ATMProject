using UnityEngine;
using UnityEngine.UI;

public class Remittance : MonoBehaviour
{
    [Header("InputField")]
    public InputField recipientInputField; // 수신자 입력 필드
    public InputField moneyInputField; // 금액 입력 필드

    [Header("Button")]
    public Button remittanceButton; // 송금 버튼
    public Button backButton; // 뒤로 가기 버튼

    [Header("GameObject")]
    public GameObject atm; // ATM 오브젝트
    public GameObject remittance; // 송금 오브젝트

    public GameObject remittanceError; // 송금 에러 오브젝트
    public GameObject cashError; // 현금 에러 오브젝트
    public GameObject recipientError; // 대상 에러 오브젝트

    private UserData userData; // 현재 유저 데이터
    private PopupBank popupBank; // PopupBank 스크립트 참조

    // Start is called before the first frame update
    void Start()
    {
        userData = GameManager.Instance.userData; // 현재 유저 데이터 설정
        popupBank = FindObjectOfType<PopupBank>(); // FindObjectOfType를 사용하여 PopupBank 인스턴스를 찾습니다.

        moneyInputField.contentType = InputField.ContentType.IntegerNumber; // 금액 입력 필드를 정수형으로 설정

        remittanceButton.onClick.AddListener(OnRemittance); // 송금 버튼 클릭 시 OnRemittance 메서드 호출
        backButton.onClick.AddListener(OnBackButton); // 뒤로 가기 버튼 클릭 시 OnBackButton 메서드 호출

        popupBank.ReFresh(); // UI 업데이트
    }

    // 송금 버튼 클릭 시 호출되는 메서드
    public void OnRemittance()
    {
        OnErrorCheck(); // 에러 체크
        Debug.Log("송금 버튼 완료");
    }

    // 에러를 체크하는 메서드
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

        OnRemittanceCode(int.Parse(moneyInputField.text)); // 송금 처리
        GameManager.Instance.SaveUserData(); // 송금 후 저장
        popupBank.ReFresh(); // 송금 후 UI 업데이트
    }

    // 수신자가 유효한지 확인하는 메서드
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

    // 송금을 처리하는 메서드
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

    // 금액 입력 필드의 값을 처리하는 메서드
    public void OnInputField()
    {
        int money;

        if (int.TryParse(moneyInputField.text, out money))
        {
            OnRemittanceCode(money);
        }
    }

    // 뒤로 가기 버튼 클릭 시 호출되는 메서드
    public void OnBackButton()
    {
        remittance.gameObject.SetActive(false); // 송금 오브젝트 비활성화
        atm.gameObject.SetActive(true); // ATM 오브젝트 활성화
    }
}
