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
            //SetUserData("���°�", 85000, 115000);
            LoadUserData(); // UserData.json ������ �о userData�� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUserData(string userName, int cash, int BankBalance)
    {
        userData = new UserData(userName, cash, BankBalance);
        SaveUserData(); // userData�� UserData.json ���Ϸ� ����
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
            Debug.Log("�ҷ����� ����");
        }
        else
        {
            Debug.Log("����� ������ �����ϴ�.");
            SetUserData("���°�", 85000, 115000);
            Debug.Log("���°�, 85000, 115000. ���� ������ ����ϴ�.");

        }
    }
}
