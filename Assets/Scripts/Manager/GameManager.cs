using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public List<UserData> userDataList = new List<UserData>();
    public UserData userData;

    private string Savepath;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Savepath = Path.Combine(Application.dataPath, "JsonFile/UserData.json");
            LoadUserData(); // UserData.json ������ �о userData�� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<UserData> GetUserDataList()
    {
        return userDataList;
    }

    public void AddUserData(string id, string pw, string userName, int cash, int BankBalance)
    {
        userData = new UserData(id, pw, userName, cash, BankBalance);
        userDataList.Add(userData);
        SaveUserData(); // userData�� UserData.json ���Ϸ� ����
    }
    public void SaveUserData()
    {
        string jsonData = JsonConvert.SerializeObject(userDataList, Formatting.Indented);
        File.WriteAllText(Savepath, jsonData);
    }

    public void LoadUserData()
    {
        if (File.Exists(Savepath))
        {
            string jsonData = File.ReadAllText(Savepath);
            userDataList = JsonConvert.DeserializeObject<List<UserData>>(jsonData);
            Debug.Log("�ҷ����� ����");
        }
        else
        {
            Debug.Log("����� ������ �����ϴ�.");
            userDataList = new List<UserData>(); // �� ����Ʈ �ʱ�ȭ
        }
    }
}
