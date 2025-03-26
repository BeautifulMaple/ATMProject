using UnityEngine;

[System.Serializable]
public class UserData
{
    [SerializeField]

    public string id;           // 아이디
    public string pw;           // 비밀번호

    public string userName;     // 유저 이름
    public int cash;            // 현금
    public int bankBalance;     // 통장 잔고


    public UserData(string id, string pw, string userName, int cash, int BankBalance)
    {
        this.id = id;
        this.pw = pw;

        this.userName = userName;
        this.cash = cash;
        this.bankBalance = BankBalance;
    }
}
