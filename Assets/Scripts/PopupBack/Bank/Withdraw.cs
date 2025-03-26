using UnityEngine;
using UnityEngine.UI;

public class Withdraw : MonoBehaviour
{
    public InputField withdrawInputField; // 출금 금액 입력 필드

    [Header("Money")]
    public Button withdrawTenThousandButton;   // 1만 원 출금 버튼
    public Button withdrawThirtyThousandButton; // 3만 원 출금 버튼
    public Button withdrawFiftyThousandButton; // 5만 원 출금 버튼

    public Button backButton;  // 뒤로 가기 버튼
    public Button withdrawInputFieldButton; // 입력한 금액 출금 버튼

    [Header("Object")]
    public GameObject atm;              // ATM UI (송금, 임금, 출금 메뉴)
    public GameObject withdrawObject;    // 출금 UI
    public GameObject error;             // 출금 실패 시 에러 메시지

    private UserData userData;  // 현재 사용자 데이터
    private PopupBank popupBank; // 은행 UI 갱신을 위한 PopupBank 인스턴스

    private void Awake()
    {
        // 출금 입력 필드를 숫자만 입력 가능하도록 설정
        withdrawInputField.contentType = InputField.ContentType.IntegerNumber;
    }

    void Start()
    {
        // 현재 사용자 정보 가져오기
        userData = GameManager.Instance.userData;
        popupBank = FindObjectOfType<PopupBank>(); // PopupBank 인스턴스를 찾아 할당

        // 버튼 클릭 이벤트 추가
        withdrawTenThousandButton.onClick.AddListener(OnTenThousandButton);
        withdrawThirtyThousandButton.onClick.AddListener(OnThirtyThousandButton);
        withdrawFiftyThousandButton.onClick.AddListener(OnFiftyThousandButton);
        withdrawInputFieldButton.onClick.AddListener(OnInputField);
        backButton.onClick.AddListener(OnBackButton);

        // 은행 UI 초기 갱신
        popupBank.ReFresh();
    }

    /// <summary>
    /// 뒤로 가기 버튼 클릭 시 실행되는 메서드 (출금 UI 닫기)
    /// </summary>
    public void OnBackButton()
    {
        withdrawObject.SetActive(false); // 출금 UI 비활성화
        atm.SetActive(true); // ATM UI 활성화
    }

    /// <summary>
    /// 1만 원 출금 버튼 클릭 시 실행되는 메서드
    /// </summary>
    public void OnTenThousandButton()
    {
        OnWithdrawMoney(MoneyUnit.tenThousand);
    }

    /// <summary>
    /// 3만 원 출금 버튼 클릭 시 실행되는 메서드
    /// </summary>
    public void OnThirtyThousandButton()
    {
        OnWithdrawMoney(MoneyUnit.thirtyThousand);
    }

    /// <summary>
    /// 5만 원 출금 버튼 클릭 시 실행되는 메서드
    /// </summary>
    public void OnFiftyThousandButton()
    {
        OnWithdrawMoney(MoneyUnit.fiftyThousand);
    }

    /// <summary>
    /// 출금 UI가 활성화될 때 실행되는 메서드 (사용자 데이터 갱신)
    /// </summary>
    private void OnEnable()
    {
        userData = GameManager.Instance.userData;
    }

    /// <summary>
    /// 출금 기능 실행 메서드
    /// </summary>
    /// <param name="money">출금할 금액</param>
    public void OnWithdrawMoney(int money)
    {
        // 계좌 잔액이 출금할 금액보다 많거나 같을 경우 출금 진행
        if (userData.bankBalance >= money)
        {
            userData.cash += money;           // 현금 증가
            userData.bankBalance -= money;    // 은행 잔액 감소
            GameManager.Instance.SaveUserData(); // 변경된 데이터 저장
            popupBank.ReFresh();  // UI 갱신
        }
        else
        {
            error.SetActive(true); // 출금 불가 메시지 활성화
        }
    }

    /// <summary>
    /// 입력된 금액을 출금하는 메서드
    /// </summary>
    public void OnInputField()
    {
        int money;
        // 입력된 값이 숫자로 변환 가능한 경우 출금 실행
        if (int.TryParse(withdrawInputField.text, out money))
        {
            OnWithdrawMoney(money);
        }
    }
}
