using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// �����ϴ� ���
// 1. ������ �����Ͱ� ����
// 2. �����͸� ���̽����� ��ȯ tojson
// 3. ���̽��� �ܺο� ����

// �ҷ����� ���
// 1. �ܺο� ����� ���̽��� ������
// 2. ���̽��� ������ ���·� ��ȯ
// 3. �ҷ��� �����͸� ���

public class PlayerData
{
    // ��������, �̸�, �ְ�����, ����,     ///   �������� ĳ����, ĳ���� ��ȭ���
    public bool Firstconnect;
    public string nickName;
    public int Score;
    public int Diamond;
    public bool[] IsChar = new bool[14];
    public int[] CharGrade = new int[14];
}

public class DataManager : MonoBehaviour
{
    public PlayerData nowPlayer = new PlayerData();

    string path;
    string fileName = "PlayerSaveFile";


    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        path = Application.persistentDataPath + "/";
        print(path);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        DataInit(); // �������� �� ������ �ʱ�ȭ
        LoadData(); // ������ �ҷ�����
    }

    //ó�� ������ �� ��� ������ �ʱ�ȭ
    void DataInit()
    {
        if (!nowPlayer.Firstconnect) // �������� ��
        {
            //print("������ �ʱ�ȭ");

            //ĳ���� ��� �ʱ�ȭ
            for (int i = 0; i < 14; i++) nowPlayer.CharGrade[i] = 1;

            //ĳ���� �������� �ʱ�ȭ
            for (int i = 0; i < 3; i++) nowPlayer.IsChar[i] = true;
            for (int i = 3; i < 14; i++) nowPlayer.IsChar[i] = false;
            
            //�г���, ����, ���� �ʱ�ȭ
            nowPlayer.nickName = "NICKNAME";
            nowPlayer.Score = 30;
            nowPlayer.Diamond = 30;
            nowPlayer.Firstconnect = true;

            SaveData();
        }
        else if(nowPlayer.Firstconnect) //���� ������ ��
        {
            LoadData();
            //print("���� ����");
            nowPlayer.Firstconnect = true;
            SaveData();
        }
    }

    //������ ����
    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + fileName, data);
    }

    //������ �ҷ�����
    public void LoadData()
    {
        string filePath = path + fileName;

        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(path + fileName);
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        }
    }
}
