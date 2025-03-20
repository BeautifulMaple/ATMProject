using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public List<UserData> userDataList = new List<UserData>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SetUserData("Player", 1000, 10000);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUserData(string userName, int cash, int BankBalance)
    {
        userDataList.Add(new UserData(userName, cash, BankBalance));
    }
}
