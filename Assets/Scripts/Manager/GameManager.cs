using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private string path = Path.Combine(Application.dataPath, "JsonFile/UserData.json");
    public UserData userData;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //SetUserData("김태겸", 85000, 115000);
            LoadUserData(); // UserData.json 파일을 읽어서 userData에 저장
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUserData(string userName, int cash, int BankBalance)
    {
        userData = new UserData(userName, cash, BankBalance);
        SaveUserData(); // userData를 UserData.json 파일로 저장
    }
    public void SaveUserData()
    {
        string jsonData = JsonUtility.ToJson(userData, true);
        string Savepath = path;
        File.WriteAllText(Savepath, jsonData);
    }

    public void LoadUserData()
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            userData = JsonUtility.FromJson<UserData>(jsonData);
            Debug.Log("불러오기 성공");
        }
        else
        {
            Debug.Log("저장된 파일이 없습니다.");
            SetUserData("김태겸", 85000, 115000);
            Debug.Log("김태겸, 85000, 115000. 으로 파일을 만듭니다.");

        }
    }
}
