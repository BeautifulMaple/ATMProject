using UnityEngine.UI;
using UnityEngine;

public class Deposit : MonoBehaviour
{
    public InputField depositInputField; // 입금 금액 입력 필드

    [Header("Money")]
    public Button depositTenThousandButton;   // 1만 원 입금 버튼
    public Button depositThirtyThousandButton; // 3만 원 입금 버튼
    public Button depositFiftyThousandButton; // 5만 원 입금 버튼

    public Button backButton;  // 뒤로 가기 버튼
    public Button depositInputFieldButton; // 입력한 금액 입금 버튼

    [Header("Object")]
    public GameObject atm;              // ATM UI (송금, 임금, 출금 메뉴)
    public GameObject depositObject;    // 입금 UI
    public GameObject error;            // 입금 실패 시 에러 메시지

    private UserData userData => GameManager.Instance.userData; // 현재 사용자 데이터
    private PopupBank popupBank; // 은행 UI 갱신을 위한 PopupBank 인스턴스

    private void Awake()
    {
        // 입금 입력 필드를 숫자만 입력 가능하도록 설정
        depositInputField.contentType = InputField.ContentType.IntegerNumber;
    }

    private void Start()
    {
        popupBank = FindObjectOfType<PopupBank>(); // PopupBank 인스턴스를 찾아 할당

        // 버튼 클릭 이벤트 추가
        depositTenThousandButton.onClick.AddListener(OnTenThousandButton);
        depositThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        depositFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);
        depositInputFieldButton.onClick.AddListener(OnInputField);
        backButton.onClick.AddListener(OnBackButton);

        // 은행 UI 초기 갱신
        popupBank.ReFresh();
    }

    /// <summary>
    /// 뒤로 가기 버튼 클릭 시 실행되는 메서드 (입금 UI 닫기)
    /// </summary>
    public void OnBackButton()
    {
        depositObject.SetActive(false); // 입금 UI 비활성화
        atm.SetActive(true); // ATM UI 활성화
        popupBank.ReFresh(); // UI 갱신
    }

    /// <summary>
    /// 1만 원 입금 버튼 클릭 시 실행되는 메서드
    /// </summary>
    public void OnTenThousandButton()
    {
        OnDepositMoney(MoneyUnit.tenThousand);
    }

    /// <summary>
    /// 3만 원 입금 버튼 클릭 시 실행되는 메서드
    /// </summary>
    public void OnThirtyThousandButton()
    {
        OnDepositMoney(MoneyUnit.thirtyThousand);
    }

    /// <summary>
    /// 5만 원 입금 버튼 클릭 시 실행되는 메서드
    /// </summary>
    public void OnFiftyThousandButton()
    {
        OnDepositMoney(MoneyUnit.fiftyThousand);
    }

    /// <summary>
    /// 입금 기능 실행 메서드
    /// </summary>
    /// <param name="money">입금할 금액</param>
    public void OnDepositMoney(int money)
    {
        // 현재 유저 정보 로그 출력 (디버깅용)
        Debug.Log($" 유저 정보 - ID: {userData.id}, UserName: {userData.userName}, Cash: {userData.cash}, BankBalance: {userData.bankBalance}");

        // 사용자의 현금이 입금할 금액보다 많거나 같을 경우 입금 진행
        if (userData.cash >= money)
        {
            userData.cash -= money;           // 현금 감소
            userData.bankBalance += money;    // 은행 잔액 증가
            GameManager.Instance.SaveUserData(); // 변경된 데이터 저장
            popupBank.ReFresh();  // UI 갱신
        }
        else
        {
            error.SetActive(true); // 입금 불가 메시지 활성화
        }
    }

    /// <summary>
    /// 입력된 금액을 입금하는 메서드
    /// </summary>
    public void OnInputField()
    {
        int money;
        // 입력된 값이 숫자로 변환 가능한 경우 입금 실행
        if (int.TryParse(depositInputField.text, out money))
        {
            OnDepositMoney(money);
        }
    }
}
