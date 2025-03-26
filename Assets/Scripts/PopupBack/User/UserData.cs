using UnityEngine;

[System.Serializable]
public class UserData
{
    [SerializeField]

    public string id;           // ���̵�
    public string pw;           // ��й�ȣ

    public string userName;     // ���� �̸�
    public int cash;            // ����
    public int bankBalance;     // ���� �ܰ�


    public UserData(string id, string pw, string userName, int cash, int BankBalance)
    {
        this.id = id;
        this.pw = pw;

        this.userName = userName;
        this.cash = cash;
        this.bankBalance = BankBalance;
    }
}
