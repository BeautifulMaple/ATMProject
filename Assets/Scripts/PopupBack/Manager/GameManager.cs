using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public List<UserData> userDataList = new List<UserData>();  // 저장된 사용자 데이터 리스트
    public UserData userData;   // 현재 로그인한 사용자 데이터

    private string Savepath;    // 사용자 데이터 저장 경로

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
    /// <summary>
    /// 저장된 모든 사용자 데이터 반환하기
    /// </summary>
    public List<UserData> GetUserDataList()
    {
        return userDataList;
    }
    /// <summary>
    /// 새로운 사용자 데이터 추가하기
    /// </summary>
    /// <param name="id">사용자 ID</param>
    /// <param name="pw">사용자 비밀번호</param>
    /// <param name="userName">사용자 이름</param>
    /// <param name="cash">초기 보유 현금</param>
    /// <param name="BankBalance">초기 은행 잔고</param>
    public void AddUserData(string id, string pw, string userName, int cash, int BankBalance)
    {
        userData = new UserData(id, pw, userName, cash, BankBalance);   // 새로운 유저 데이터
        userDataList.Add(userData); //추가하기
        SaveUserData(); // userData를 UserData.json 파일로 저장
    }
    /// <summary>
    /// 사용자 데이터를 Json 파일로 저장(직렬화)
    /// </summary>
    public void SaveUserData()
    {
        string jsonData = JsonConvert.SerializeObject(userDataList, Formatting.Indented);
        File.WriteAllText(Savepath, jsonData);
    }
    /// <summary>
    /// Json 파일에서 사용자 데이터를 불러오는 메서드
    /// </summary>
    public void LoadUserData()
    {
        if (File.Exists(Savepath))  // 파일이 존재하는 지 확인
        {
            string jsonData = File.ReadAllText(Savepath);   // 파일 내용 읽기
            userDataList = JsonConvert.DeserializeObject<List<UserData>>(jsonData); // JSON 데이터를 리스트로 변환
            Debug.Log("불러오기 성공");
        }
        else
        {
            Debug.Log("저장된 파일이 없습니다.");
            userDataList = new List<UserData>(); // 빈 리스트 초기화
        }
    }
    /// <summary>
    /// 사용자 로그아웃
    /// </summary>
    public void Logout()
    {
        Debug.Log($"로그아웃: {userData?.userName}");
        userData = null;
    }
    /// <summary>
    /// 입력된 ID와 비밀번호를 확인하여 로그인
    /// </summary>
    /// <param name="id">입력한 ID</param>
    /// <param name="pw">입력한 비밀번호</param>
    /// <returns></returns>
    public bool SetCurrentUser(string id, string pw)
    {
        userData = userDataList.Find(user => user.id == id && user.pw == pw);
        return userData != null;    // 로그인 성공 여부
    }
}
