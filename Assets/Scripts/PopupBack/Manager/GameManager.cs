using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public List<UserData> userDataList = new List<UserData>();  // ����� ����� ������ ����Ʈ
    public UserData userData;   // ���� �α����� ����� ������

    private string Savepath;    // ����� ������ ���� ���

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
    /// <summary>
    /// ����� ��� ����� ������ ��ȯ�ϱ�
    /// </summary>
    public List<UserData> GetUserDataList()
    {
        return userDataList;
    }
    /// <summary>
    /// ���ο� ����� ������ �߰��ϱ�
    /// </summary>
    /// <param name="id">����� ID</param>
    /// <param name="pw">����� ��й�ȣ</param>
    /// <param name="userName">����� �̸�</param>
    /// <param name="cash">�ʱ� ���� ����</param>
    /// <param name="BankBalance">�ʱ� ���� �ܰ�</param>
    public void AddUserData(string id, string pw, string userName, int cash, int BankBalance)
    {
        userData = new UserData(id, pw, userName, cash, BankBalance);   // ���ο� ���� ������
        userDataList.Add(userData); //�߰��ϱ�
        SaveUserData(); // userData�� UserData.json ���Ϸ� ����
    }
    /// <summary>
    /// ����� �����͸� Json ���Ϸ� ����(����ȭ)
    /// </summary>
    public void SaveUserData()
    {
        string jsonData = JsonConvert.SerializeObject(userDataList, Formatting.Indented);
        File.WriteAllText(Savepath, jsonData);
    }
    /// <summary>
    /// Json ���Ͽ��� ����� �����͸� �ҷ����� �޼���
    /// </summary>
    public void LoadUserData()
    {
        if (File.Exists(Savepath))  // ������ �����ϴ� �� Ȯ��
        {
            string jsonData = File.ReadAllText(Savepath);   // ���� ���� �б�
            userDataList = JsonConvert.DeserializeObject<List<UserData>>(jsonData); // JSON �����͸� ����Ʈ�� ��ȯ
            Debug.Log("�ҷ����� ����");
        }
        else
        {
            Debug.Log("����� ������ �����ϴ�.");
            userDataList = new List<UserData>(); // �� ����Ʈ �ʱ�ȭ
        }
    }
    /// <summary>
    /// ����� �α׾ƿ�
    /// </summary>
    public void Logout()
    {
        Debug.Log($"�α׾ƿ�: {userData?.userName}");
        userData = null;
    }
    /// <summary>
    /// �Էµ� ID�� ��й�ȣ�� Ȯ���Ͽ� �α���
    /// </summary>
    /// <param name="id">�Է��� ID</param>
    /// <param name="pw">�Է��� ��й�ȣ</param>
    /// <returns></returns>
    public bool SetCurrentUser(string id, string pw)
    {
        userData = userDataList.Find(user => user.id == id && user.pw == pw);
        return userData != null;    // �α��� ���� ����
    }
}
