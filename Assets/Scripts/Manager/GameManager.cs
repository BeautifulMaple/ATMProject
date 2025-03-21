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
            LoadUserData(); // UserData.json 파일을 읽어서 userData에 저장
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
        SaveUserData(); // userData를 UserData.json 파일로 저장
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
            Debug.Log("불러오기 성공");
        }
        else
        {
            Debug.Log("저장된 파일이 없습니다.");
            userDataList = new List<UserData>(); // 빈 리스트 초기화
        }
    }
}
