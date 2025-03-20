using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public UserData userData;

    //public List<UserData> userDataList = new List<UserData>();


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SetUserData("±èÅÂ°â", 85000, 115000);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUserData(string userName, int cash, int BankBalance)
    {
        userData = new UserData(userName, cash, BankBalance);
    }

}
