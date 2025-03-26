using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    [Header("User Data")]
    public TextMeshProUGUI userNameText;    // 사용자 이름 UI 텍스트
    public TextMeshProUGUI cashMoneyText;   // 현금 보유량 UI 텍스트
    public TextMeshProUGUI bankBalanceText; // 은행 잔고 UI 텍스트

    [Header("GameObject")]
    public GameObject atm;         // ATM 메뉴 UI
    public GameObject deposit;     // 입금 UI
    public GameObject withdraw;    // 출금 UI
    public GameObject remittance;  // 송금 UI
    public GameObject popupBank;   // 은행 팝업 UI
    public GameObject login;       // 로그인 UI

    [Header("Checking account")]
    public Button depositButton;    // 입금 버튼
    public Button withdrawButton;   // 출금 버튼
    public Button remittanceButton; // 송금 버튼
    public Button loginBackButton;  // 뒤로 가기 버튼

    private UserData userData; // 현재 로그인한 사용자 정보

    void Start()
    {
        // GameManager에서 현재 사용자 데이터 가져오기
        userData = GameManager.Instance.userData;

        // 버튼 클릭 시 실행할 메서드 연결
        depositButton.onClick.AddListener(OoDepositButton);
        withdrawButton.onClick.AddListener(OoWithdrawButton);
        remittanceButton.onClick.AddListener(OoRemittanceButton);
        loginBackButton.onClick.AddListener(OnBackButton);

        // UI 정보 갱신
        ReFresh();
    }

    /// <summary>
    /// 사용자 정보를 UI에 반영하여 갱신하는 메서드
    /// </summary>
    public void ReFresh()
    {
        // GameManager에서 현재 사용자 데이터 갱신
        userData = GameManager.Instance.userData;

        // UI 텍스트 갱신
        userNameText.text = userData.userName;
        cashMoneyText.text = string.Format("{0}", userData.cash.ToString("N0")); // 현금을 천 단위로 표시
        bankBalanceText.text = string.Format("{0}", userData.bankBalance.ToString("N0")); // 은행 잔고 천 단위 표시
    }

    /// <summary>
    /// 입금 버튼 클릭 시 입금 UI 활성화
    /// </summary>
    public void OoDepositButton()
    {
        atm.gameObject.SetActive(false);     // ATM UI 비활성화
        deposit.gameObject.SetActive(true);  // 입금 UI 활성화
    }

    /// <summary>
    /// 출금 버튼 클릭 시 출금 UI 활성화
    /// </summary>
    public void OoWithdrawButton()
    {
        atm.gameObject.SetActive(false);     // ATM UI 비활성화
        withdraw.gameObject.SetActive(true); // 출금 UI 활성화
    }

    /// <summary>
    /// 송금 버튼 클릭 시 송금 UI 활성화
    /// </summary>
    public void OoRemittanceButton()
    {
        atm.gameObject.SetActive(false);      // ATM UI 비활성화
        remittance.gameObject.SetActive(true); // 송금 UI 활성화
    }

    /// <summary>
    /// 뒤로 가기 버튼 클릭 시 로그인 UI 및 ATM UI 활성화
    /// </summary>
    public void OnBackButton()
    {
        // 로그인 UI가 비활성화 상태일 경우 활성화
        if (!login.activeSelf)
        {
            login.gameObject.SetActive(true);       // 로그인 UI 활성화
            popupBank.gameObject.SetActive(false);  // 은행 팝업 UI 비활성화
        }

        atm.gameObject.SetActive(true); // ATM UI 활성화
    }
}
