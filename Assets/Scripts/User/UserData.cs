using UnityEngine;

[System.Serializable]
public class UserData
{
    [SerializeField]

    public string id;
    public string pw;

    public string userName;     //{ get; set; }
    public int cash;            //{ get; private set; }
    public int bankBalance;     //{ get; private set; }


    public UserData(string id, string pw, string userName, int cash, int BankBalance)
    {
        this.id = id;
        this.pw = pw;

        this.userName = userName;
        this.cash = cash;
        this.bankBalance = BankBalance;
    }
}
