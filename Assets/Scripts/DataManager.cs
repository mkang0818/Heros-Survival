using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// 저장하는 방법
// 1. 저장할 데이터가 존재
// 2. 데이터를 제이슨으로 변환 tojson
// 3. 제이슨을 외부에 저장

// 불러오는 방법
// 1. 외부에 저장된 제이슨을 가져옴
// 2. 제이슨을 데이터 형태로 변환
// 3. 불러온 데이터를 사용

public class PlayerData
{
    // 최초접속, 이름, 최고점수, 보석,     ///   착용중인 캐릭터, 캐릭터 강화등급
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
        DataInit(); // 최초접속 시 데이터 초기화
        LoadData(); // 데이터 불러오기
    }

    //처음 접속일 때 모든 데이터 초기화
    void DataInit()
    {
        if (!nowPlayer.Firstconnect) // 최초접속 시
        {
            //print("데이터 초기화");

            //캐릭터 등급 초기화
            for (int i = 0; i < 14; i++) nowPlayer.CharGrade[i] = 1;

            //캐릭터 보유변수 초기화
            for (int i = 0; i < 3; i++) nowPlayer.IsChar[i] = true;
            for (int i = 3; i < 14; i++) nowPlayer.IsChar[i] = false;
            
            //닉네임, 보석, 점수 초기화
            nowPlayer.nickName = "NICKNAME";
            nowPlayer.Score = 30;
            nowPlayer.Diamond = 30;
            nowPlayer.Firstconnect = true;

            SaveData();
        }
        else if(nowPlayer.Firstconnect) //기존 유저일 시
        {
            LoadData();
            //print("기존 유저");
            nowPlayer.Firstconnect = true;
            SaveData();
        }
    }

    //데이터 저장
    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);

        File.WriteAllText(path + fileName, data);
    }

    //데이터 불러오기
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
