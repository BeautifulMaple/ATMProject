using UnityEngine;

[System.Serializable]
public class UserData
{
    [SerializeField]
    public string userName;  //{ get; set; }
    public int cash; //{ get; private set; }
    public int bankBalance; //{ get; private set; }

    public UserData(string userName, int cash, int BankBalance)
    {
        this.userName = userName;
        this.cash = cash;
        this.bankBalance = BankBalance;
    }
}
